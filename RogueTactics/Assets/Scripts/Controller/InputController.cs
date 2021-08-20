using UnityEngine;

public class InputController : MonoBehaviour
{
    private Repeater _horizontal = new Repeater("Horizontal");
    private Repeater _vertical = new Repeater("Vertical");
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
