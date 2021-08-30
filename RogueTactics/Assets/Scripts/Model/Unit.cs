using UnityEngine;

namespace Model
{
    public class Unit : MonoBehaviour
    {
        public TileDefinitionData TileDefinition { get; private set; }

        public void Place(TileDefinitionData targetTile)
        {
            // Make sure old tile location is not still pointing to this unit
            if (TileDefinition != null && TileDefinition.content.Equals(gameObject)) 
            {
                TileDefinition.content = null;
            }
    
            // Link unit and tile references
            TileDefinition = targetTile;

            if (targetTile != null)
            {
                TileDefinition.content = gameObject;
            }
        }

        public void Match()
        {
            transform.localPosition = TileDefinition.position;
        }
    }
}
