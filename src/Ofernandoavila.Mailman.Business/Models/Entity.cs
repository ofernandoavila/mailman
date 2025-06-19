using FluentValidation.Results;

namespace Ofernandoavila.Mailman.Business.Models;

public abstract class Entity
{
    public Guid Id { get; set; }
    public bool Active { get; set; }
    public ValidationResult ValidationResult { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }

    public void Activate()
    {
        Active = true;
    }

    public void Desactivate()
    {
        Active = false;
    }

    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity;

        if(ReferenceEquals(this, compareTo)) return true;
        if(compareTo is null) return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if(a is null && b is null) return true;
        if(a is null || b is null) return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return GetType().Name + " [Id=" + Id + "]";
    }
}