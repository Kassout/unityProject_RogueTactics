// Copyright (c) 2011 Bob Berkebile (pixelplacement)
// Please direct any bugs/comments/suggestions to http://pixelplacement.com
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
/*
TERMS OF USE - EASING EQUATIONS
Open source under the BSD License.
Copyright (c)2001 Robert Penner
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
Neither the name of the author nor the names of contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using UnityEngine;

/// <summary>
///     TODO: comments
/// </summary>
public static class EasingEquations
{
    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float Linear(float start, float end, float value)
    {
        return (end - start) * value + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float Spring(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) +
                 value) * (1f + 1.2f * (1f - value));
        return start + (end - start) * value;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInQuad(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInOutQuad(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
            return end / 2 * value * value + start;
        value--;
        return -end / 2 * (value * (value - 2) - 1) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInCubic(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseOutCubic(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value + 1) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInOutCubic(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
            return end / 2 * value * value * value + start;
        value -= 2;
        return end / 2 * (value * value * value + 2) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInQuart(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseOutQuart(float start, float end, float value)
    {
        value--;
        end -= start;
        return -end * (value * value * value * value - 1) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInOutQuart(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
            return end / 2 * value * value * value * value + start;
        value -= 2;
        return -end / 2 * (value * value * value * value - 2) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInQuint(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value * value + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseOutQuint(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value * value * value + 1) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInOutQuint(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
            return end / 2 * value * value * value * value * value + start;
        value -= 2;
        return end / 2 * (value * value * value * value * value + 2) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInSine(float start, float end, float value)
    {
        end -= start;
        return -end * Mathf.Cos(value / 1 * (Mathf.PI / 2)) + end + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseOutSine(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Sin(value / 1 * (Mathf.PI / 2)) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInOutSine(float start, float end, float value)
    {
        end -= start;
        return -end / 2 * (Mathf.Cos(Mathf.PI * value / 1) - 1) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInExpo(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Pow(2, 10 * (value / 1 - 1)) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseOutExpo(float start, float end, float value)
    {
        end -= start;
        return end * (-Mathf.Pow(2, -10 * value / 1) + 1) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInOutExpo(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
            return end / 2 * Mathf.Pow(2, 10 * (value - 1)) + start;
        value--;
        return end / 2 * (-Mathf.Pow(2, -10 * value) + 2) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInCirc(float start, float end, float value)
    {
        end -= start;
        return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseOutCirc(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * Mathf.Sqrt(1 - value * value) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInOutCirc(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
            return -end / 2 * (Mathf.Sqrt(1 - value * value) - 1) + start;
        value -= 2;
        return end / 2 * (Mathf.Sqrt(1 - value * value) + 1) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    private static float EaseInBounce(float start, float end, float value)
    {
        end -= start;
        var d = 1f;
        return end - EaseOutBounce(0, end, d - value) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    private static float EaseOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < 1 / 2.75f) return end * (7.5625f * value * value) + start;

        if (value < 2 / 2.75f)
        {
            value -= 1.5f / 2.75f;
            return end * (7.5625f * value * value + .75f) + start;
        }

        if (value < 2.5 / 2.75)
        {
            value -= 2.25f / 2.75f;
            return end * (7.5625f * value * value + .9375f) + start;
        }

        value -= 2.625f / 2.75f;
        return end * (7.5625f * value * value + .984375f) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInOutBounce(float start, float end, float value)
    {
        end -= start;
        var d = 1f;
        if (value < d / 2)
            return EaseInBounce(0, end, value * 2) * 0.5f + start;
        return EaseOutBounce(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInBack(float start, float end, float value)
    {
        end -= start;
        value /= 1;
        var s = 1.70158f;
        return end * value * value * ((s + 1) * value - s) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseOutBack(float start, float end, float value)
    {
        var s = 1.70158f;
        end -= start;
        value = value / 1 - 1;
        return end * (value * value * ((s + 1) * value + s) + 1) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInOutBack(float start, float end, float value)
    {
        var s = 1.70158f;
        end -= start;
        value /= .5f;
        if (value < 1)
        {
            s *= 1.525f;
            return end / 2 * (value * value * ((s + 1) * value - s)) + start;
        }

        value -= 2;
        s *= 1.525f;
        return end / 2 * (value * value * ((s + 1) * value + s) + 2) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float Punch(float amplitude, float value)
    {
        float s = 9;
        if (value == 0) return 0;

        if (value == 1) return 0;

        var period = 1 * 0.3f;
        s = period / (2 * Mathf.PI) * Mathf.Asin(0);
        return amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period);
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInElastic(float start, float end, float value)
    {
        end -= start;

        var d = 1f;
        var p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0)
            return start;

        if ((value /= d) == 1)
            return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return -(a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseOutElastic(float start, float end, float value)
    {
        end -= start;

        var d = 1f;
        var p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0)
            return start;

        if ((value /= d) == 1)
            return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start;
    }

    /// <summary>
    ///     TODO: comments
    /// </summary>
    /// <param name="start">TODO: comments</param>
    /// <param name="end">TODO: comments</param>
    /// <param name="value">TODO: comments</param>
    /// <returns>TODO: comments</returns>
    public static float EaseInOutElastic(float start, float end, float value)
    {
        end -= start;

        var d = 1f;
        var p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0)
            return start;

        if ((value /= d / 2) == 2)
            return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        if (value < 1)
            return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) +
                   start;
        return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end +
               start;
    }
}