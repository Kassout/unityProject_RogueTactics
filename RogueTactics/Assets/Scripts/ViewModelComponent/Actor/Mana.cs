using UnityEngine;

public class Mana : MonoBehaviour 
{
    #region Fields
    public int MP
    {
        get { return _unitStats[UnitStatTypes.MP]; }
        set { _unitStats[UnitStatTypes.MP] = value; }
    }
  
    public int MMP
    {
        get { return _unitStats[UnitStatTypes.MMP]; }
        set { _unitStats[UnitStatTypes.MMP] = value; }
    }
    Unit unit;
    UnitStats _unitStats;
    #endregion
  
    #region MonoBehaviour
    void Awake ()
    {
        _unitStats = GetComponent<UnitStats>();
        unit = GetComponent<Unit>();
    }
  
    void OnEnable ()
    {
        this.AddObserver(OnMPWillChange, UnitStats.WillChangeNotification(UnitStatTypes.MP), _unitStats);
        this.AddObserver(OnMMPDidChange, UnitStats.DidChangeNotification(UnitStatTypes.MMP), _unitStats);
        this.AddObserver(OnTurnBegan, BattleController.TurnBeganNotification, unit);
    }
  
    void OnDisable ()
    {
        this.RemoveObserver(OnMPWillChange, UnitStats.WillChangeNotification(UnitStatTypes.MP), _unitStats);
        this.RemoveObserver(OnMMPDidChange, UnitStats.DidChangeNotification(UnitStatTypes.MMP), _unitStats);
        this.RemoveObserver(OnTurnBegan, BattleController.TurnBeganNotification, unit);
    }
    #endregion
  
    #region Event Handlers
    void OnMPWillChange (object sender, object args)
    {
        ValueChangeException vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, 0, _unitStats[UnitStatTypes.MHP]));
    }
  
    void OnMMPDidChange (object sender, object args)
    {
        int oldMMP = (int)args;
        if (MMP > oldMMP)
            MP += MMP - oldMMP;
        else
            MP = Mathf.Clamp(MP, 0, MMP);
    }
    void OnTurnBegan (object sender, object args)
    {
        if (MP < MMP)
            MP += Mathf.Max(Mathf.FloorToInt(MMP * 0.1f), 1);
    }
    #endregion
}