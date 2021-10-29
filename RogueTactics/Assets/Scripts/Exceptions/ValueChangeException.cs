using System.Collections.Generic;

public class ValueChangeException : BaseException
{
    #region Fields / Properties

    public readonly float fromValue;

    public readonly float toValue;
    
    public float delta
    {
        get { return toValue - fromValue; }
    }

    private List<ValueModifier> _modifiers;

    #endregion

    #region Constructor

    public ValueChangeException(float fromValue, float toValue) : base(true)
    {
        this.fromValue = fromValue;
        this.toValue = toValue;
    }

    #endregion

    #region Public

    public void AddModifier(ValueModifier m)
    {
        if (_modifiers == null)
        {
            _modifiers = new List<ValueModifier>();
        }
        
        _modifiers.Add(m);
    }

    public float GetModifiedValue()
    {
        if (_modifiers == null)
        {
            return toValue;
        }

        float value = toValue;
        _modifiers.Sort(Compare);

        for (int i = 0; i < _modifiers.Count; ++i)
        {
            value = _modifiers[i].Modify(fromValue, value);
        }

        return value;
    }

    #endregion

    #region Private

    int Compare(ValueModifier x, ValueModifier y)
    {
        return x.sortOrder.CompareTo(y.sortOrder);
    }

    #endregion
}
