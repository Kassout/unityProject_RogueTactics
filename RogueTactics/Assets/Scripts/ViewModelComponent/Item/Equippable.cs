using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equippable : MonoBehaviour
{
    #region Fields

    public EquipSlots defaultSlots;
    public EquipSlots slots;
    protected bool isEquipped;

    #endregion

    #region Public

    public virtual void OnEquip()
    {
        if (isEquipped)
        {
            return;
        }

        isEquipped = true;

        Feature[] features = GetComponentsInChildren<Feature>();
        for (int i = 0; i < features.Length; ++i)
        {
            features[i].Activate(gameObject);
        }
    }

    public virtual void OnUnEquip ()
    {
        if (!isEquipped)
        {
            return;
        }

        isEquipped = false;
        
        Feature[] features = GetComponentsInChildren<Feature>();
        for (int i = 0; i < features.Length; ++i)
        {
            features[i].Deactivate();
        }
    }
    #endregion
}
