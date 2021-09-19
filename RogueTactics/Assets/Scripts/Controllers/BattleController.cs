using System;
using System.Collections.Generic;
using Common.StateMachine;
using Model;
using UnityEngine;

/// <summary>
///     TODO: comments
/// </summary>
public class BattleController : StateMachine
{
    public static BattleController Instance { get; private set; }
    
    #region Notifications
    
    public const string BattleBeganNotification = "BattleController.roundBegan";
    
    public const string TurnBeganNotification = "BattleController.turnBegan";
    
    public const string TurnCompletedNotification = "BattleController.turnCompleted";
    
    public const string BattleEndedNotification = "BattleController.roundEnded";
    
    #endregion
    
    /// <summary>
    ///     TODO: comments
    /// </summary>
    public Camera battleCamera;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    public Transform tileSelectionCursor;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public AbilityMenuPanelController abilityMenuPanelController;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public StatPanelController statPanelController;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public BattleMessageController battleMessageController;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public ComputerPlayer cpu;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public HitSuccessIndicator hitSuccessIndicator;

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
    [HideInInspector] public WorldTile currentSelectedWorldTile;

    /// <summary>
    ///     TODO: comments
    /// </summary>
    [HideInInspector] public Vector2 position;

    [HideInInspector] public bool inTransition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    private void Start()
    {
        ChangeState<InitBattleState>();
    }
}