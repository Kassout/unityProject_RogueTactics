using System;

public class StatComparisonCondition : StatusCondition
{
    #region Fields

    public UnitStatTypes type { get; private set; }

    public int value { get; private set; }
    
    public Func<bool> condition { get; private set; }

    private UnitStats _unitStats;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _unitStats = GetComponentInParent<UnitStats>();
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnStatChanged, UnitStats.DidChangeNotification(type), _unitStats);
    }

    #endregion

    #region Public

    public void Init(UnitStatTypes type, int value, Func<bool> condition)
    {
        this.type = type;
        this.value = value;
        this.condition = condition;
        this.AddObserver(OnStatChanged, UnitStats.DidChangeNotification(type), _unitStats);
    }

    public bool EqualTo ()
    {
        return _unitStats[type] == value;
    }
  
    public bool LessThan ()
    {
        return _unitStats[type] < value;
    }
  
    public bool LessThanOrEqualTo ()
    {
        return _unitStats[type] <= value;
    }
  
    public bool GreaterThan ()
    {
        return _unitStats[type] > value;
    }
  
    public bool GreaterThanOrEqualTo ()
    {
        return _unitStats[type] >= value;
    }
    #endregion
    
    
    #region Notification Handlers
    
    void OnStatChanged (object sender, object args)
    {
        if (condition != null && !condition())
            Remove();
    }
    
    #endregion
}
