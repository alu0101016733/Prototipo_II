using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// datafile to hold level data and give methods an easy way to get them
public class LevelData : MonoBehaviour
{
    // each position corresponds to the level we are in
    static int[] droneGuardiansAtOnce = {1, 2, 3, 6};
    static int[] droneCollectorAtOnce = {0, 1, 2, 2};
    static int[] droneGuardiansTotal = {2, 5, 10, 30};
    static int[] droneCollectorTotal = {0, 2, 6, 8};
    static float[] droneGuardiansSpeed = {.005f, .1f, .2f, .4f};
    static float[] droneCollectorSpeed = {.01f, .1f, .2f, .4f};

    // get droneGuardiansAtOnce
    static public int getGAtOnce(int level) {
        return droneGuardiansAtOnce[lSize(level)];
    }

    // get droneCollectorAtOnce
    static public int getCAtOnce(int level) {
        return droneCollectorAtOnce[lSize(level)];
    }

    // get droneGuardiansTotal
    static public int getGTotal(int level) {
        return droneGuardiansTotal[lSize(level)];
    }

    // get droneCollectorTotal
    static public int getCTotal(int level) {
        return droneCollectorTotal[lSize(level)];
    }

    // get droneGuardiansSpeed
    static public float getGSpeed(int level) {
        return droneGuardiansSpeed[lSize(level)];
    }

    // get droneCollectorSpeed
    static public float getCSpeed(int level) {
        return droneCollectorSpeed[lSize(level)];
    }

    // limits size to existent levels
    static int lSize(int level) {
        if (level >= droneGuardiansAtOnce.Length) {
            return droneGuardiansAtOnce.Length - 1;
        } else if (level < 0) {
            return 0;
        }
        return level;
    }
}
