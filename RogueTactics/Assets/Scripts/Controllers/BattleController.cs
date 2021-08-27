using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleController : StateMachine
{
    public Camera battleCamera;

    public Transform tileSelectionCursor;

    public Vector2 position;

    public GameObject heroPrefab;
    public Unit currentUnit;

    public Board board;
    public Tile currentTile { get { return Board.boardMap.GetTile<Tile>(Vector3Int.FloorToInt(position)); }}

    // Start is called before the first frame update
    void Start()
    {
        ChangeState<InitBattleState>();
    }

    
}
