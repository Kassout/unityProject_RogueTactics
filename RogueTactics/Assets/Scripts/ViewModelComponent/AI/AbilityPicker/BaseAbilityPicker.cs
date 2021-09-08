using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityPicker : MonoBehaviour
{
    #region Fields

    protected Unit owner;

    protected AbilityCatalog ac;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        owner = GetComponentInParent<Unit>();
        ac = owner.GetComponentInChildren<AbilityCatalog>();
    }

    #endregion

    #region Public

    public abstract void Pick(PlanOfAttack plan);

    #endregion

    #region Protected

    protected Ability Find(string abilityName)
    {
        for (int i = 0; i < ac.transform.childCount; ++i)
        {
            Transform category = ac.transform.GetChild(i);
            Transform child = category.Find(abilityName);
            if (child != null)
            {
                return child.GetComponent<Ability>();
            }
        }

        return null;
    }

    protected Ability Default()
    {
        return owner.GetComponentInChildren<Ability>();
    }

    #endregion
}
