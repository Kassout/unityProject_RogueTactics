public class WeaponStatModifierFeature : Feature
{
    #region Fields / Properties

    public WeaponStatTypes type;
    public int amount;

    WeaponStats weaponStats
    {
        get
        {
            return _target.GetComponentInParent<WeaponStats>();
        }
    }
    
    #endregion

    #region Protected

    protected override void OnApply()
    {
        weaponStats[type] += amount;
    }

    protected override void OnRemove()
    {
        weaponStats[type] -= amount;
    }

    #endregion
}