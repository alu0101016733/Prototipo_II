using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameComunicationCodes : MonoBehaviour
{
    // Codes
    static public int showStartMenu = 0;
    static public int pauseCurrentGame = 1;
    static public int resumeToCurrentGame = 2;
    static public int togglePause = 3;
    static public int quitGame = 5;
    static public int playerDied = 6;

    static public int droneGuardianType = 100;
    static public int droneCollectorType = 101;
    static public int droneHasDetectedUser = 102;
    static public int droneHasDied = 103;

    // Limits:
    static public float microphoneLimitsUnitlCall = .1f;
    static public float loudnessDecreaseOverDistanceOne = 1f;
    static public float loudnessSufficientToAttack = 0.00005f;

    // Tags:
    static public string enemyMeshTag = "enemy_drone";
    static public string[] allEnemies = {"enemy_drone_guardian", "enemy_drone_collector"};
    static public string enemyDroneGuardian = allEnemies[0];
    static public string enemyDroneCollector = allEnemies[1];
}
