using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.Runtime;
using Orleans.TestingHost;
using UnitTests.Tester;

namespace Tester.StreamingTests
{
    [Serializable]
    public class Sender
    {
        protected bool Equals(Sender other)
        {
            return string.Equals(PhotoUrl, other.PhotoUrl) && string.Equals(ProfileUrl, other.ProfileUrl) && string.Equals(LoginProvider, other.LoginProvider) && string.Equals(Name, other.Name) && IsSelf == other.IsSelf;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (PhotoUrl != null ? PhotoUrl.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ProfileUrl != null ? ProfileUrl.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LoginProvider != null ? LoginProvider.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ IsSelf.GetHashCode();
                return hashCode;
            }
        }

        public string PhotoUrl { get; set; }
        public string ProfileUrl { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public bool IsSelf { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Sender) obj);
        }
    }

    [Serializable]
    public class Comment
    {
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ThreadId != null ? ThreadId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (CommentText != null ? CommentText.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Sender != null ? Sender.GetHashCode() : 0);
                return hashCode;
            }
        }

        public string Id { get; set; }
        public string ThreadId { get; set; }
        public string CommentText { get; set; }
        public Sender Sender { get; set; }

        protected bool Equals(Comment other)
        {
            return string.Equals(Id, other.Id) && string.Equals(ThreadId, other.ThreadId) && string.Equals(CommentText, other.CommentText) && Equals(Sender, other.Sender);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Comment) obj);
        }
    }

    [Serializable]
    public class EventData
    {
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (CategoryId != null ? CategoryId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (StreamId != null ? StreamId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (CommentId != null ? CommentId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Comment != null ? Comment.GetHashCode() : 0);
                return hashCode;
            }
        }

        public string CategoryId { get; set; }
        public string StreamId { get; set; }
        public string CommentId { get; set; }
        public Comment Comment { get; set; }

        protected bool Equals(EventData other)
        {
            return string.Equals(CategoryId, other.CategoryId) && string.Equals(StreamId, other.StreamId) && string.Equals(CommentId, other.CommentId) && Equals(Comment, other.Comment);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EventData) obj);
        }
    }

    [Serializable]
    public class Event
    {
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (EventData != null ? EventData.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Nonce != null ? Nonce.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Timestamp != null ? Timestamp.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Signature != null ? Signature.GetHashCode() : 0);
                return hashCode;
            }
        }

        public string Name { get; set; }
        public List<EventData> EventData { get; set; }
        public string Nonce { get; set; }
        public string Timestamp { get; set; }
        public string Signature { get; set; }

        protected bool Equals(Event other)
        {
            return string.Equals(Name, other.Name) && EventData.SequenceEqual(other.EventData) && string.Equals(Nonce, other.Nonce) && string.Equals(Timestamp, other.Timestamp) && string.Equals(Signature, other.Signature);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Event) obj);
        }
    }

    [DeploymentItem("OrleansConfigurationForStreamingUnitTests.xml")]
    [DeploymentItem("OrleansProviders.dll")]
    [DeploymentItem("Orleans.KafkaStreamProvider.dll")]
    [TestClass]
    public class KafkaCustomEventTests : UnitTestSiloHost
    {
        private const string KafkaStreamProviderName = "KafkaProvider";                 // must match what is in OrleansConfigurationForStreamingUnitTests.xml

        private readonly CustomEventTestRunner _runner;
        private static TestingSiloHost _host;

        public KafkaCustomEventTests()
            : base(new TestingSiloOptions()
            {
                StartFreshOrleans = false,
                StartSecondary = false,
                SiloConfigFile = new FileInfo("OrleansConfigurationForStreamingUnitTests.xml"),
            })
        {
            _runner = new CustomEventTestRunner(KafkaStreamProviderName, logger);
            _host = this;
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            _host.StopAllSilos();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task SimpleCustomEventTaskTest()
        {
            logger.Info("************************ SimpleCustomEventTask *********************************");
            string valueToSend = @"
                event=newComment& 
                eventData=[{ 
                    'categoryID': '17235654',
                    'streamID': 'strm1', 
                    'commentID': '2a9f579657c044f380a4a0e1b6fc2af8', 
                    'comment': { 
                        'ID': '2a9f579657c044f380a4a0e1b6fc2af8', 
                        'threadID': '', 
                        'commentText': 'This is a comment!', 
                        'sender': { 
                            'photoURL': 'http://graph.facebook.com/100002990175600/picture?type=square', 
                            'profileURL': http://www.facebook.com/profile.php?id=100002990175600', 
                            'loginProvider': 'Facebook', 
                            'name': 'Mark Zuck', 
                            'isSelf':true 
                        } 
                    } 
                 }]& 
                nonce=b7a00b88e1b9469599e5f53049eb1b6d& 
                timestamp=1332079751& ;
                signature=gCLCNiNxt8xGkzqzP7MugKLv2Ak=";

            await _runner.SimpleCustomEventTask(valueToSend);
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task SimpleCustomEventTaskTestComplexType()
        {
            var sender = new Sender()
            {
                PhotoUrl = "http://graph.facebook.com/100002990175600/picture?type=square",
                ProfileUrl = "http://www.facebook.com/profile.php?id=100002990175600",
                LoginProvider = "Facebook",
                Name = "Jessie Eizenberg",
                IsSelf = true
            };

            var comment = new Comment()
            {
                Id = "2a9f579657c044f380a4a0e1b6fc2af8",
                CommentText = "This is a comment!",
                Sender = sender,
                ThreadId = string.Empty
            };

            var eventData = new EventData()
            {
                CategoryId = "17235654",
                StreamId = "strm1",
                CommentId = "2a9f579657c044f380a4a0e1b6fc2af8",
                Comment = comment
            };

            var newEvent = new Event()
            {
                Name = "newComment",
                EventData = new List<EventData>() {eventData},
                Nonce = "b7a00b88e1b9469599e5f53049eb1b6d",
                Timestamp = "1332079751",
                Signature = "gCLCNiNxt8xGkzqzP7MugKLv2Ak"
            };

            await _runner.SimpleCustomEventTask(newEvent);
        }
    }
}
