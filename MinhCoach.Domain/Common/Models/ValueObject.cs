namespace MinhCoach.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    //IEquatable cung cấp các loại tuỳ chỉnh để kiểm tra sự bằng nhau của hai object
    //ValueObject là lớp có sỡ trừu tượng cho các đối tượng giá trị => các object không có danh tính
    //và so sanh dựa trên giá trị thay cho danh tính
    public abstract IEnumerable<object> GetEqualityComponents();
    //phương thức của IEquatable<ValueObject>  không cần kiểm kiểu vì tham số đã đủ xác định rồi
    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }
    public override bool Equals(object? obj)
    {
        if(obj is null || obj.GetType() != GetType())
        {
            return false;
        }
        
        var valueObject = (ValueObject)obj;
        
        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject left, ValueObject right) 
    { 
        return Equals(left, right);
    }
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !Equals(left, right);
    }

    
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
    
}