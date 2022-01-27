using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    // each position corresponds to the level we are in
    static int[] droneGuardiansAtOnce = {1, 2, 3, 6};
    static int[] droneCollectorAtOnce = {0, 1, 2, 2};
    static int[] droneGuardiansTotal = {2, 5, 10, 30};
    static int[] droneCollectorTotal = {0, 2, 6, 8};
    static float[] droneGuardiansSpeed = {.005f, .1f, .2f, .4f};
    static float[] droneCollectorSpeed = {.01f, .1f, .2f, .4f};

    static public int getGAtOnce(int level) {
        return droneGuardiansAtOnce[lSize(level)];
    }

    static public int getCAtOnce(int level) {
        return droneCollectorAtOnce[lSize(level)];
    }

    static public int getGTotal(int level) {
        return droneGuardiansTotal[lSize(level)];
    }

    static public int getCTotal(int level) {
        return droneCollectorTotal[lSize(level)];
    }

    static public float getGSpeed(int level) {
        return droneGuardiansSpeed[lSize(level)];
    }

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
