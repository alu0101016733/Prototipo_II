using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class that describes behavior of simple gun
public class gun : MonoBehaviour
{
    bool isActive = true;
    float lastFired = 0f;
    public float bulletSpeed = 1400f;
    public Rigidbody bullet1;
    public float bulletForce = 1f;
    public int numberBullet = 12;
    public float repeatFireAfter = 0.5f;
    public static float damage = 2f;
    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
    }

	void Awake() {
		// when user fires we will get informed
		userInput.userInputEvent += Fire;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		// limit gun to fire interval
        lastFired += Time.deltaTime;
    }

	// Fire a bullet towards the position the gun is pointing
    void Fire(int state)
    {
        if (state == userInputDefinition.Fire && isActive)
        {
            //Debug.Log("Entrando en fire");
            if (lastFired >= repeatFireAfter)
            {
				// create bullet and send it his way
                GetComponent<AudioSource>().Play();
                Rigidbody bulletClone = (Rigidbody)Instantiate(
					bullet1, bullet1.transform.position, transform.rotation);
                bulletClone.velocity = Camera.main.transform.forward * bulletSpeed;
                bulletClone.GetComponent<bullet>().damage = damage;
                bulletClone.GetComponent<bullet>().startPosition = GetComponent<Transform>().position;
                bulletClone.GetComponent<bullet>().active = true;
                bulletClone.GetComponent<bullet>().initialVelocity = bulletSpeed;
                bulletClone.GetComponent<bullet>().DeleteYourselfAfter();
                bulletClone.GetComponent<bullet>().addBulletTrail();
                lastFired = 0f;
            }
        }
    }
}
