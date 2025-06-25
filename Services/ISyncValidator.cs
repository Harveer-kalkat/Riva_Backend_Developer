using BatchSyncApp.Models;

namespace BatchSyncApp.Services
{
    public interface ISyncValidator
    {
        string ObjectType { get; }
        void Handle(SyncJob job);

    }
}