using BatchSyncApp.Models;

namespace BatchSyncApp.Services
{
    public class BatchSyncProcessor
    {
        private readonly Dictionary<string, ISyncValidator> _handlers;
        private readonly SimpleTokenValidator _tokenValidator;

        public BatchSyncProcessor(IEnumerable<ISyncValidator> handlers, SimpleTokenValidator tokenValidator)
        {
            // Filter out SimpleTokenValidator from handlers list
            _handlers = handlers
                .Where(h => h.ObjectType != "TokenValidation")
                .ToDictionary(h => h.ObjectType, h => h);

            _tokenValidator = tokenValidator;
        }
        internal void ProcessAll(List<SyncJob> syncJobs)
        {
            foreach (var job in syncJobs)
            {
                try
                {
                    Console.WriteLine($"Processing SyncJob ID: {job.Id} for {job.ObjectType}");

                    _tokenValidator.Handle(job);

                    job.SyncStatus = "Success";
                    job.SyncTime = DateTime.UtcNow;
                    job.ErrorMessage = null;
                }
                catch (Exception ex)
                {
                    job.SyncStatus = "Failed";
                    job.SyncTime = DateTime.UtcNow;
                    job.ErrorMessage = ex.Message;
                    Console.WriteLine($"Error syncing job {job.SyncStatus}: {ex.Message}");
                }
            }
        }
    }
}
