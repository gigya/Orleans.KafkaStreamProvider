# Orleans.KafkaStreamProvider
Kafka persistent stream provider for Microsoft Orleans

## Version 0.8.6

## TBD
- Support rewinding streams beyond the content of the cache

## Overview
The KafkaStreamProvider is a new implementation of a PersistentStreamProvider for Microsoft Orleans. 
It works with a modified Kafka-Net library which is a native C# client for Apache Kafka (for more information see https://github.com/gigya/KafkaNetClient)

## Installation
To start working with the KafkaStreamProvider make sure you do the following steps:

1. Install Kafka on a machine (or cluster) which you have access to.
2. Create a Topic in Kafka with a certain name. Make sure this partition has at least as many partitions as the number of queues desired for your KafkaStreamProvider (for more information about that, check out [Usage of Kafka](#usageOfKafka))
3. Add to the Silo configuration the a new stream provider with the necessary parameters and the optional ones (if you wish). you can see what is configurable in KafkaStreamProvider under [Configurable Values](#configurableValues).

For Example:

```xml
<Provider Type="Orleans.KafkaStreamProvider.PersistentStreams.KafkaStreamProvider" Name="KafkaProvider" ConsumerGroupName="siloConsumerGroup" ConnectionStrings="http://192.168.1.1:9092" TopicName="TopicForSilo" NumQueues="4"/>
```

## Implementation
The KafkaStreamProvider is implemented using the Orleans Guidelines to implement a new PersistentStreamProvider over the PersistentStreamProvider class (shown in this page: http://dotnet.github.io/orleans/Orleans-Streams/Streams-Extensibility)

###The main classes written were:
- KafkaQueueAdapterFactory
- KafkaQueueAdapter
- KafkaQueueAdapaterReceiver
- KafkaBatchContainer
- KafkaStreamProviderOptions

### <a name="usageOfKafka"></a>Usage of Kafka
To understand this section better, I recommend reading a bit the [Kafka Documentation](http://kafka.apache.org/documentation.html) in order to understand the terms discussed here and in the "Configurable Values" section below). 
The KafkaStreamProvider uses Kafka in the following way:
Each Silo is configured to work with of a certain **Kafka Cluster** on a specific **Topic** with a specific **ConsumerGroupName**. On the Topic there is a certain number of **partitions**. Each partition correlates to one physical queue that can contain many of Orelans' virtual streams.

#### Producing Messages
The [KafkaQueueAdapter](./src/Orleans.KafkaStreamProvider/KafkaQueue/KafkaQueueAdapter.cs) is responsible to assign each virtual stream to a certain Queue (a partition in Kafka) and produce messages to it. There is one producer for all of the partitions in the provider.

#### Consuming Messages
The [KafkaQueueAdapterReceiver](./src/Orleans.KafkaStreamProvider/KafkaQueue/KafkaQueueAdapterReceiver.cs) is responsible for fetching the messages from Kafka. For every queue (partition) a separate receiver is created that is exclusively responsible to fetch messages from it. The receiver manages the offset to fetch from and commits it to Kafka using the ConsumerGroupName so the silo has a persistent offset for each queue (partition) in the Topic.

## <a name="timedbaseCache"></a>Working with the time based cache
Since version 0.8.6 the KafkaStreamProvider works with a time based cache.
This cache has a few key principles:
- This cache will hold messages that arrived from Kafka for a configurable TimeSpan.
- This cache has a maximum size
- This cache is **guaranteed** to contain messages for the configurable time span. That means that even if there are no cursors on a certain message, the message will stay in the cache as long as it is in the TimeSpan limits.
- The cache is being considerate of slow consumers and will wait for them to end consuming before removing the messages.

Under these principles, the cache will be under pressure under the following conditions:
- The cache is full, and removing the last message in order to add the new message will violate the timespan guarantee.
- The cache is full, and removing the last message in order to add the new message will prevent a consumer to consume the message.

The cache also get a callback function that will be called after a **configurable** number of removals from the cache. This callback is being used by the KafkaStreamProvider to commit the offsets and hence ensure that no messages will be lost in case of a sudden crash (pulled messages are stored in the cache, only when a message leaves the cache the KafkaQueueAdapterReceiver knows that it was consumed and was held in the cache the guaranteed timespan and therefore it can commit the offset). This method can fill up memory so be cautious.

## <a name="configurableValues"></a>Configurable Values
These are the configurable values that the KafkaStreamProvider offers to its users, the first three are required while the others have default values:

- **ConnectionStrings**: The address or addresses to the Kafka broker(s) in the Kafka cluster, multiple addresses
  should be separated by the ";" character.
- **TopicName**: The topic the KafkaStreamProvider will work with.
- **ConsumerGroupName**: The name of the consumer group the StreamProvider will use to save offsets.
- **NumOfQueues**: The number of queues the Provider will use. *Default value is 1*
- **CacheSize**: The size of each queue's cache. The value represents the number of messages that can be stored in the PullingAgent's cache (similar to other PersistentStreamProviders). *Default value is 16384*.
- **ProduceBatchSize**: The number of messages the QueueAdapter will gather before sending them to Kafka. *Default value is 1000*
- **TimeToWaitForBatchInMs**: The amount of time (in ms of course) the QueueAdapter will wait for the wanted ProduceBatchSize before sending the messages to Kafka anyway. *Default value is 10*.
- **MaxBytesInMessageSet**: The maximum size **in bytes** that a Kafka response can have. The size includes individual Kafka message headers and the response header. *Default value is 16384*.
- **AckLevel**: Sets the wanted Ack level with the KafkaBroker. The Ack level can be 0, 1 or -1 . The ack level determines from how many brokers (replicas) does the producer (in our case the QueueAdpater) need to receive an Ack from that the produced message has been saved (0 for none, 1 for the leader only, and -1 for all of the replicas). You can read more about ack level here (http://kafka.apache.org/documentation.html#replication). *Default value is 1*
- **ReceiveWaitTimeInMs** - The time the QueueAdapterReceiver will wait when fetching a batch of messages from Kafka. If the Receiver did not get any messages in the allotted time, it will return an empty batch and will try again the next time the PullingAgent asks for messages. *Default value is 100*.
- **OffsetCommitInterval** - Determines after how many removes from the time based cache the QueueAdapterReceiver will commit its offset to Kafka. *Default value is 1*.
- **ShouldInitWithLastOffset** - Determines whether the Receiver should get its initial offset from the value saved at Kafka (according to the ConsumerGroupName) or just take the last offset from Kafka (the top of the queue). *Default value is True*
- **CacheTimespan** - The timespan the cache will guarantee to keep.

Additionally you have the default configuration options offered by Orleans to any PersistentStreamProvider which can be found here (under StreamProvider configuration): http://dotnet.github.io/orleans/Orleans-Streams/Streams-Extensibility. 

It's important to configure your KafkaStreamProvider to your needs in order to get optimal performance.
You can see all of the KafkaStreamProvider configuration value in the [KafkaStreamProviderOptions](./src/Orleans.KafkaStreamProvider/KafkaQueue/KafkaStreamProviderOptions.cs) class.
