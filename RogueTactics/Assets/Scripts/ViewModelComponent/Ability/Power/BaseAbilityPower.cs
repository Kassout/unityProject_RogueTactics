using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityPower : MonoBehaviour
{
    protected abstract int GetBaseAttack();
    protected abstract int GetBaseDefense(Unit target);
    protected abstract int GetPower(Unit target);

    private void OnEnable()
    {
        this.AddObserver(OnGetBaseAttack, BaseAbilityEffect.GetStrengthNotification);
        this.AddObserver(OnGetBaseDefense, BaseAbilityEffect.GetDefenseNotification);
        this.AddObserver(OnGetPower, BaseAbilityEffect.GetPowerNotification);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnGetBaseAttack, BaseAbilityEffect.GetStrengthNotification);
        this.RemoveObserver(OnGetBaseDefense, BaseAbilityEffect.GetDefenseNotification);
        this.RemoveObserver(OnGetPower, BaseAbilityEffect.GetPowerNotification);
    }

    private void OnGetBaseAttack(object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetBaseAttack()) );
        }
    }
    
    void OnGetBaseDefense (object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetBaseDefense(info.arg1)) );
        }
    }
    
    void OnGetPower (object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add( new AddValueModifier(0, GetPower(info.arg1)) );
        }
    }
    
    bool IsMyEffect (object sender)
    {
        MonoBehaviour obj = sender as MonoBehaviour;
        return (obj != null && obj.transform.parent == transform);
    }
}
