using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    public WorldTile tile { get; private set; }

    public bool hasEndTurn;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Place(WorldTile targetWorldTile)
    {
        // Make sure old tile location is not still pointing to this unit
        if (tile != null && tile.content.Equals(gameObject)) tile.content = null;

        // Link unit and tile references
        tile = targetWorldTile;

        if (targetWorldTile != null) tile.content = gameObject;
    }

    public void Match()
    {
        transform.localPosition = tile.position;
    }
}