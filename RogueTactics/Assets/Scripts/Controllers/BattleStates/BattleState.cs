using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class BattleState : State
{
    protected BattleController owner;

    protected InputManager inputManager;

    public Board board { get { return owner.board; }}

    public Camera battleCamera { get { return owner.battleCamera; }}

    public Transform tileSelectionCursor { get { return owner.tileSelectionCursor; }}

    public Vector2 position { get { return owner.position; }}

    protected virtual void Awake() 
    {
        owner = GetComponent<BattleController>();
        inputManager = new InputManager();
    }

    protected override void AddListeners()
    {
        inputManager.Cursor.Movement.performed += OnMovement;
        inputManager.Cursor.Movement.Enable();

        inputManager.Cursor.Interaction.performed += OnInteraction;
        inputManager.Cursor.Interaction.Enable();
    }

    protected override void RemoveListeners()
    {
        inputManager.Cursor.Movement.performed -= OnMovement;
        inputManager.Cursor.Movement.Disable();

        inputManager.Cursor.Interaction.performed -= OnInteraction;
        inputManager.Cursor.Interaction.Disable();
    }

    protected virtual void OnMovement(InputAction.CallbackContext context)
    {
    }

    protected virtual void OnInteraction(InputAction.CallbackContext context)
    {
        
    }
}
