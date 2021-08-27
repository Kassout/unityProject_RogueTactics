using UnityEngine;

/// <summary>
/// TODO: comments
/// </summary>
public class InputController : MonoBehaviour
{

    /// <summary>
    /// TODO: comments
    /// </summary>
    private Repeater _horizontal = new Repeater("Horizontal");

    /// <summary>
    /// TODO: comments
    /// </summary>
    private Repeater _vertical = new Repeater("Vertical");
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    void Update()
    {
        
    }
}

/// <summary>
/// TODO: comments
/// </summary>
class Repeater
{
    /// <summary>
    /// TODO: comments
    /// </summary>
    private const float threshold = 0.5f;
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    private const float rate = 0.25f;
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    private float _next;
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    private bool _hold;
    
    /// <summary>
    /// TODO: comments
    /// </summary>
    private string _axis;

    /// <summary>
    /// TODO: comments
    /// </summary>
    /// <param name="axisName">TODO: comments</param>
    public Repeater(string axisName)
    {
        _axis = axisName;
    }

    /// <summary>
    /// TODO: comments
    /// </summary>
    public int Update()
    {
        int retValue = 0;
        int value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));

        if (value != 0)
        {
            if (Time.time > _next)
            {
                retValue = value;
                _next = Time.time + (_hold ? rate : threshold);
                _hold = true;
            }
        }
        else
        {
            _hold = false;
            _next = 0;
        }

        return retValue;
    }
}
