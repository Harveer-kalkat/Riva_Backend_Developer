namespace BatchSyncApp.Models;

public class CrmUser
{
    public int Id { get; set; } // User ID
    public string? Email { get; set; }
    public string? CrmPlatform { get; set; }
    public string? CrmToken { get; set; }

}
