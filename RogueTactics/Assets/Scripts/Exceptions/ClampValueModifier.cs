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

    public override float Modify(float value)
    {
        return Mathf.Clamp(value, _min, _max);
    }
}
