using System.Collections.Generic;
using BattleStates;
using Common.StateMachine;
using Model;
using UnityEngine;

/// <summary>
///     TODO: comments
/// </summary>
public class BattleController : StateMachine
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public Camera battleCamera;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public Transform tileSelectionCursor;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public GameObject heroPrefab;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public AbilityMenuPanelController abilityMenuPanelController;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public Turn turn = new Turn();

    /// <summary>
    /// TODO: comments
    /// </summary>
    public List<Unit> units = new List<Unit>();

    /// <summary>
    ///     TODO: comments
    /// </summary>
    [HideInInspector] public TileDefinitionData currentSelectedTile;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    [HideInInspector] public Vector2 position;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private void Start()
    {
        ChangeState<InitBattleState>();
    }
}