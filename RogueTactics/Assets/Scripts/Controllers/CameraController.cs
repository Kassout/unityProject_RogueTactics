using UnityEngine;

/// <summary>
/// TODO: comments
/// </summary>
public class CameraController : MonoBehaviour
{
    public int boundary = 50;

    /// <summary>
    /// TODO: comments
    /// </summary>
    public float speed = 3f;

    private int _screenWidth;

    private int _screenHeight;

    private int _xMin;

    private int _xMax;

    private int _yMin;

    private int _yMax;
    
    private Vector3 _transformPosition;

    private void Start()
    {
#if  UNITY_EDITOR
        speed = 3f;
#endif
    
        _screenHeight = Screen.height;
        _screenWidth = Screen.width;

        _transformPosition = BattleController.Instance.tileSelectionCursor.position;
        transform.position = new Vector3(_transformPosition.x, _transformPosition.y, transform.position.z);
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    private void LateUpdate()
    {
        //if (follow) _transform.position = Vector3.Lerp(_transform.position, new Vector3(follow.position.x, follow.position.y, _transform.position.z), speed * Time.fixedDeltaTime);
        if (!BattleController.Instance.inTransition)
        {
            
            //Vector2 cursorScreenPos = Mouse.current.position.ReadValue();
            Vector2 cursorScreenPos =
                Camera.main.WorldToScreenPoint(BattleController.Instance.tileSelectionCursor.position);
        
            if (cursorScreenPos.x > _screenWidth - boundary)
            {
                _transformPosition.x += speed * Time.fixedDeltaTime;
            }
        
            if (cursorScreenPos.x < 0 + boundary)
            {
                _transformPosition.x -= speed * Time.fixedDeltaTime;
            }
        
            if (cursorScreenPos.y > _screenHeight - boundary)
            {
                _transformPosition.y += speed * Time.fixedDeltaTime;
            }
        
            if (cursorScreenPos.y < 0 + boundary)
            {
                _transformPosition.y -= speed * Time.fixedDeltaTime;
            }
            
            transform.position = Vector3.Lerp(transform.position, new Vector3(_transformPosition.x, _transformPosition.y, transform.position.z), speed * Time.fixedDeltaTime);
        }
        else
        {
            _transformPosition = BattleController.Instance.turn.actor.transform.position;
            transform.position = Vector3.Lerp(transform.position, new Vector3(_transformPosition.x, _transformPosition.y, transform.position.z), speed * Time.fixedDeltaTime);
        }
    }
}