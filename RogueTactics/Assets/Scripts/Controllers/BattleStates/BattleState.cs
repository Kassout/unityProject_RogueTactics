using Model;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace BattleStates
{
    public class BattleState : State
    {
        protected BattleController owner;

        private InputManager inputManager;

        protected Camera battleCamera => owner.battleCamera;

        protected Transform tileSelectionCursor => owner.tileSelectionCursor;

        protected TileDefinitionData currentTile => owner.currentTile;

        private Vector2 position
        {
            get => owner.position;
            set => owner.position = value;
        }

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
            
            inputManager.Cursor.Cancel.performed += OnCancel;
            inputManager.Cursor.Cancel.Enable();
        }

        protected override void RemoveListeners()
        {
            inputManager.Cursor.Movement.performed -= OnMovement;
            inputManager.Cursor.Movement.Disable();

            inputManager.Cursor.Interaction.performed -= OnInteraction;
            inputManager.Cursor.Interaction.Disable();
            
            inputManager.Cursor.Cancel.performed -= OnCancel;
            inputManager.Cursor.Cancel.Disable();
        }

        protected virtual void OnMovement(InputAction.CallbackContext context)
        {
        }

        protected virtual void OnInteraction(InputAction.CallbackContext context)
        {
        
        }

        protected virtual void OnCancel(InputAction.CallbackContext context)
        {
            
        }

        protected virtual void SelectTile(Vector2 targetPosition)
        {
            if (targetPosition == position || Board.GetTile(targetPosition) is null)
            {
                return;
            }

            position = targetPosition;
            tileSelectionCursor.localPosition = Board.GetTile(targetPosition).position;
        }
    }
}
