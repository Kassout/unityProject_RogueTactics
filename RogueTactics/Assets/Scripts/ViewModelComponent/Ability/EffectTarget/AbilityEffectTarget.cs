using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffectTarget : MonoBehaviour
{
    public abstract bool IsTarget(TileDefinitionData tile);
}
