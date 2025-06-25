using System;
using BatchSyncApp.Models;
using BatchSyncApp.Services;

namespace BatchSyncApp.Services
{
    public class SimpleTokenValidator : ISyncValidator
    {
        public string ObjectType => "TokenValidation";

        public void Handle(SyncJob job)
        {
            if (job.CrmUser == null)
                throw new Exception("CRM user is not attached to the sync job.");

            var token = job.CrmUser.CrmToken;

            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Missing CRM token.");

            // Simulate invalid token logic (e.g., too short)
            if (token.Length < 5)
                throw new Exception("Invalid CRM token.");


        }
    }
}
