using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAllLevelObjective : MonoBehaviour
{
    public GameManager gameManager;

    HashSet<GameObject> collectablesSet = new HashSet<GameObject>();


    public void AddCollectable(GameObject o) {
        if (o == null) {
            return;
        }
        collectablesSet.Add(o);
    }

    public void Collect(GameObject o) {
        if (o == null) {
            return;
        }
        collectablesSet.Remove(o);

        if (collectablesSet.Count == 0) {
            gameManager?.StopTheGame(true);
        }
    }
}
