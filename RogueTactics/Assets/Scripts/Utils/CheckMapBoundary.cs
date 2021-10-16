using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckMapBoundary : MonoBehaviour
{
    private int xMin;
    private int xMax;
    private int yMin;
    private int yMax;
    
    // Start is called before the first frame update
    void Start()
    {
        xMin = (int) Board.tileBoard.Min(data => data.position.x);
        xMax = (int) Board.tileBoard.Max(data => data.position.x);
        yMin = (int) Board.tileBoard.Min(data => data.position.y);
        yMax = (int) Board.tileBoard.Max(data => data.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position;
        position.x = Mathf.Clamp(transform.position.x, xMin, xMax);
        position.y = Mathf.Clamp(transform.position.y, yMin, yMax);

        transform.position = position;
    }
}
