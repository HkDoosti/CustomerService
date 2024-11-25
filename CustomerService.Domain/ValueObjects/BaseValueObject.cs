namespace CustomerService.Domain.ValueObjects;

public abstract class BaseValueObject<TValueObject> : IEquatable<TValueObject>
        where TValueObject : BaseValueObject<TValueObject>
{
    public bool Equals(TValueObject other) => this == other;

    public static bool operator ==(
        BaseValueObject<TValueObject> right, 
        BaseValueObject<TValueObject> left)
    {
        if (right is null && left is null)
            return true;
        if (right is null || left is null)
            return false;
        return right.Equals(left);
    }
    public static bool operator !=(
        BaseValueObject<TValueObject> right, 
        BaseValueObject<TValueObject> left) => !(right == left);


}
