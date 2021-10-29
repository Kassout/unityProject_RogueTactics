using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityPower : MonoBehaviour
{
    protected abstract int GetBaseOffensiveStat();
    protected abstract int GetBaseDefensiveStat(Unit target);
    protected abstract int GetAbilityPower(Unit target);

    private void OnEnable()
    {
        this.AddObserver(OnGetBaseOffensiveStat, BaseAbilityEffect.GetOffensiveStatNotification);
        this.AddObserver(OnGetBaseDefensiveStat, BaseAbilityEffect.GetDefensiveStatNotification);
        this.AddObserver(OnGetAbilityPower, BaseAbilityEffect.GetAbilityPowerNotification);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnGetBaseOffensiveStat, BaseAbilityEffect.GetOffensiveStatNotification);
        this.RemoveObserver(OnGetBaseDefensiveStat, BaseAbilityEffect.GetDefensiveStatNotification);
        this.RemoveObserver(OnGetAbilityPower, BaseAbilityEffect.GetAbilityPowerNotification);
    }

    private void OnGetBaseOffensiveStat(object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetBaseOffensiveStat()) );
        }
    }
    
    void OnGetBaseDefensiveStat (object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetBaseDefensiveStat(info.arg1)) );
        }
    }
    
    void OnGetAbilityPower (object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetAbilityPower(info.arg1)) );
        }
    }
    
    bool IsMyEffect (object sender)
    {
        MonoBehaviour obj = sender as MonoBehaviour;
        return (obj != null && obj.transform.parent == transform);
    }
}
