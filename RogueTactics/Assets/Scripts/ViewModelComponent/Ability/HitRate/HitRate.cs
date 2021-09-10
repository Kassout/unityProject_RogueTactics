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

    #endregion
}
