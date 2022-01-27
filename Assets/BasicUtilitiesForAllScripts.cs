using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Placeholder class to implement basic scripts that could be used by all functions
public class BasicUtilitiesForAllScripts : MonoBehaviour
{
    // Function to randomize order of a list
    static public List<T> Randomize<T>(List<T> list) {
        List<T> randomizedList = new List<T>();
        //System.Random rnd = new System.Random();
        while (list.Count > 0) {
            int index = Random.Range(0, list.Count); //pick a random item from the master list
            randomizedList.Add(list[index]); //place it at the end of the randomized list
            list.RemoveAt(index);
        }
        return randomizedList;
    }
}
