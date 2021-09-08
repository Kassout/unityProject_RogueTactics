using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityPower : MonoBehaviour
{
    protected abstract int GetBaseAttack();
    protected abstract int GetBaseDefense(Unit target);
    protected abstract int GetPower();

    private void OnEnable()
    {
        this.AddObserver(OnGetBaseAttack, DamageAbilityEffect.GetAttackNotification);
        this.AddObserver(OnGetBaseDefense, DamageAbilityEffect.GetDefenseNotification);
        this.AddObserver(OnGetPower, DamageAbilityEffect.GetPowerNotification);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnGetBaseAttack, DamageAbilityEffect.GetAttackNotification);
        this.RemoveObserver(OnGetBaseDefense, DamageAbilityEffect.GetDefenseNotification);
        this.RemoveObserver(OnGetPower, DamageAbilityEffect.GetPowerNotification);
    }

    private void OnGetBaseAttack(object sender, object args)
    {
        var info = args as Info<Unit, Unit, List<ValueModifier>>;
        
        if (info.arg0 != GetComponentInParent<Unit>())
            return;
        AddValueModifier mod = new AddValueModifier(0, GetBaseAttack());
        info.arg2.Add(mod);
    }
    
    void OnGetBaseDefense (object sender, object args)
    {
        var info = args as Info<Unit, Unit, List<ValueModifier>>;
        if (info.arg0 != GetComponentInParent<Unit>())
            return;
    
        AddValueModifier mod = new AddValueModifier(0, GetBaseDefense(info.arg1));
        info.arg2.Add( mod );
    }
    
    void OnGetPower (object sender, object args)
    {
        var info = args as Info<Unit, Unit, List<ValueModifier>>;
        if (info.arg0 != GetComponentInParent<Unit>())
            return;
    
        AddValueModifier mod = new AddValueModifier(0, GetPower());
        info.arg2.Add( mod );
    }
}
