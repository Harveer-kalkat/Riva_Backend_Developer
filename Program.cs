using BatchSyncApp.Services;
using BatchSyncApp.Models;
using Microsoft.Extensions.DependencyInjection;


// 1. Set up DI container
var services = new ServiceCollection();

// 2. Register sync handlers
services.AddSingleton<SimpleTokenValidator>();

// 3. Register the processor
services.AddSingleton<BatchSyncProcessor>();

// 4. Build the service provider
var provider = services.BuildServiceProvider();

// 5. Create test data
var user = new CrmUser
{
    Id = 1,
    Email = "jdoe@email.com",
    CrmPlatform = "Salesforce",
    CrmToken = "token123"
};

var jobs = new List<SyncJob>
        {
            new SyncJob
            {
                CrmUser = new CrmUser { Id = 2, Email = "alice@example.com", CrmToken = "token123" },
                Id = 201,
                ObjectType = "Contact",
                Payload = "{ \"email\": \"alice@example.com\", \"name\": \"Alice\" }",
            },
            // new SyncJob { Id = 2, CrmUser = user, ObjectType = "Meeting", Payload = "{}" },
            // new SyncJob { Id = 3, CrmUser = user, ObjectType = "Note", Payload = "{}" }, // unsupported
        };

// 6. Run the processor
var validator = new SimpleTokenValidator();
var processor = provider.GetRequiredService<BatchSyncProcessor>();
processor.ProcessAll(jobs);

// 7. Print result
foreach (var job in jobs)
{
    Console.WriteLine($"Job {job.Id}: {job.SyncStatus}, {job.ErrorMessage}");
}