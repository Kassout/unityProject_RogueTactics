using BattleStates;
using Common.StateMachine;
using Model;
using UnityEngine;

public class BattleController : StateMachine
{
    public Camera battleCamera;

    public Transform tileSelectionCursor;

    public Vector2 position;

    public GameObject heroPrefab;
    
    [HideInInspector] public Unit currentUnit;

    public TileDefinitionData currentTile;
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeState<InitBattleState>();
    }
}
