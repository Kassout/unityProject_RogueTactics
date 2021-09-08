using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    public TileDefinitionData TileDefinition { get; private set; }

    public bool hasEndTurn;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Place(TileDefinitionData targetTile)
    {
        // Make sure old tile location is not still pointing to this unit
        if (TileDefinition != null && TileDefinition.content.Equals(gameObject)) TileDefinition.content = null;

        // Link unit and tile references
        TileDefinition = targetTile;

        if (targetTile != null) TileDefinition.content = gameObject;
    }

    public void Match()
    {
        transform.localPosition = TileDefinition.position;
    }
}