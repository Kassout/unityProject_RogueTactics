using UnityEngine;

public class MinValueModifier : ValueModifier 
{
    private readonly float _min;
    
    public MinValueModifier (int sortOrder, float min) : base (sortOrder)
    {
        _min = min;
    }
  
    public override float Modify (float fromValue, float toValue)
    {
        return Mathf.Min(_min, toValue);
    }
}