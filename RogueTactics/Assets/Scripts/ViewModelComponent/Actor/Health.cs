using UnityEngine;

public class Health : MonoBehaviour
{
    #region Fields

    public int HP
    {
        get { return _unitStats[UnitStatTypes.HP]; }
        set { _unitStats[UnitStatTypes.HP] = value; }
    }

    public int MHP
    {
        get { return _unitStats[UnitStatTypes.MHP]; }
        set { _unitStats[UnitStatTypes.MHP] = value; }
    }

    public int MinHP = 0;
    UnitStats _unitStats;

    #endregion

    #region MonoBehaviour

    void Awake()
    {
        _unitStats = GetComponent<UnitStats>();
    }

    void OnEnable()
    {
        this.AddObserver(OnHPWillChange, UnitStats.WillChangeNotification(UnitStatTypes.HP), _unitStats);
        this.AddObserver(OnMHPDidChange, UnitStats.DidChangeNotification(UnitStatTypes.MHP), _unitStats);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnHPWillChange, UnitStats.WillChangeNotification(UnitStatTypes.HP), _unitStats);
        this.RemoveObserver(OnMHPDidChange, UnitStats.DidChangeNotification(UnitStatTypes.MHP), _unitStats);
    }

    #endregion

    #region Event Handlers

    void OnHPWillChange(object sender, object args)
    {
        ValueChangeException vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, MinHP, _unitStats[UnitStatTypes.MHP]));
    }

    void OnMHPDidChange(object sender, object args)
    {
        int oldMHP = (int)args;
        if (MHP > oldMHP)
            HP += MHP - oldMHP;
        else
            HP = Mathf.Clamp(HP, MinHP, MHP);
    }

    #endregion
}