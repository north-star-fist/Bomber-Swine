using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, Damageable
{
    public float timer;
    public GameObject explosionPrefab;
    public GameObject explosionWavePrefab;

    float startTime;

    const float DESTROY_DELAY = 5;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - startTime <= timer) {
            return;
        }

        boom();
    }

    public void OnDamage(float damage) {
        //Time to chain boom!
        boom();
    }

    private void boom() {
        //boom
        //pool can be used here
        GameObject expGo = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject.Destroy(expGo, DESTROY_DELAY);
        //pool can be used here
        Quaternion turn90 = Quaternion.LookRotation(Vector3.right);
        Quaternion rot = Quaternion.identity;
        GameObject dirN = Instantiate(explosionWavePrefab, transform.position, rot);
        GameObject.Destroy(dirN, DESTROY_DELAY);
        rot = turn90 * rot;
        GameObject dirE = Instantiate(explosionWavePrefab, transform.position, rot);
        GameObject.Destroy(dirE, DESTROY_DELAY);
        rot = turn90 * rot;
        GameObject dirS = Instantiate(explosionWavePrefab, transform.position, rot);
        GameObject.Destroy(dirS, DESTROY_DELAY);
        rot = turn90 * rot;
        GameObject dirW = Instantiate(explosionWavePrefab, transform.position, rot);
        GameObject.Destroy(dirW, DESTROY_DELAY);

        Destroy(gameObject);
    }
}
