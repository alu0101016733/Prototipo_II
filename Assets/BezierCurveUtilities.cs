using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveUtilities : MonoBehaviour
{
    static public int ClampListPos(int pos, int length)
	{
		if (pos > length)
		{
			pos = 0;
		}
		else if (pos > length - 1)
		{
			pos = 1;
		}
		return pos;
	}

	static public Vector3 GetBezierCurvePosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		Vector3 a = Mathf.Pow((1f - t), 3f) * p0;
		Vector3 b = 3 * Mathf.Pow((1f - t), 2f) * t * p1;
		Vector3 c = 3 * (1f - t) * Mathf.Pow(t, 2f) * p2;
		Vector3 d = Mathf.Pow(t, 3f) * p3;

		Vector3 pos = a + b + c + d;

		return pos;
	}
}
