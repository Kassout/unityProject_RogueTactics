using System.Collections.Generic;
using UnityEngine;

public abstract class HitRate : MonoBehaviour
{
    #region Notifications

    /// <summary>
    /// Includes a toggleable MatchException argument which defaults to false.
    /// </summary>
    public const string AutomaticHitCheckNotification = "HitRate.AutomaticHitCheckNotification";

    /// <summary>
    /// Includes a toggleable MatchException argument which defaults to false.
    /// </summary>
    public const string AutomaticMissChechNotification = "HitRate.AutomaticMissCheckNotification";

    /// <summary>
    /// Includes an Info argument with three parameters: Attacker (Unit), Defender (Unit), and Defender's calculated Evade / Resistance (int).
    /// Status effects which modify Hit Rate should modify the arg2 parameter.
    /// </summary>
    public const string StatusCheckNotification = "HitRate.StatusCheckNotification";
    
    public const string GetSkillNotification = "HitRate.GetSkillNotification";
    public const string GetLuckNotification = "HitRate.GetLuckNotification";
    public const string GetHitRateNotification = "HitRate.GetHitRateNotification";
    public const string GetSpeedNotification = "HitRate.GetSpeedNotification";
    public const string GetEvadeNotification = "HitRate.GetEvadeNotification";

    #endregion

    #region Fields
    
    protected Unit attacker;

    #endregion

    #region MonoBehaviour

    protected void Start()
    {
        attacker = GetComponentInParent<Unit>();
    }

    #endregion

    #region Public

    /// <summary>
    /// Returns a value in the range of 0 to 100 as a percent chance of an ability succeeding to hit
    /// </summary>
    /// <param name="attacker">TODO: comments</param>
    /// <param name="target">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public abstract int Calculate(TileDefinitionData target);

    public virtual bool RollForHit(TileDefinitionData target)
    {
        int roll = Random.Range(0, 101);
        int chance = Calculate(target);
        return roll <= chance;
    }

    #endregion

    #region Protected

    protected virtual bool AutomaticHit(Unit target)
    {
        MatchException exc = new MatchException(attacker, target);
        this.PostNotification(AutomaticHitCheckNotification, exc);
        return exc.toggle;
    }

    protected virtual bool AutomaticMiss(Unit target)
    {
        MatchException exc = new MatchException(attacker, target);
        this.PostNotification(AutomaticMissChechNotification, exc);
        return exc.toggle;
    }

    protected virtual int AdjustForStatusEffects(Unit target, int rate)
    {
        Info<Unit, Unit, int> args = new Info<Unit, Unit, int>(attacker, target, rate);
        this.PostNotification(StatusCheckNotification, args);
        return args.arg2;
    }

    protected virtual int Final(int evade)
    {
        return 100 - evade;
    }
    
    protected virtual int GetStat(Unit attacker, Unit target, string notification, int startValue)
    {
        var mods = new List<ValueModifier>();
        var info = new Info<Unit, Unit, List<ValueModifier>>(attacker, target, mods);
        this.PostNotification(notification, info);
        mods.Sort(Compare);

        float value = startValue;
        for (int i = 0; i < mods.Count; ++i)
            value = mods[i].Modify(startValue, value);

        int retValue = Mathf.FloorToInt(value);
        retValue = Mathf.Clamp(retValue, 0, 100);
        return retValue;
    }

    int Compare(ValueModifier x, ValueModifier y)
    {
        return x.sortOrder.CompareTo(y.sortOrder);
    }

    #endregion

    protected int GetBaseSkill()
    {
        return GetComponentInParent<UnitStats>()[UnitStatTypes.SKL];
    }

    protected int GetBaseLuck()
    {
        return GetComponentInParent<UnitStats>()[UnitStatTypes.LUC];
    }

    protected int GetBaseSpeed()
    {
        return GetComponentInParent<UnitStats>()[UnitStatTypes.SPD];
    }

    protected int GetBaseEvade(Unit target)
    {
        return target.GetComponent<UnitStats>()[UnitStatTypes.EVD];
    }

    protected virtual int GetHitRate()
    {
        return 95;
    }
    
    private void OnEnable()
    {
        this.AddObserver(OnGetBaseSkill, GetSkillNotification);
        this.AddObserver(OnGetBaseLuck, GetLuckNotification);
        this.AddObserver(OnGetBaseSpeed, GetSpeedNotification);
        this.AddObserver(OnGetHitRate, GetHitRateNotification);
        this.AddObserver(OnGetBaseEvade, GetEvadeNotification);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnGetBaseSkill, GetSkillNotification);
        this.RemoveObserver(OnGetBaseLuck, GetLuckNotification);
        this.RemoveObserver(OnGetBaseSpeed, GetSpeedNotification);
        this.RemoveObserver(OnGetHitRate, GetHitRateNotification);
        this.RemoveObserver(OnGetBaseEvade, GetEvadeNotification);
    }
    
    private void OnGetBaseSkill(object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetBaseSkill()) );
        }
    }
    
    void OnGetBaseLuck (object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetBaseLuck()) );
        }
    }
    
    void OnGetBaseSpeed (object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetBaseSpeed()) );
        }
    }
    
    void OnGetBaseEvade (object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetBaseEvade(info.arg1)) );
        }
    }
    
    void OnGetHitRate (object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetHitRate()) );
        }
    }
    
    bool IsMyEffect (object sender)
    {
        MonoBehaviour obj = sender as MonoBehaviour;
        return (obj != null && obj.transform == transform);
    }
}
