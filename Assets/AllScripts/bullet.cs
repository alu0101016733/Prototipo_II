using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class to hold bullet script
public class bullet : MonoBehaviour
{
	// variables intresting for a bullet:
    public float damage = 2f;
    public Vector3 startPosition; // where bullet started
    public float initialVelocity;
    public float deceleration;
    public bool active = false;

    private Vector3 currentPosition; // where bullet goes

	// Update position of the bullet and use Linecast to inform
	// all objects that where hit while bullet was travelling
    void FixedUpdate()
    {
        if (active)
        {
            currentPosition = GetComponent<Transform>().position;
            Debug.DrawLine(startPosition, currentPosition, Color.green, 1f);

            // draw raycast and check if it hits something
            RaycastHit hit;
            if (Physics.Linecast(startPosition, currentPosition, out hit))
            {
                if (hit.collider != null)
                {
                    var hitReceiver = hit.collider.gameObject.GetComponent<enemy>();
                    if (hitReceiver != null)
                    {
                        Debug.Log("LINECAST HIT SOMETHING");
                        hitReceiver.BulletHitMe(gameObject);
                    }
                }
            }

            startPosition = currentPosition;
        }
    }

	// Since the bullets are only a short time active killers
	// we will dispose of them after x time
    public void DeleteYourselfAfter(float sec_ = 3f)
    {
        // Debug.Log("I GOT DESTROYED");
        Destroy(gameObject, sec_);
    }

	// To prettier see the path a bullet takes, we ad a tail:
    public void addBulletTrail()
    {
        GameObject trailPrefab = Resources.Load("EtherialStreaksTrail") as GameObject;
        GameObject bulletTrail = Instantiate(trailPrefab, transform.position, Quaternion.identity) as GameObject;
        bulletTrail.transform.parent = gameObject.transform;
        Destroy(bulletTrail, 1f);
    }
}
