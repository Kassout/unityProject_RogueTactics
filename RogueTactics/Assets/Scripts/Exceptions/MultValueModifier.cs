public class MultValueModifier : ValueModifier 
{
    private readonly float _toMultiply;
    
    public MultValueModifier (int sortOrder, float toMultiply) : base (sortOrder)
    {
        _toMultiply = toMultiply;
    }
    
    public override float Modify (float value)
    {
        return value * _toMultiply;
    }
}