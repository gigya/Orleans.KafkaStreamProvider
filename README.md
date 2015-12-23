# Orleans.KafkaStreamProvider
Kafka persistent stream provider for Microsoft Orleans

## Version 1.0.0

## Overview
The KafkaStreamProvider is a new implementation of a PersistentStreamProvider for Microsoft Orleans. 
It works with a modified Kafka-Net library which is a native C# client for Apache Kafka (for more information see https://github.com/gigya/KafkaNetClient)

# Dependencies
KafkaStreamProivder has the following dependencies:
* Orleans 1.1 and up 
* KafkaNetClient version 1.0.1.5 and up
* Metrics.NET version 0.2.16 and up

## Installation
To start working with the KafkaStreamProvider make sure you do the following steps:

1. Install Kafka on a machine (or cluster) which you have access to.
2. Create a Topic in Kafka with a certain name. Make sure this partition has at least as many partitions as the number of queues desired for your KafkaStreamProvider (for more information about that, check out [Usage of Kafka](#usageOfKafka))
3. Add to the Silo configuration the a new stream provider with the necessary parameters and the optional ones (if you wish). you can see what is configurable in KafkaStreamProvider under [Configurable Values](#configurableValues).
4. In order to make the Tester project run its tests successfully, you will have to configure the orleans configuration files (notice that there are many of them) in the Tester project to work with the Kafka cluster you created (you can either replace the "http://kafak1:9092" and "http://kafka2:9092" with your addresses under "ConnectionStrings" to a valid address, or just change your host file to match these addresses ).

Example for KafkaStreamProvider configuration: 
```xml
<Provider Type="Orleans.KafkaStreamProvider.PersistentStreams.KafkaStreamProvider" 
          Name="KafkaProvider" 
          ConsumerGroupName="siloConsumerGroup"
          ConnectionStrings="http://192.168.1.1:9092;http:192.168.1.2:9092"
          TopicName="TopicForSilo"
          NumOfQueues="4"/>
``` 

The relevant configuration files are:
* OrleansConfigurationForStreamingUnitTests.xml
* OrleansConfigurationForPressureTests.xml
* OrleansConfigurationForRewinding.xml

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
The [KafkaQueueAdapterReceiver](./src/Orleans.KafkaStreamProvider/KafkaQueue/KafkaQueueAdapterReceiver.cs) is responsible for fetching the messages from Kafka. For every queue (partition), a separate receiver is created that is exclusively responsible to fetch messages from it. The receiver manages the offset to fetch from and commits it to Kafka using the ConsumerGroupName so the silo has a persistent offset for each queue (partition) in the Topic.

## <a name="timedbaseCache"></a>Working with the time based cache
KafkaStreamProvider works with a time based cache.
This cache has a few key principles:
- This cache will hold messages that arrived from Kafka for a configurable TimeSpan.
- This cache has a maximum size
- This cache is **guaranteed** to contain messages for the configurable time span. That means that even if there are no cursors on a certain message (i.e - no consumers are currently consuming this message), the message will stay in the cache as long as it is in the TimeSpan limits.
- The cache is being considerate of slow consumers and will wait for them to end consuming before removing the messages.
- When a message or messages leave the cache, an offset commit will be made in `KafkaQueueAdapterReceiver` with the offset of the message with the highest offset.

Under these principles, the cache will be under pressure under the following conditions:
- The cache is full, and removing the last message in order to add the new message will violate the timespan guarantee.
- The cache is full, and removing the last message in order to add the new message will prevent a consumer to consume the message.

This implementation allows rewinding on the cache (With limitations to the configured timespan of course).

## Metrics
The KafkaStreamProvider is taking metrics of the activity that is being run inside it. You can view the metrics wherever the KafkaStreamProvider is being run on the metrics port (a configurable value).

The current metrics are:
- Meters
 - Number of kafka messages produced per second
 - Number of kafka messages consumed per second 
 - Cache evacuations per second
- Counter
 - Active receivers
 - Messages in cache
 - Number of cursors that are causing pressure
- Histograms
 - Produced messages batch size
 - Number of consumed messages per fetch
- Timers
 - Time to produce message
 - Time to consume messages
 - Time to commit offset

## <a name="configurableValues"></a>Configurable Values
These are the configurable values that the KafkaStreamProvider offers to its users, the first three are required while the others have default values:

- **ConnectionStrings**: The address or addresses to the Kafka broker(s) in the Kafka cluster, multiple addresses
  should be separated by the ";" character.
- **TopicName**: The topic the KafkaStreamProvider will work with.
- **ConsumerGroupName**: The name of the consumer group the StreamProvider will use to save offsets.
- **NumOfQueues**: The number of queues the Provider will use. *Default value is 1*
- **CacheSize**: The size of each queue's cache. The value represents the number of messages that can be stored in the PullingAgent's cache (similar to other PersistentStreamProviders). *Default value is 16384*.
- **MaxBytesInMessageSet**: The maximum size **in bytes** that a Kafka response can have. The size includes individual Kafka message headers and the response header. *Default value is 16384*.
- **AckLevel**: Sets the wanted Ack level with the KafkaBroker. The Ack level can be 0, 1 or -1 . The ack level determines from how many brokers (replicas) does the producer (in our case the QueueAdpater) need to receive an Ack from that the produced message has been saved (0 for none, 1 for the leader only, and -1 for all of the replicas). You can read more about ack level here (http://kafka.apache.org/documentation.html#replication). *Default value is 1*
- **ReceiveWaitTimeInMs** - The time the QueueAdapterReceiver will wait when fetching a batch of messages from Kafka. If the Receiver did not get any messages in the allotted time, it will return an empty batch and will try again the next time the PullingAgent asks for messages. *Default value is 100*.
- **ShouldInitWithLastOffset** - Determines whether the Receiver should get its initial offset from the value saved at Kafka (according to the ConsumerGroupName) or just take the last offset from Kafka (the top of the queue). *Default value is True*
- **CacheTimespan** - The timespan in seconds the cache will guarantee to keep. *Default value is 60*
- **MetricsPort** - The port for the Metrics to show its data on. *Default value is 20490*
- **IncludeMetrics** - A boolean that determines whether to take metrics for the KafkaStreamProvider or not. *Default value is True*
- **UsingExternalMetrics** - A boolean that tells the KafakStreamProvider whether the metrics where already initialized in an external app that is using KafkaStreamProvider. *Default value is false*

Additionally you have the default configuration options offered by Orleans to any PersistentStreamProvider which can be found here (under StreamProvider configuration): http://dotnet.github.io/orleans/Orleans-Streams/Streams-Extensibility. 

It's recommended to configure your KafkaStreamProvider to your needs in order to get optimal performance.
You can see all of the KafkaStreamProvider configuration value in the [KafkaStreamProviderOptions](./src/Orleans.KafkaStreamProvider/KafkaQueue/KafkaStreamProviderOptions.cs) class.

## TBD
- Support rewinding streams beyond the content of the cache
