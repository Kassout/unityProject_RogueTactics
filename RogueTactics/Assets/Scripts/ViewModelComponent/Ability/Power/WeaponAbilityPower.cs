public class WeaponAbilityPower : BaseAbilityPower
{
    private Weapons _weaponsCat;
    
    protected override int GetBaseOffensiveStat()
    {
        return GetComponentInParent<UnitStats>()[UnitStatTypes.STR];
    }

    protected override int GetBaseDefensiveStat(Unit target)
    {
        return target.GetComponent<UnitStats>()[UnitStatTypes.DEF];
    }
    protected override int GetAbilityPower(Unit target)
    {
        int power = GetComponentInParent<WeaponStats>()[WeaponStatTypes.POW];
        
        Equipment eq = GetComponentInParent<Equipment>();
        Weapon weapon = (Weapon)eq.GetItem(EquipSlots.Weapon);
            
        Equipment targetEq = target.GetComponentInChildren<Equipment>();
        Weapon targetWeapon = (Weapon) targetEq.GetItem(EquipSlots.Weapon);

        if (weapon.weaponCat != Weapons.None && targetWeapon.weaponCat != Weapons.None)
        {
            power += CheckWeaponTriangleBonus(weapon, targetWeapon);
        }
        
        return power;
    }

    int CheckWeaponTriangleBonus(Weapon attackerWeapon, Weapon defenderWeapon)
    {
        return attackerWeapon.CheckWeaponAdvantage(defenderWeapon);
    }
}