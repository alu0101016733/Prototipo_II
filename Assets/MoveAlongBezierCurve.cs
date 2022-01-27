using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongBezierCurve : MoveAlongPath
{
    private List<Transform> checkPoints; // checkoints drone will follow
    private List<Transform> beginningCheckPoints;
    private Transform spawnPoint;
    private int previousPos = 0;

    // Start is called before the first frame update
    void Start() {
        checkPoints = new List<Transform>();
        foreach (GameObject enemyWP in GameObject.FindGameObjectsWithTag(tagOfCheckpoints)){
            checkPoints.Add(enemyWP.GetComponent<Transform>());
            //enemyWP.GetComponent<Renderer>().enabled = false;
        }  
        checkPoints = BasicUtilitiesForAllScripts.Randomize(checkPoints);
        beginningCheckPoints = new List<Transform>(checkPoints);
        spawnPoint = GameObject.FindGameObjectsWithTag(tagOfSpawnpoint)[0].GetComponent<Transform>();
        checkPoints.Insert(0, spawnPoint);
        nextPosition = GetNextPosition(Mathf.FloorToInt(curPos));
        currentPosition = nextPosition;
        SetCheckpointsToEven();
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
        GetComponent<Transform>().position = nextPosition;
        curPos += Time.deltaTime * speedModifier;
        int flooredCurPos = Mathf.FloorToInt(curPos);
        if (flooredCurPos != previousPos)
        {
            UpdateWhatPointToConnect(flooredCurPos);
            flooredCurPos = Mathf.FloorToInt(curPos);
            previousPos = flooredCurPos;
        }
        if (flooredCurPos >= checkPoints.Count) {
            curPos = 0f;
            previousPos = 0;
            checkPoints = new List<Transform>(beginningCheckPoints);
            checkPoints = BasicUtilitiesForAllScripts.Randomize(checkPoints);
            checkPoints.Insert(0, spawnPoint);
            SetCheckpointsToEven();
            Debug.Log("Resetting Bezier Curve position to 0, list has size"+checkPoints.Count);
            nextPosition = GetNextPosition(0);
        } else {
            nextPosition = GetNextPosition(flooredCurPos);
        }
        GetComponent<Transform>().LookAt(nextPosition);
        Debug.DrawLine(GetComponent<Transform>().position, nextPosition, Color.red, 5f);
    }

    void SetCheckpointsToEven() {
        // check if odd, if it is we will duplicate entries to have even values
        if ((checkPoints.Count % 2) == 1) {
            int listLen = checkPoints.Count;
            for (int i = 0; i < listLen; i++) {
                checkPoints.Add(checkPoints[i]);
            }
        }
    }

    Vector3 GetNextPosition(int pos) {
        if (pos == 0) {
            return StartBezierCurve(pos);
        } else {
            return ContinueBezierCurve(pos);
        }
    }

    Vector3 StartBezierCurve(int pos = 0) {
		Vector3 p0 = checkPoints[pos].position;
		Vector3 p1 = checkPoints[pos + 1].position;
		Vector3 p2 = checkPoints[pos + 2].position;
		Vector3 p3 = checkPoints[pos + 3].position;
        return GetNextPositionFromPoints(pos, p0, p1, p2, p3);
    }

    Vector3 ContinueBezierCurve(int pos) {
		Vector3 p0 = checkPoints[pos].position;
        Vector3 p1 = checkPoints[pos - 1].position;
        p1 = (-1 * (p1 - p0)) + p0;
		Vector3 p2 = checkPoints[BezierCurveUtilities.ClampListPos(pos + 1, checkPoints.Count)].position;
		Vector3 p3 = checkPoints[BezierCurveUtilities.ClampListPos(pos + 2, checkPoints.Count)].position;
        if (BezierCurveUtilities.ClampListPos(pos + 1, checkPoints.Count) == 1) {
            p2 = (-1 * (p2 - p3)) + p3;
        }
        return GetNextPositionFromPoints(pos, p0, p1, p2, p3);
    }

    Vector3 GetNextPositionFromPoints(int pos, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
        // Debug.Log("Bezier curve Position: " + pos + "CurPos = " + curPos);
        return BezierCurveUtilities.GetBezierCurvePosition(curPos-pos, p0, p1, p2, p3);
    }

    void UpdateWhatPointToConnect(int pos) {
        if (pos < 3) {
            curPos = 3f;
        } else {
            curPos += 1;
        }
    }
}
