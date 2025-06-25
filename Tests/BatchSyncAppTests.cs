using NUnit.Framework;
using NUnit.Framework.Legacy;
using BatchSyncApp.Models;
using BatchSyncApp.Services;

namespace BatchSyncApp.Tests
{
    [TestFixture]
    public class BatchSyncProcessorTests
    {
        private BatchSyncProcessor? _processor;

        [SetUp]
        public void Setup()
        {
            // Use only handlers we need for the test
            var handlers = new List<ISyncValidator>();

            var tokenValidator = new SimpleTokenValidator();
            _processor = new BatchSyncProcessor(handlers, tokenValidator);
        }

        [Test]
        public void Should_Fail_When_Token_Is_Missing()
        {
            // Arrange: Job with missing token
            var job = new SyncJob
            {
                Id = 1,
                ObjectType = "Contact",
                Payload = "{}",
                CrmUser = new CrmUser
                {
                    Id = 1,
                    CrmPlatform = "Salesforce",
                    CrmToken = "" // Missing token
                }
            };

            var jobs = new List<SyncJob> { job };

            // Act
            _processor?.ProcessAll(jobs);

            // Assert
            ClassicAssert.AreEqual("Failed", job.SyncStatus);
            ClassicAssert.AreEqual("Missing CRM token.", job.ErrorMessage);
        }

        [Test]
        public void Should_Succeed_When_Token_Is_Valid()
        {
            var job = new SyncJob
            {
                Id = 2,
                ObjectType = "Contact",
                Payload = "{}",
                CrmUser = new CrmUser
                {
                    Id = 2,
                    CrmPlatform = "Salesforce",
                    CrmToken = "valid123"
                }
            };

            var jobs = new List<SyncJob> { job };

            _processor?.ProcessAll(jobs);

            ClassicAssert.AreEqual("Success", job.SyncStatus);
            ClassicAssert.IsNull(job.ErrorMessage);
        }
    }
}
