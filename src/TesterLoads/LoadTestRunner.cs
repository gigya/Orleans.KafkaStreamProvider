using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans;
using Orleans.Runtime;
using Orleans.TestingHost;
using TestGrainInterfaces;
using TestGrains.Models;
using UnitTests.GrainInterfaces;

namespace TesterLoads
{    
    public class LoadTestRunner
    {
        private readonly string _streamProviderName;
        private readonly Logger _logger;

        public LoadTestRunner(string streamProviderName, Logger logger)
        {
            _streamProviderName = streamProviderName;
            _logger = logger;
        }

        public async Task TimeToConsumeTest(int numOfMessagesInBatch, int numOfTimeToProduce)
        {
            var producer = GrainClient.GrainFactory.GetGrain<ICustomEventProducerGrain<LoadTestMessage>>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ICustomEventConsumerGrain<LoadTestMessage>>(Guid.NewGuid());

            var streamGuid = Guid.NewGuid();
            var streamNamespace = "LoadTestNamespace";

            await producer.BecomeProducer(streamGuid, streamNamespace, _streamProviderName);
            await consumer.BecomeConsumer(streamGuid, streamNamespace, _streamProviderName);
            await consumer.SetMessageExpectation(numOfMessagesInBatch * numOfTimeToProduce);

            var messages = new List<LoadTestMessage>();

            for (int i = 0; i < numOfMessagesInBatch; i++)
            {
                messages.Add(LoadTestMessage.GenerateTestMessage());
            }

            for (int i = 0; i < numOfTimeToProduce; i++)
            {
                await producer.Produce(messages);    
            }
            
            while (!consumer.HasFinishedConsuming().Result)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
            }

            await consumer.StopConsuming();

            var numConsumed = await consumer.GetNumberConsumed();
            var numProduced = await producer.GetNumberProduced();

            _logger.Info("***Consuming***");
            _logger.Info("Total number of events consumed: {0}", numConsumed);
            _logger.Info("***Producing***");
            _logger.Info("Total number of events produced: {0}", numProduced);
            _logger.Info("Total consuming time : {0}", consumer.GetConsumingTime().Result.TotalMilliseconds);
            _logger.Info("Total deliver time : {0}", consumer.GetTotalSendTime().Result.TotalMilliseconds);
            _logger.Info("**************************************************", consumer.GetConsumingTime().Result.TotalMilliseconds);
        }

        public async Task TimeToProduceTest(int numOfMessagesInBatch, int numOfTimeToProduce)
        {
            var producer = GrainClient.GrainFactory.GetGrain<ICustomEventProducerGrain<LoadTestMessage>>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ICustomEventConsumerGrain<LoadTestMessage>>(Guid.NewGuid());

            var streamGuid = Guid.NewGuid();
            var streamNamespace = "LoadTestNamespace";

            await producer.BecomeProducer(streamGuid, streamNamespace, _streamProviderName);
            await consumer.BecomeConsumer(streamGuid, streamNamespace, _streamProviderName);
            await consumer.SetMessageExpectation(numOfMessagesInBatch * numOfTimeToProduce);

            var messages = new List<LoadTestMessage>();

            for (int i = 0; i < numOfMessagesInBatch; i++)
            {
                messages.Add(LoadTestMessage.GenerateTestMessage());
            }

            for (int i = 0; i < numOfTimeToProduce; i++)
            {
                await producer.Produce(messages);
            }

            while (!consumer.HasFinishedConsuming().Result)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
            }

            await consumer.StopConsuming();

            var numConsumed = await consumer.GetNumberConsumed();
            var numProduced = await producer.GetNumberProduced();

