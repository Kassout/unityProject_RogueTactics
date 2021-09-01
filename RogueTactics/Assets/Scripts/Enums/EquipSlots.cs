using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum EquipSlots
{
    None = 0,
    Weapon = 1 << 0,
    Cape = 1 << 1,
    Feet = 1 << 2,
    Gauntlet = 1 << 3,
    Helmet = 1 << 4,
    Leg = 1 << 5,
    Torso = 1 << 6,
    Accessory = 1 <<7
}
