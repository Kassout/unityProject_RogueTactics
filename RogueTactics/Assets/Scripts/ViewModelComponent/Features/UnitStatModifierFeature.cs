public class UnitStatModifierFeature : Feature
{
    #region Fields / Properties

    public UnitStatTypes type;
    public int amount;

    UnitStats unitStats
    {
        get
        {
            return _target.GetComponentInParent<UnitStats>();
        }
    }
    
    #endregion

    #region Protected

    protected override void OnApply()
    {
        unitStats[type] += amount;
    }

    protected override void OnRemove()
    {
        unitStats[type] -= amount;
    }

    #endregion

}
