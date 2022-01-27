using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmoForCatmullRomSpline : MonoBehaviour
{
    public string tagOfCheckpoints = "enemy_waypoint";
    public string tagOfSpawnpoint = "enemy_waypoint_spawn";
    public float speedModifier = 0.1f;
    
    private List<Transform> checkPoints;
	//Are we making a line or a loop?
	public bool isLooping = true;

	//Display without having to press play
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

        checkPoints = new List<Transform>();
        foreach (GameObject enemyWP in GameObject.FindGameObjectsWithTag(tagOfCheckpoints)){
            checkPoints.Add(enemyWP.GetComponent<Transform>());
        }  
        //checkPoints = BasicUtilitiesForAllScripts.Randomize(checkPoints);
        checkPoints.Insert(0, GameObject.FindGameObjectsWithTag(tagOfSpawnpoint)[0].GetComponent<Transform>());

		//Draw the Catmull-Rom spline between the points
		for (int i = 0; i < checkPoints.Count; i++)
		{
			//Cant draw between the endpoints
			//Neither do we need to draw from the second to the last endpoint
			//...if we are not making a looping line
			if ((i == 0 || i == checkPoints.Count - 2 || i == checkPoints.Count - 1) && !isLooping)
			{
				continue;
			}

			DisplayCatmullRomSpline(i);
		}
	}

	//Display a spline between 2 points derived with the Catmull-Rom spline algorithm
	void DisplayCatmullRomSpline(int pos)
	{
		//The 4 points we need to form a spline between p1 and p2
		Vector3 p0 = checkPoints[CatmullRomSplineUtilities.ClampListPos(pos - 1, checkPoints.Count)].position;
		Vector3 p1 = checkPoints[pos].position;
		Vector3 p2 = checkPoints[CatmullRomSplineUtilities.ClampListPos(pos + 1, checkPoints.Count)].position;
		Vector3 p3 = checkPoints[CatmullRomSplineUtilities.ClampListPos(pos + 2, checkPoints.Count)].position;

		//The start position of the line
		Vector3 lastPos = p1;

		//The spline's resolution
		//Make sure it's is adding up to 1, so 0.3 will give a gap, but 0.2 will work
		float resolution = 0.1f;

		//How many times should we loop?
		int loops = Mathf.FloorToInt(1f / resolution);

		for (int i = 1; i <= loops; i++)
		{
			//Which t position are we at?
			float t = i * resolution;

			//Find the coordinate between the end points with a Catmull-Rom spline
			Vector3 newPos = CatmullRomSplineUtilities.GetCatmullRomPosition(t, p0, p1, p2, p3);

			//Draw this line segment
			Gizmos.DrawLine(lastPos, newPos);

			//Save this pos so we can draw the next line segment
			lastPos = newPos;
		}
	}
}
