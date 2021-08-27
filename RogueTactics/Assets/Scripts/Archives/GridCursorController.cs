using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// TODO: comments
/// </summary>
public class GridCursorController : MonoBehaviour
{
    /// <summary>
    /// Instance variable <c>mapCamera</c> represents the map camera of the current scene.
    /// </summary>
    [SerializeField]
    private Camera mapCamera;

    /// <summary>
    /// Instance variable <c>mouseScreenPos</c> represents the 2D coordinate values of the mouse position projected on the screen.
    /// </summary>
    private Vector2 mouseScreenPos;

    /// <summary>
    /// Instance variable <c>mousePos</c> represents the 2D coordinate values of the current mouse position.
    /// </summary>
    private Vector2 mousePos;

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    public void OnMovement(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="context">TODO: comments</param>
    public void OnInteraction(InputAction.CallbackContext context) {

    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    void Update()
    {
        mouseScreenPos = mapCamera.ScreenToWorldPoint(mousePos);
        transform.position = new Vector2(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y));
    }
}
