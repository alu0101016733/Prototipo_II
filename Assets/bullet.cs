using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
  public float damage = 2f;
  public Vector3 startPosition;
  public float initialVelocity;
  public float deceleration;
  public bool active = false;

  private Vector3 currentPosition;

  void FixedUpdate() {
    if (active) {
      currentPosition = GetComponent<Transform>().position;
      Debug.DrawLine(startPosition, currentPosition, Color.green, 1f);

      // draw raycast and check if it hits something
      RaycastHit hit;
      if (Physics.Linecast(startPosition, currentPosition, out hit)) {
        if (hit.collider != null) {
          var hitReceiver = hit.collider.gameObject.GetComponent<enemy>();
          if (hitReceiver != null) {
            Debug.Log("LINECAST HIT SOMETHING");
            hitReceiver.BulletHitMe(gameObject);
          }
        }
      }
      
      startPosition = currentPosition;
    }
  }

  public void DeleteYourselfAfter(float sec_ = 3f) {
    // Debug.Log("I GOT DESTROYED");
    Destroy(gameObject, sec_);
  }
  
}
