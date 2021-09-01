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

        var dx = (int)(_end.x - _start.x);
        var dy = (int)(_end.y - _start.y);

        var ax = Mathf.Abs(dx) << 1;
        var ay = Mathf.Abs(dy) << 1;

        var sx = (int)Mathf.Sign(dx);
        var sy = (int)Mathf.Sign(dy);

        var x = (int)_start.x;
        var y = (int)_start.y;

        if (ax >= Mathf.Max(ay)) // x dominant
        {
            var yd = ay - (ax >> 1);
            for (;;)
            {
                result.x = (int)(x / _steps);
                result.y = (int)(y / _steps);
                yield return result;

                if (x == (int)_end.x)
                    yield break;

                if (yd >= 0)
                {
                    y += sy;
                    yd -= ax;
                }

                x += sx;
                yd += ay;
            }
        }

        if (ay >= Mathf.Max(ax)) // y dominant
        {
            var xd = ax - (ay >> 1);
            for (;;)
            {
                result.x = (int)(x / _steps);
                result.y = (int)(y / _steps);
                yield return result;

                if (y == (int)_end.y)
                    yield break;

                if (xd >= 0)
                {
                    x += sx;
                    xd -= ay;
                }

                y += sy;
                xd += ax;
            }
        }
    }
}