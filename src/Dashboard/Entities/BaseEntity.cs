namespace Dashboard.Entities;

public class BaseEntity
{
    private Guid _id;

    public Guid id {
        get => _id; 
        set => _id = value == default ? Guid.NewGuid() : value; 
    }

    private DateTime _created_at;

    public DateTime created_at { 
        get => _created_at;
        set => _created_at = value == default ? DateTime.UtcNow : value;
    }

    private DateTime _updated_at;

    public DateTime updated_at { 
        get => _updated_at;
        set => _updated_at = DateTime.UtcNow;
    }
}