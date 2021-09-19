using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAbilityArea : AbilityArea
{
    public override List<WorldTile> GetTilesInArea(Vector2 position)
    {
        AbilityRange ar = GetComponent<AbilityRange>();
        return ar.GetTilesInRange();
    }
}
