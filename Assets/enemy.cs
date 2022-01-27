using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
  public delegate void enemyDelegate(int whatDroneAmI, int whatHappend);
  public static event enemyDelegate EnemyEventSubscription;
  
  public float hp = 6;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void setActiveListener() {
      MicrophoneInputControler.microphoneOverThreshold += loudNoiseHandler;
    }

  public void BulletHitMe(GameObject other) {
    hp -= other.GetComponent<bullet>().damage;
    Debug.Log(hp);
    AnimateWhenDying();
  }

  public void gotKilled() {
    if (hp > 0)
    {
      hp = 0;
      AnimateWhenDying(); 
    }
  }

  void AnimateWhenDying() {
    GameObject hitPrefab = Resources.Load("BasicSpark2") as GameObject;
    GameObject currentHit = Instantiate(hitPrefab, transform.position, Quaternion.identity) as GameObject;
    Destroy(currentHit, 1f);
    if (hp <= 0) { 
      GameObject prefab = Resources.Load("sprite_realExplosion_c_example") as GameObject;
      // Debug.Log(prefab);
      GameObject explosion = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
      GetComponent<AudioSource>().Play();
      InformOverEvent();
      //Debug.Log(explosion); 
      foreach (GameObject child in GameObjectSearcher.GetChildObject(
        GetComponent<Transform>(),InGameComunicationCodes.enemyMeshTag)) {
        //Debug.Log("foreach"+child);
        Destroy(child,0.1f);
      }
      Destroy(explosion,1.2f);
      Destroy(gameObject,4f);
    }
  }

  void OnDestroy() {
    MicrophoneInputControler.microphoneOverThreshold -= loudNoiseHandler;
  }

  void InformOverEvent() {
    if (gameObject.tag == InGameComunicationCodes.enemyDroneGuardian) {
      EnemyEventSubscription(InGameComunicationCodes.droneGuardianType, 
        InGameComunicationCodes.droneHasDied);
    } else if (gameObject.tag == InGameComunicationCodes.enemyDroneCollector) {
      EnemyEventSubscription(InGameComunicationCodes.droneCollectorType, 
        InGameComunicationCodes.droneHasDied);
    }
  }

  void loudNoiseHandler(float db_, Vector3 position_) {
    float distanceToSource = Vector3.Distance(position_, transform.position);
    float hearingAtThisPosition = db_ * (InGameComunicationCodes.loudnessDecreaseOverDistanceOne / distanceToSource);
    Debug.Log("Distance: "+distanceToSource+"; Hearing: "+hearingAtThisPosition+"; Original: "+ db_);
    if (hearingAtThisPosition > InGameComunicationCodes.loudnessSufficientToAttack) {
      gameObject.GetComponent<MoveAlongPath>().setToUserAttackMode(position_);
    }
  }
}
