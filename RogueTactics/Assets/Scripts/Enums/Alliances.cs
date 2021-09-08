using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Alliances
{
    None = 0,
    Neutral = 1 << 0,
    Hero = 1 << 1,
    Enemy = 1 << 2
}
