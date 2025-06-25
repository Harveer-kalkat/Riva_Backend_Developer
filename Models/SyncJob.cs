namespace BatchSyncApp.Models;

public class SyncJob
{
    public int Id { get; set; } // Sync job ID
    public int CrmUserId { get; set; } // Foreign key to CrmUser

    public string? ObjectType { get; set; } // e.g., "Contact", "Meeting"
    public string? Payload { get; set; }
    public DateTime SyncTime { get; set; }
    public string? SyncStatus { get; set; }
    public string? ErrorMessage { get; set; }

    public CrmUser? CrmUser { get; set; } // Navigation property
}