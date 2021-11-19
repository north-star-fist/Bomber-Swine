using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public CollectAllLevelObjective levelObjective;

    public string playerTag = "Player";

    public GameObject objectRoot;

    // Start is called before the first frame update
    void Awake()
    {
        if (objectRoot == null) {
            objectRoot = gameObject;
        }
        levelObjective.AddCollectable(objectRoot);
    }


    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(playerTag)) {
            return;
        }

        levelObjective?.Collect(gameObject);
    }
}
