using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to hold some basic functions used by multiple scripts
// to calculate variables needed for Catmull Rom Spline calculation
public class CatmullRomSplineUtilities : MonoBehaviour
{
    //Clamp the list positions to allow looping
    static public int ClampListPos(int pos, int length)
    {
        if (pos < 0)
        {
            pos = length - 1;
        }

        if (pos > length)
        {
            pos = 1;
        }
        else if (pos > length - 1)
        {
            pos = 0;
        }

        return pos;
    }

    //Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
    static public Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;
        Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

        return pos;
    }
}
