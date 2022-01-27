using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        userInput.userInputEvent += Fire;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastFired += Time.deltaTime;
    }
    void Fire(int state) {
      if (state == userInputDefinition.Fire && isActive) {
        //Debug.Log("Entrando en fire");
        if (lastFired >= repeatFireAfter) {
          GetComponent<AudioSource>().Play();
          Rigidbody bulletClone = (Rigidbody) Instantiate(bullet1, bullet1.transform.position , transform.rotation);
          bulletClone.velocity = Camera.main.transform.forward * bulletSpeed;
          bulletClone.GetComponent<bullet>().damage = damage;
          bulletClone.GetComponent<bullet>().startPosition = GetComponent<Transform>().position;
          bulletClone.GetComponent<bullet>().active = true;
          bulletClone.GetComponent<bullet>().initialVelocity = bulletSpeed;
          bulletClone.GetComponent<bullet>().DeleteYourselfAfter();
          lastFired = 0f;
        }
      }
    }
}
