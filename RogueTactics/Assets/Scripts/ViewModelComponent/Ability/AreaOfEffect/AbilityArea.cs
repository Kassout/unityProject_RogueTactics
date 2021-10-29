using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityArea : MonoBehaviour
{
    public abstract List<WorldTile> GetTilesInArea(Vector2 position);
}
