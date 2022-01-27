using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class implementing the game controller
// contains method to generate enemies according to level
public class GameLogicController : MonoBehaviour
{
    // delegate user menu to start or pause when needed
    public delegate void gameMenuHandlerDelegation(int whatToDo);
    public static event gameMenuHandlerDelegation menuHandlerDelegation;

    // data for current level
    public int startLevel = 0;
    int currentLevel = 0;
    bool currentlyPlaying = false;
    int timeWaitLimitEnemyCreation = 20;

    int inGameLimitEnemyCreation = 5;

    // Enemy Base block:
    GameObject enemyDroneGuardian = null;
    GameObject enemyDroneCollector = null;

    // Level ralated Variables (Values for level 0):
    int droneGuardiansAtOnce = 1;
    int droneCollectorAtOnce = 0;
    int droneGuardiansTotal = 2;
    int droneCollectorTotal = 0;
    int currentlyActiveDroneGuardians = 0;
    int currentlyActiveDroneCollector = 0;
    float droneGuardiansSpeed = 0.2f;
    float droneCollectorSpeed = 0.2f;

    // Start is called before the first frame update
    void Start() {
        // get Enemy drones that will be cloned
        enemyDroneGuardian = GameObject.FindGameObjectWithTag(InGameComunicationCodes.enemyDroneGuardian);
        enemyDroneCollector = GameObject.FindGameObjectWithTag(InGameComunicationCodes.enemyDroneCollector);

        menuHandlerDelegation(InGameComunicationCodes.showStartMenu);
        //Debug.Log("INFORMING ABOUT GAME LOGIC CONTROLLER START MENU");

        loadLevel();
        updateCurrentLevel();
    }

    void Awake () {
        enemy.EnemyEventSubscription += enemyDroneEventHappenend;
        MicrophoneInputControler.microphoneOverThreshold += loudNoiseHandler;
    }

    // Update is called once per frame
    void Update() {
    }

    // also subscribed to microphone input noise exceedence event
    void loudNoiseHandler(float db_, Vector3 position_) {
        //Debug.Log("Picked up loud noise"+db_);
    }

    // when drones get killed or else, they will informe this class
    void enemyDroneEventHappenend(int droneType, int whatHappend) {
        Debug.Log("Drone Event Happened");
        if (whatHappend == InGameComunicationCodes.droneHasDied) {
            enemyDroneGotDestroyed(droneType);
        }
    }

    // action to take place when a drone was destroyed
    void enemyDroneGotDestroyed(int droneType) {
        if (droneType == InGameComunicationCodes.droneGuardianType) {
            currentlyActiveDroneGuardians -= 1;
        } else if (droneType == InGameComunicationCodes.droneCollectorType) {
            currentlyActiveDroneCollector -= 1;
        }
        updateCurrentLevel();
    }

    // loading next level data from LevelData class
    void loadLevel() {
        droneGuardiansAtOnce = LevelData.getGAtOnce(currentLevel);
        droneCollectorAtOnce = LevelData.getCAtOnce(currentLevel);
        droneGuardiansTotal = LevelData.getGTotal(currentLevel);
        droneCollectorTotal = LevelData.getCTotal(currentLevel);
        droneGuardiansSpeed = LevelData.getGSpeed(currentLevel);
        droneCollectorSpeed = LevelData.getCSpeed(currentLevel);
    }

    // Update current drone numbers to match specs defined in leveldata
    void updateCurrentLevel() {
        Debug.Log("GAME UPDATE LEVEL:" +(droneGuardiansAtOnce - currentlyActiveDroneGuardians) +
            " -- " + droneGuardiansTotal);
        int generateNumberOfGuardians = System.Math.Min(
            (droneGuardiansAtOnce - currentlyActiveDroneGuardians), droneGuardiansTotal);
        int generateNumberOfCollector = System.Math.Min(
            (droneCollectorAtOnce - currentlyActiveDroneCollector), droneCollectorTotal);
        for (int i = 0; i < generateNumberOfGuardians; i++) {
            droneGuardiansTotal -= 1;
            currentlyActiveDroneGuardians += 1;
            GameObject createdEnemy = GameObject.Instantiate(enemyDroneGuardian);
            createdEnemy.GetComponent<MoveAlongPath>().currentlyActive = true;
            createdEnemy.GetComponent<enemy>().setActiveListener();
            Debug.Log("Created drone guardian");
        }
        for (int i = 0; i < generateNumberOfCollector; i++) {
            droneCollectorTotal -= 1;
            currentlyActiveDroneCollector += 1;
            GameObject createdEnemy = GameObject.Instantiate(enemyDroneCollector);
            createdEnemy.GetComponent<MoveAlongPath>().currentlyActive = true;
            createdEnemy.GetComponent<enemy>().setActiveListener();
        }
        timeWaitLimitEnemyCreation = inGameLimitEnemyCreation;
        if (droneGuardiansTotal == 0 && droneCollectorTotal == 0) {
            oldLevelCompleted();
        }
    }

    // increase one level when old one was completed
    void oldLevelCompleted() {
        currentLevel += 1;
        Debug.Log("Changed Level to: "+currentLevel);
        loadLevel();
        updateCurrentLevel();
    }
}
