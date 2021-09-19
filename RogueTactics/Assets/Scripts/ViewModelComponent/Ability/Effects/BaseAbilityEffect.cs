using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseAbilityEffect : MonoBehaviour
{
    #region Consts & Notifications

    protected const int minDamage = -99;
    protected const int maxDamage = 99;

    public const string GetOffensiveStatNotification = "BaseAbilityEffect.GetStrengthNotification";
    public const string GetDefensiveStatNotification = "BaseAbilityEffect.GetDefenseNotification";
    public const string GetAbilityPowerNotification = "BaseAbilityEffect.GetPowerNotification";

    public const string TweakDamageNotification = "BaseAbilityEffect.TweakDamageNotification";
    public const string MissedNotification = "BaseAbilityEffect.MissedNotification";
    public const string HitNotification = "BaseAbilityEffect.HitNotification";

    #endregion

    #region Public

    public abstract int Predict(WorldTile target);

    public void Apply(WorldTile target)
    {
        if (GetComponent<AbilityEffectTarget>().IsTarget(target) == false)
            return;

        if (GetComponent<HitRate>().RollForHit(target))
            this.PostNotification(HitNotification, OnApply(target));
        else
            this.PostNotification(MissedNotification);
    }

    #endregion

    #region Protected

    protected abstract int OnApply(WorldTile target);

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
        retValue = Mathf.Clamp(retValue, minDamage, maxDamage);
        return retValue;
    }

    int Compare(ValueModifier x, ValueModifier y)
    {
        return x.sortOrder.CompareTo(y.sortOrder);
    }

    #endregion
}