using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for heritance for MoveAlongPath classes
// contains basic methods and variables needed by the other classes
public class MoveAlongPath : MonoBehaviour
{
    public string tagOfCheckpoints = "enemy_waypoint";
    public string tagOfSpawnpoint = "enemy_waypoint_spawn";
    public float speedModifier = 0.2f;
    public bool currentlyActive = false;
    public bool attackUserMode = false;
    private Vector3 attackPosition;
    private float attackSpeed;

    public float curPos = 0f;
    public Vector3 currentPosition;
    public Vector3 nextPosition;
    private float speedIncreaseWhenUserAttacking = 5f;
    private float timeAttackInProgress = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // void FixedUpdate() {
    //     if (attackUserMode) {
    //         moveTowardsUser();
    //     }
    // }

    // instead of moving on a curve, move towards the user
    public void moveTowardsUser() {
        attackSpeed += speedIncreaseWhenUserAttacking * Time.deltaTime;
        Vector3 targetDirection = attackPosition - transform.position;
        float singleStep = attackSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position += transform.forward* attackSpeed * Time.deltaTime;
        timeAttackInProgress += Time.deltaTime;
    }

    // change the mode from moving along curve, to moving directly
    // towards the user
    public void setToUserAttackMode(Vector3 toPos) {
        timeAttackInProgress = 0f;
        attackSpeed = speedModifier;
        attackPosition = toPos;
        currentlyActive = false;
        attackUserMode = true;
    }

    

}
