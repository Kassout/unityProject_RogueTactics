using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public abstract class AbilityRange : MonoBehaviour
{
    public int horizontal = 1;
    
    public virtual bool positionOriented => true;

    protected Unit unit
    {
        get { return GetComponentInParent<Unit>(); }
    }

    public abstract List<TileDefinitionData> GetTilesInRange();
    
    public abstract List<TileDefinitionData> GetTilesInRange(List<TileDefinitionData> movementRadiusTiles);

}
