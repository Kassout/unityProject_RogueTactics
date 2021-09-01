using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equippable : MonoBehaviour
{
    #region Fields

    public EquipSlots defaultSlots;
    public EquipSlots secondarySlots;
    public EquipSlots slots;
    private bool _isEquipped;

    #endregion

    #region Public

    public void OnEquip()
    {
        if (_isEquipped)
        {
            return;
        }

        _isEquipped = true;

        Feature[] features = GetComponentsInChildren<Feature>();
        for (int i = 0; i < features.Length; ++i)
        {
            features[i].Activate(gameObject);
        }
    }

    public void OnUnEquip ()
    {
        if (!_isEquipped)
        {
            return;
        }

        _isEquipped = false;
        
        Feature[] features = GetComponentsInChildren<Feature>();
        for (int i = 0; i < features.Length; ++i)
        {
            features[i].Deactivate();
        }
    }
    #endregion
}