            _logger.Info("***Consuming***");
            _logger.Info("Total number of events consumed: {0}", numConsumed);
            _logger.Info("***Producing***");
            _logger.Info("Total number of events produced: {0}", numProduced);
            _logger.Info("Total producing time : {0}", consumer.GetConsumingTime().Result.TotalMilliseconds);
            _logger.Info("Total send time : {0}", consumer.GetTotalSendTime().Result.TotalMilliseconds);
            _logger.Info("**************************************************", consumer.GetConsumingTime().Result.TotalMilliseconds);
        }

        public async Task RunStreams(int numOfStreams, int numOfProducersPerStream, int sizeOfBatch, int numOfSends)
        {
            string streamNamespace = "LoadTestNamespace";
            
            List<Guid> streamGuids = new List<Guid>();
            Dictionary<Guid, List<ICustomEventProducerGrain<LoadTestMessage>>> producers = new Dictionary<Guid, List<ICustomEventProducerGrain<LoadTestMessage>>>();
            Dictionary<Guid, ICustomEventConsumerGrain<LoadTestMessage>> consumers =
                new Dictionary<Guid, ICustomEventConsumerGrain<LoadTestMessage>>();

            for (int i = 0; i < numOfStreams; i++)
            {
                var newStreamGuid = Guid.NewGuid();
                streamGuids.Add(newStreamGuid);

                var producersForStream = new List<ICustomEventProducerGrain<LoadTestMessage>>();
                for (int j = 0; j < numOfProducersPerStream; j++)
                {
                    var newProducer =
                        GrainClient.GrainFactory.GetGrain<ICustomEventProducerGrain<LoadTestMessage>>(Guid.NewGuid());
                    await newProducer.BecomeProducer(newStreamGuid, streamNamespace, _streamProviderName);
                    producersForStream.Add(newProducer);                    
                }

                producers.Add(newStreamGuid, producersForStream);
            }

            foreach (var currentGuid in streamGuids)
            {
                var newConsumer = GrainClient.GrainFactory.GetGrain<ICustomEventConsumerGrain<LoadTestMessage>>(currentGuid);
                await newConsumer.BecomeConsumer(currentGuid, streamNamespace, _streamProviderName);
                await newConsumer.SetMessageExpectation(sizeOfBatch*numOfSends*numOfProducersPerStream);
                consumers.Add(currentGuid, newConsumer);
            }

            var batch = new List<LoadTestMessage>();

            for (int i = 0; i < sizeOfBatch; i++)
            {
                batch.Add(LoadTestMessage.GenerateTestMessage());
            }

            List<ICustomEventProducerGrain<LoadTestMessage>> allProducers = new List<ICustomEventProducerGrain<LoadTestMessage>>();
            producers.Values.ToList().ForEach(p => allProducers.AddRange(p));
            
            List<Task> startProduceList = new List<Task>();
            List<Task> stoppingList = new List<Task>();

            for (int i = 0; i < numOfSends; i++)
            {
                allProducers.ForEach(p => startProduceList.Add(p.Produce(batch)));    
            }
            
            await Task.WhenAll(startProduceList);            

            List<Task> waitingTasks = (from currentGuid in producers.Keys 
                                       let totalNumProduced = producers[currentGuid].Select(s => s.GetNumberProduced().Result).Sum()
                                       select TestingUtils.WaitUntilAsync(lastTry => consumers[currentGuid].HasFinishedConsuming(), TimeSpan.FromSeconds(30))).ToList();

            await Task.WhenAll(waitingTasks);

            consumers.Values.ToList().ForEach(c => stoppingList.Add(c.StopConsuming()));

            await Task.WhenAll(stoppingList);

            TimeSpan slowestConsumer = consumers.Values.Select(c => c.GetConsumingTime().Result).Max();
            TimeSpan slowestProducer = allProducers.Select(p => p.GetTotalProducedTime().Result).Max();
            TimeSpan totalSendTime = consumers.Values.Select(c => c.GetTotalSendTime().Result).Max();

            await WriteResults(sizeOfBatch, producers, consumers.Values.ToList(), slowestConsumer, slowestProducer, totalSendTime);
        }

        private async Task WriteResults(int batchSize, Dictionary<Guid, List<ICustomEventProducerGrain<LoadTestMessage>>> producers,
            List<ICustomEventConsumerGrain<LoadTestMessage>> consumers, TimeSpan slowestConsumer, TimeSpan slowestProdcer, TimeSpan totalSendTime)
        {
            var eachProducerNumProduced = new Dictionary<Guid,List<int>>();
            var eachConsumerNumConsumed = new List<int>();

            foreach (var producersForStream in producers)
            {
                var producerCountList = new List<int>();
                foreach (var producer in producersForStream.Value)
                {
                    var numProduced = await producer.GetNumberProduced();
                    producerCountList.Add(numProduced);
                }

                eachProducerNumProduced.Add(producersForStream.Key, producerCountList);
            }
            foreach (var consumer in consumers)
            {
                var numConsumed = await consumer.GetNumberConsumed();
                eachConsumerNumConsumed.Add(numConsumed);
            }

            _logger.Info("********************************************* Test Results **************************************************");
            _logger.Info("***Consuming***");
            _logger.Info("Total number of events consumed: {0}", eachConsumerNumConsumed.Sum());
            _logger.Info("Avg number of events consumed: {0}", eachConsumerNumConsumed.Average());
            _logger.Info("Number of consumers: {0}", eachConsumerNumConsumed.Count);
            _logger.Info("***Producing***");
            _logger.Info("Total number of events produced: {0}", eachProducerNumProduced.Values.Select(s => s.Sum()).Sum());
            _logger.Info("Avg events produced per stream: {0}", eachProducerNumProduced.Values.Select(s => s.Sum()).Average());
            _logger.Info("Avg number of events per producer: {0}", eachProducerNumProduced.Values.Select(s => s.Average()).Average());
            _logger.Info("Batch size per producing: {0}", batchSize);
            _logger.Info("***Streams***");
            _logger.Info("Total number of streams: {0}", producers.Count);
            _logger.Info("Slowest consumer was: {0}", slowestConsumer.TotalMilliseconds);
            _logger.Info("Slowest producer was: {0}", slowestProdcer.TotalMilliseconds);            
            _logger.Info("Total delivery time was: {0}", totalSendTime.TotalMilliseconds);
        }
               
    }
}
