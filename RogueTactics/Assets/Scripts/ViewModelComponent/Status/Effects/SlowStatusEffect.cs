public class SlowStatusEffect : StatusEffect
{
    private UnitStats _myUnitStats;
    
    void OnEnable ()
    {
        _myUnitStats = GetComponentInParent<UnitStats>();
        if (_myUnitStats)
            this.AddObserver(OnAddedStatus, Status.AddedNotification, this);
    }
  
    void OnDisable ()
    {
        this.RemoveObserver(OnAddedStatus, Status.AddedNotification, this);
    }
  
    void OnAddedStatus(object sender, object args)
    {
        int currentMov = _myUnitStats[UnitStatTypes.MOV];
        _myUnitStats.SetValue(UnitStatTypes.MOV, (currentMov - 1), false);
    }
}
