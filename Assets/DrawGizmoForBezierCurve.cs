using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Draw gizmo for bezier curve:
public class DrawGizmoForBezierCurve : MonoBehaviour
{
    public string tagOfCheckpoints = "enemy_waypoint";
    public string tagOfSpawnpoint = "enemy_waypoint_spawn";
    public float speedModifier = 0.1f;
    
    private List<Transform> checkPoints;
    private int currentBezierPosition = 0;

	//Display the bezier curve gizmo without having to press play
	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;

        checkPoints = new List<Transform>();
        foreach (GameObject enemyWP in GameObject.FindGameObjectsWithTag(tagOfCheckpoints)){
            checkPoints.Add(enemyWP.GetComponent<Transform>());
        }  
        checkPoints.Insert(0, GameObject.FindGameObjectsWithTag(tagOfSpawnpoint)[0].GetComponent<Transform>());

        // check if odd, if it is we will duplicate entries to have even values
        if ((checkPoints.Count % 2) == 1) {
            int listLen = checkPoints.Count;
            for (int i = 0; i < listLen; i++) {
                checkPoints.Add(checkPoints[i]);
            }
        }

        StartBezierCurve(0);
		//Draw the Catmull-Rom spline between the points
		for (int i = 3; i < checkPoints.Count; i+=2)
		{
			ContinueBezierCurve(i);
		}
	}

    void StartBezierCurve(int pos = 0) {
		Vector3 p0 = checkPoints[pos].position;
		Vector3 p1 = checkPoints[pos + 1].position;
		Vector3 p2 = checkPoints[pos + 2].position;
		Vector3 p3 = checkPoints[pos + 3].position;
        DisplayBezierCurve(p0, p1, p2, p3);
    }

    void ContinueBezierCurve(int pos) {
		Vector3 p0 = checkPoints[pos].position;
        Vector3 p1 = checkPoints[pos - 1].position;
        p1 = (-1 * (p1 - p0)) + p0;
		Vector3 p2 = checkPoints[BezierCurveUtilities.ClampListPos(pos + 1, checkPoints.Count)].position;
		Vector3 p3 = checkPoints[BezierCurveUtilities.ClampListPos(pos + 2, checkPoints.Count)].position;
        if (BezierCurveUtilities.ClampListPos(pos + 1, checkPoints.Count) == 1) {
            p2 = (-1 * (p2 - p3)) + p3;
        }
        DisplayBezierCurve(p0, p1, p2, p3);
    }

	//Display a curve between ywo points gotten from bezier curve algorithm
	void DisplayBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		//The start position of the line
		Vector3 lastPos = p0;

		//Make sure it's is adding up to 1, so 0.3 will give a gap, but 0.02 will work
		float resolution = 0.02f;

		//How many times should we loop?
		int loops = Mathf.FloorToInt(1f / resolution);

		for (int i = 1; i <= loops; i++)
		{
			//Which t position are we at?
			float t = i * resolution;

			//Find the coordinate between the end points with a bezier curve
			Vector3 newPos = BezierCurveUtilities.GetBezierCurvePosition(t, p0, p1, p2, p3);

			
            Gizmos.DrawLine(lastPos, newPos);

			//Save this pos so we can draw the next line segment
			lastPos = newPos;
		}
	}
}
