using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script o handle everything regarding to the phisical object representing the user
public class UserCharacter : MonoBehaviour
{
    public delegate void userCharacterDelegation(int action);
    public static event userCharacterDelegation userCharacterEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    // when user get hit, drones get killed
    void OnTriggerEnter(Collider other) {
        Debug.Log("TRIGGER USER CHARACTER COLLIDER");
        if (other.GetComponent<enemy>() != null) {
            other.GetComponent<enemy>().gotKilled();
            //userCharacterEvent(InGameComunicationCodes.playerDied);
        }
    }
}
