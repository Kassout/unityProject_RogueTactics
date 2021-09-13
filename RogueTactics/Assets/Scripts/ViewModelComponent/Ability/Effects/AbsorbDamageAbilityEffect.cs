using UnityEngine;
using System.Collections;

public class AbsorbDamageAbilityEffect : BaseAbilityEffect
{
    #region Fields

    public int trackedSiblingIndex;
    BaseAbilityEffect effect;
    int amount;

    #endregion

    #region MonoBehaviour

    void Awake()
    {
        effect = GetTrackedEffect();
    }

    void OnEnable()
    {
        this.AddObserver(OnEffectHit, HitNotification, effect);
        this.AddObserver(OnEffectMiss, MissedNotification, effect);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnEffectHit, HitNotification, effect);
        this.RemoveObserver(OnEffectMiss, MissedNotification, effect);
    }

    #endregion

    #region Base Ability Effect

    public override int Predict(TileDefinitionData target)
    {
        return 0;
    }

    protected override int OnApply(TileDefinitionData target)
    {
        UnitStats s = GetComponentInParent<UnitStats>();
        s[UnitStatTypes.HP] += amount;
        return amount;
    }

    #endregion

    #region Event Handlers

    void OnEffectHit(object sender, object args)
    {
        amount = (int)args;
    }

    void OnEffectMiss(object sender, object args)
    {
        amount = 0;
    }

    #endregion

    #region Private

    BaseAbilityEffect GetTrackedEffect()
    {
        Transform owner = GetComponentInParent<Ability>().transform;
        if (trackedSiblingIndex >= 0 && trackedSiblingIndex < owner.childCount)
        {
            Transform sibling = owner.GetChild(trackedSiblingIndex);
            return sibling.GetComponent<BaseAbilityEffect>();
        }

        return null;
    }

    #endregion
}