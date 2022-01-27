using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to implement methods for searching game objects
public class GameObjectSearcher : MonoBehaviour
{
	// find all children with certain tag inside a gameObject
    public static List<GameObject> GetChildObject(Transform parent, string _tag)
    {
        List<GameObject> foundChildren = new List<GameObject>();
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                foundChildren.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
        return foundChildren;
    }
}
