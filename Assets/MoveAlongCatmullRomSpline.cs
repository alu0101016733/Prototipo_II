using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongCatmullRomSpline : MoveAlongPath
{
    private List<Transform> checkPoints; // checkoints drone will follow
    
    // Start is called before the first frame update
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

    void FixedUpdate() {
        if (currentlyActive) {
            currentPosition = nextPosition;
            UpdatePosition();
        } else if (attackUserMode) {
            moveTowardsUser();
        }
    }

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
