using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityArea : MonoBehaviour
{
    public abstract List<TileDefinitionData> GetTilesInArea(Vector2 position);
}
