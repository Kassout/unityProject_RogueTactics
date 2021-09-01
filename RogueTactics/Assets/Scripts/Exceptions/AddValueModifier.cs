
public class AddValueModifier : ValueModifier
{
    private readonly float _toAdd;
    
    public AddValueModifier(int sortOrder, float toAdd) : base(sortOrder)
    {
        _toAdd = toAdd;
    }

    public override float Modify(float value)
    {
        return value + _toAdd;
    }
}
