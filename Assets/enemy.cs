using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class describing Enemy Drones:
public class enemy : MonoBehaviour
{
	// delegate to inform about what the drones are doing
    public delegate void enemyDelegate(int whatDroneAmI, int whatHappend);
    public static event enemyDelegate EnemyEventSubscription;

	// life the enemy has left
    public float hp = 6;

    // Start is called before the first frame update
    void Start()
    {
    }

	// Set to be an active listener to hear for user sound and
	// in case of detection, sneak up to him and explode
    public void setActiveListener()
    {
        MicrophoneInputControler.microphoneOverThreshold += loudNoiseHandler;
    }

	// function to get informed if i was hit by bullet
    public void BulletHitMe(GameObject other)
    {
        hp -= other.GetComponent<bullet>().damage;
        Debug.Log(hp);
        AnimateWhenDying();
    }

	// Kill current drone
    public void gotKilled()
    {
        if (hp > 0)
        {
            hp = 0;
            AnimateWhenDying();
        }
    }

	// Create explosion with sound when drone is killed
	// also show sparks when hit by bullet
    void AnimateWhenDying()
    {
        GameObject hitPrefab = Resources.Load("BasicSpark2") as GameObject;
        GameObject currentHit = Instantiate(hitPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(currentHit, 1f);
        if (hp <= 0)
        {
            GameObject prefab = Resources.Load("sprite_realExplosion_c_example") as GameObject;
            // Debug.Log(prefab);
            GameObject explosion = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
            GetComponent<AudioSource>().Play();
            InformOverEvent();
            //Debug.Log(explosion); 
            foreach (GameObject child in GameObjectSearcher.GetChildObject(
              GetComponent<Transform>(), InGameComunicationCodes.enemyMeshTag))
            {
                //Debug.Log("foreach"+child);
                Destroy(child, 0.1f);
            }
            Destroy(explosion, 1.2f);
            Destroy(gameObject, 4f);
        }
    }

	// remove active listener from microphone when drone died
    void OnDestroy()
    {
        MicrophoneInputControler.microphoneOverThreshold -= loudNoiseHandler;
    }

	// inform other over events the drone is doing
    void InformOverEvent()
    {
        if (gameObject.tag == InGameComunicationCodes.enemyDroneGuardian)
        {
            EnemyEventSubscription(InGameComunicationCodes.droneGuardianType,
              InGameComunicationCodes.droneHasDied);
        }
        else if (gameObject.tag == InGameComunicationCodes.enemyDroneCollector)
        {
            EnemyEventSubscription(InGameComunicationCodes.droneCollectorType,
              InGameComunicationCodes.droneHasDied);
        }
    }

	// when loud noise was heard, calculate the voice after travelling the distance
	// to where drone is and in case it exceeds threshold, start attacking user
    void loudNoiseHandler(float db_, Vector3 position_)
    {
        float distanceToSource = Vector3.Distance(position_, transform.position);
        float hearingAtThisPosition = db_ * (InGameComunicationCodes.loudnessDecreaseOverDistanceOne / distanceToSource);
        Debug.Log("Distance: " + distanceToSource + "; Hearing: " + hearingAtThisPosition + "; Original: " + db_);
        if (hearingAtThisPosition > InGameComunicationCodes.loudnessSufficientToAttack)
        {
            gameObject.GetComponent<MoveAlongPath>().setToUserAttackMode(position_);
        }
    }
}

