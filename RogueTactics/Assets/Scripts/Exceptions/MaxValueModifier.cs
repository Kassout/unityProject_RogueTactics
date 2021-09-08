using UnityEngine;

public class MaxValueModifier : ValueModifier 
{
    private readonly float _max;
    
    public MaxValueModifier (int sortOrder, float max) : base (sortOrder)
    {
        _max = max;
    }
    
    public override float Modify (float fromValue, float toValue)
    {
        return Mathf.Max(toValue, _max);
    }
}