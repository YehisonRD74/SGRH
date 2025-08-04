using System;

public abstract class AuditEntity
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
    public string? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }

    public void SetCreated(string createdBy)
    {
        CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        CreatedAt = DateTime.UtcNow;
    }

    public void SetUpdated(string updatedBy)
    {
        UpdatedBy = updatedBy ?? throw new ArgumentNullException(nameof(updatedBy));
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDeleted(string deletedBy)
    {
        IsDeleted = true;
        DeletedBy = deletedBy ?? throw new ArgumentNullException(nameof(deletedBy));
        DeletedAt = DateTime.UtcNow;
    }
}