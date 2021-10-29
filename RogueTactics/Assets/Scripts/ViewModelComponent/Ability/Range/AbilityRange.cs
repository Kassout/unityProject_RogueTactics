using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityRange : MonoBehaviour
{
    public int range = 1;
    
    public virtual bool positionOriented => true;

    protected Unit unit
    {
        get { return GetComponentInParent<Unit>(); }
    }

    public abstract List<WorldTile> GetTilesInRange();
    
    public abstract List<WorldTile> GetTilesInRange(List<WorldTile> movementRadiusTiles);

}
