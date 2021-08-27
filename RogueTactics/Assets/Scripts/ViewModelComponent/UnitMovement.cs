using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class UnitMovement : MonoBehaviour
{
    public int range;
    public int jumpHeight;
    protected Unit unit;
    protected Transform jumper;

    protected virtual void Awake ()
    {
        unit = GetComponent<Unit>();
        jumper = transform.Find("Jumper");
    }

    public virtual List<Tile> GetTilesInRange (Board board)
    {
/*         List<Tile> retValue = board.Search(unit.tile, range, ExpandSearch);
        Filter(retValue); */
        List<Tile> retValue = new List<Tile>();
        return retValue;
    }

    protected virtual bool ExpandSearch (Tile from, Tile to)
    {
        Vector3 fromPosition = Board.boardMap.CellToWorld(Vector3Int.FloorToInt(from.gameObject.transform.position));
        Vector3 toPosition = Board.boardMap.CellToWorld(Vector3Int.FloorToInt(to.gameObject.transform.position));
        return (fromPosition.x - toPosition.x) + (fromPosition.y - toPosition.y) <= range;
    }

    protected virtual void Filter (List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            if (tiles[i].sprite != null)
            {
                tiles.RemoveAt(i);
            }
        }
    }

    public abstract IEnumerator Traverse(Tile tile);
}
