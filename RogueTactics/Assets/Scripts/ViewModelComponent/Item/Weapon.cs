using UnityEngine;

public sealed class Weapon : Equippable, IStorable
{
    #region Fields / Properties

    public Weapons weaponCat;
    
    public static readonly WeaponStatTypes[] StatOrder = new WeaponStatTypes[]
    {
        WeaponStatTypes.RAN,
        WeaponStatTypes.WEI,
        WeaponStatTypes.POW,
        WeaponStatTypes.HIT,
        WeaponStatTypes.CRI
    };
    
    public int[] baseStats = new int[StatOrder.Length];

    public string weaponLevel;

    public int weaponDurability;
    
    public Sprite weaponModel;
    
    private WeaponStats _weaponStats;
    
    #endregion

    private void OnDestroy()
    {
        transform.parent.parent.GetComponentInChildren<Inventory>().UnloadObject(gameObject);
    }

    public override void OnEquip()
    {
        if (isEquipped)
        {
            return;
        }

        isEquipped = true;

        Transform attackAbility = transform.parent.Find("Attack");
        
        transform.SetParent(attackAbility);
        name = gameObject.name;
        _weaponStats = GetComponentInParent<WeaponStats>();
        LoadWeaponStats();

        Feature[] features = GetComponentsInChildren<Feature>();
        for (int i = 0; i < features.Length; ++i)
        {
            features[i].Activate(gameObject);
        }
    }
    
    public override void OnUnEquip()
    {
        base.OnUnEquip();
        
        Transform attackAbility = transform.parent;
        Destroy(attackAbility.GetChild(0).gameObject);
    }
    
    
    
    public void LoadWeaponStats()
    {
        for (int i = 0; i < StatOrder.Length; ++i)
        {
            WeaponStatTypes type = StatOrder[i];
            _weaponStats.SetValue(type, baseStats[i], false);
        }
    }

    public int CheckWeaponAdvantage(Weapon targetWeapon)
    {
        var effectiveness = (int) typeof(Weapon).GetMethod("Check" + weaponCat.ToString() + "Advantage")
            ?.Invoke(null, new[] { targetWeapon });

        return effectiveness;
    }

    private int CheckSwordAdvantage(Weapon targetWeapon)
    {
        if (targetWeapon.weaponCat == Weapons.Spear || targetWeapon.weaponCat == Weapons.Staff)
        {
            return -1;
        } 
        else if (targetWeapon.weaponCat == Weapons.Axe || targetWeapon.weaponCat == Weapons.Mace)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    
    private int CheckDaggerAdvantage(Weapon targetWeapon)
    {
        if (targetWeapon.weaponCat == Weapons.Spear || targetWeapon.weaponCat == Weapons.Staff)
        {
            return -1;
        } 
        else if (targetWeapon.weaponCat == Weapons.Axe || targetWeapon.weaponCat == Weapons.Mace)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    
    private int CheckSpearAdvantage(Weapon targetWeapon)
    {
        if (targetWeapon.weaponCat == Weapons.Axe || targetWeapon.weaponCat == Weapons.Mace)
        {
            return -1;
        } 
        else if (targetWeapon.weaponCat == Weapons.Sword || targetWeapon.weaponCat == Weapons.Dagger)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    
    private int CheckStaffAdvantage(Weapon targetWeapon)
    {
        if (targetWeapon.weaponCat == Weapons.Axe || targetWeapon.weaponCat == Weapons.Mace)
        {
            return -1;
        } 
        else if (targetWeapon.weaponCat == Weapons.Sword || targetWeapon.weaponCat == Weapons.Dagger)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    
    private int CheckAxeAdvantage(Weapon targetWeapon)
    {
        if (targetWeapon.weaponCat == Weapons.Sword || targetWeapon.weaponCat == Weapons.Dagger)
        {
            return -1;
        } 
        else if (targetWeapon.weaponCat == Weapons.Spear || targetWeapon.weaponCat == Weapons.Staff)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    
    private int CheckMaceAdvantage(Weapon targetWeapon)
    {
        if (targetWeapon.weaponCat == Weapons.Sword || targetWeapon.weaponCat == Weapons.Dagger)
        {
            return -1;
        } 
        else if (targetWeapon.weaponCat == Weapons.Spear || targetWeapon.weaponCat == Weapons.Staff)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
