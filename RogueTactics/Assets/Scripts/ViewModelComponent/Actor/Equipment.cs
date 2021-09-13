using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static GameObject defaultWeapon;
    private bool _inTransition;
    
    #region Notifications

    public const string EquippedNotification = "Equipment.EquippedNotification";
    public const string UnEquippedNotification = "Equipment.UnEquippedNotification";

    #endregion

    private void Start()
    {
        if (defaultWeapon == null)
        {
            defaultWeapon = Resources.Load<GameObject>("Weapons/Default/Fist (Unarmed)");
            defaultWeapon.name = "Fist (Unarmed)";
        }

        GameObject weapon = Instantiate(defaultWeapon, transform, true);
        weapon.name = defaultWeapon.name;
        Equip(weapon.GetComponent<Weapon>(), EquipSlots.Weapon);
    }

    private void OnEnable()
    {
       this.AddObserver(OnUnEquippedWeapon, UnEquippedNotification, this);
    }
    
    private void OnUnEquippedWeapon(object arg1, object arg2)
    {
        if (!GetItem(EquipSlots.Weapon) && !_inTransition)
        {
            GameObject weapon = Instantiate(defaultWeapon, transform, true);
            weapon.name = defaultWeapon.name;
            Equip(weapon.GetComponent<Weapon>(), EquipSlots.Weapon);
        }
    }

    #region Fields / Properties

    public IList<Equippable> items
    {
        get { return _items.AsReadOnly(); }
    }

    private List<Equippable> _items = new List<Equippable>();

    #endregion

    #region Public

    public void Equip(Equippable item, EquipSlots slots)
    {
        _inTransition = true;
        
        if (GetItem(slots))
        {
            UnEquip(slots);
        }
        
        _items.Add(item);
        item.transform.SetParent(transform);
        item.slots = slots;
        item.OnEquip();

        _inTransition = false;
        this.PostNotification(EquippedNotification, item);
    }
    
    public void UnEquip (Equippable item)
    {
        item.OnUnEquip();
        item.slots = EquipSlots.None;
        _items.Remove(item);
        this.PostNotification(UnEquippedNotification, item);
    }
  
    public void UnEquip (EquipSlots slots)
    {
        for (int i = _items.Count - 1; i >= 0; --i)
        {
            Equippable item = _items[i];
            if ((item.slots & slots) != EquipSlots.None)
            {
                UnEquip(item);
            }
        }
    }
    
    public Equippable GetItem (EquipSlots slots)
    {
        for (int i = _items.Count - 1; i >= 0; --i)
        {
            Equippable item = _items[i];
            if ( (item.slots & slots) != EquipSlots.None )
                return item;
        }
        return null;
    }
    
    #endregion
}
