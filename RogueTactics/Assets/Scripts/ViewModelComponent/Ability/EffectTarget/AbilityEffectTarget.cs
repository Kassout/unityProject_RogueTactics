using UnityEngine;

public abstract class AbilityEffectTarget : MonoBehaviour
{
    public abstract bool IsTarget(WorldTile worldTile);
}
