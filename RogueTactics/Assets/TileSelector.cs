using System;
using LDtkUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileSelector : MonoBehaviour
{
    public Vector2 inputMovement;

    public Vector2 gridSize;

    public LDtkProjectFile levelFile;

    private void Awake()
    {
        gridSize = levelFile.FromJson.UnityDefaultLevelSize / (int) levelFile.FromJson.DefaultGridSize;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        CalculateTileMovement();
        CheckMovementBoundaries();
    }

    void CalculateTileMovement()
    {
        // TODO: implement
    }

    void CheckMovementBoundaries()
    {
        float _xMovementClamp = Mathf.Clamp(transform.position.x, 0.5f, gridSize.x - 0.5f);
        float _yMovementClamp = Mathf.Clamp(transform.position.y, 0.5f, gridSize.y - 0.5f);
        Vector2 _limitPlayerMovement = new Vector2(_xMovementClamp, _yMovementClamp);
        transform.position = _limitPlayerMovement;
    }

    private void FixedUpdate()
    {
        transform.Translate(inputMovement * Time.deltaTime * 10f);
    }
}
