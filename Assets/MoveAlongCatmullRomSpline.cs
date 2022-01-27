using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// move object aling catmull rom spline
public class MoveAlongCatmullRomSpline : MoveAlongPath
{
    private List<Transform> checkPoints; // checkoints drone will follow
    
    // Start is called before the first frame update
    // get all points and generate random path
    void Start()
    {
        checkPoints = new List<Transform>();
        foreach (GameObject enemyWP in GameObject.FindGameObjectsWithTag(tagOfCheckpoints)){
            checkPoints.Add(enemyWP.GetComponent<Transform>());
            //enemyWP.GetComponent<Renderer>().enabled = false;
        }  
        checkPoints = BasicUtilitiesForAllScripts.Randomize(checkPoints);
        checkPoints.Insert(0, GameObject.FindGameObjectsWithTag(tagOfSpawnpoint)[0].GetComponent<Transform>());
        nextPosition = GetNextPositionAccordingToCatmullRomSpline(Mathf.FloorToInt(curPos));
        currentPosition = nextPosition;
    }

    // call update methods
    void FixedUpdate() {
        if (currentlyActive) {
            currentPosition = nextPosition;
            UpdatePosition();
        } else if (attackUserMode) {
            moveTowardsUser();
        }
    }

    // update the current position of the gameObject to follow path
    void UpdatePosition() {
        curPos += Time.deltaTime * speedModifier;
        if (Mathf.FloorToInt(curPos) >= checkPoints.Count) {
            curPos = 0f;
            Transform spawnPoint = checkPoints[0];
            checkPoints.RemoveAt(0);
            checkPoints = BasicUtilitiesForAllScripts.Randomize(checkPoints);
            checkPoints.Insert(0, spawnPoint);
        }
        GetComponent<Transform>().position = nextPosition;
        nextPosition = GetNextPositionAccordingToCatmullRomSpline(Mathf.FloorToInt(curPos));
        GetComponent<Transform>().LookAt(nextPosition);
    }

    // does what the function name suggests
    Vector3 GetNextPositionAccordingToCatmullRomSpline(int pos)
	{
		//The 4 points we need to form a spline between p1 and p2
		Vector3 p0 = checkPoints[CatmullRomSplineUtilities.ClampListPos(pos - 1, checkPoints.Count)].position;
		Vector3 p1 = checkPoints[pos].position;
		Vector3 p2 = checkPoints[CatmullRomSplineUtilities.ClampListPos(pos + 1, checkPoints.Count)].position;
		Vector3 p3 = checkPoints[CatmullRomSplineUtilities.ClampListPos(pos + 2, checkPoints.Count)].position;

        //Debug.Log("Catmull Rom Position: " + pos + "CurPos = " + curPos);

        return CatmullRomSplineUtilities.GetCatmullRomPosition(curPos-pos, p0, p1, p2, p3);
	}
}
