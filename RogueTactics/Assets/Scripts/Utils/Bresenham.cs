using System.Collections;
using UnityEngine;

public class Bresenham : IEnumerable
{
    private readonly Vector2 _end;
    private readonly Vector2 _start;
    private readonly float _steps;

    public Bresenham(Vector2 pStart, Vector2 pEnd)
    {
        _start = pStart;
        _end = pEnd;
        _steps = 1;
    }

    public Bresenham(Vector2 pStart, Vector2 pEnd, float pSteps)
    {
        _steps = pSteps;
        _start = pStart * _steps;
        _end = pEnd * _steps;
    }

    public IEnumerator GetEnumerator()
    {
        Vector2 result;

        var x = _start.x; 
        var y = _start.y;                         // starting cell
        
        result.x = x;
        result.y = y;
        yield return result;
        
        var dx = _start.x == _end.x ? 0 : _end.x > _start.x ? 1 : -1; // right or left
        var dy = _start.y == _end.y ? 0 : _end.y > _start.y ? 1 : -1; // up or down
        
        if (dx == 0 || dy == 0) 
        {
            // STRAIGHT LINE ...
            while (x != _end.x || y != _end.y)
            {
                if (dy == 0)
                {
                    x += dx;
                    result.x = x;
                    result.y = y;
                    yield return result;  
                }
                else if (dx == 0)
                {
                    y += dy;
                    result.x = x;
                    result.y = y;
                    yield return result;
                }
            }
        }
        else if (Mathf.Abs(_end.x - _start.x) > Mathf.Abs(_end.y - _start.y)) 
        {
            // MAINLY HORIZONTAL
            var tan = (_end.y - _start.y) / (_end.x - _start.x);      // tangent
            var max = (1 - Mathf.Abs(tan)) / 2;        // distance threshold
            
            while (x != _end.x || y != _end.y) // while target not reached
            {            
                var ideal = _start.y + (x - _start.x) * tan;    // y of ideal line at x
                if ((ideal - y) * dy >= max) y += dy; // move vertically
                else x += dx;                         // move horizontally
                result.x = x;
                result.y = y;
                yield return result;
            }
        }
        else 
        {
            // MAINLY VERTICAL
            var cotan = (_end.x - _start.x) / (_end.y - _start.y);    // cotangent
            var max = (1 - Mathf.Abs(cotan)) / 2;      // distance threshold
            
            while (x != _end.x || y != _end.y) // while target not reached
            {            
                var ideal = _start.x + (y - _start.y) * cotan;  // x of ideal line at y
                if ((ideal - x) * dx >= max) x += dx; // move horizontally
                else y += dy;                         // move vertically
                result.x = x;
                result.y = y;
                yield return result;
            }
        }
    }
}