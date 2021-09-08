using UnityEngine;

public class ClampValueModifier : ValueModifier
{
    private readonly float _min;
    private readonly float _max;
    
    public ClampValueModifier(int sortOrder, float min, float max) : base(sortOrder)
    {
        _min = min;
        _max = max;
    }

    public override float Modify(float fromValue, float toValue)
    {
        return Mathf.Clamp(toValue, _min, _max);
    }
}
