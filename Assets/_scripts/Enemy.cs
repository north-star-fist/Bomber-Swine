using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, Damageable
{

    public BoxCollider fieldBounds;

    public float destinationRefreshTime = 4;

    public Renderer[] renderers;
    public Material healthyMat;
    public Material stinkyMat;

    float prevDestinationUpdateTime;
    float prevStinkyTime;

    NavMeshAgent nmAgent;


    private const float STINKY_PERIOD = 5f;

    private const int RANDOM_DESTINATION_ATTEMPTS = 32;

    NavMeshPath nmPath;

    float initSpeed;

    // Start is called before the first frame update
    void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        initSpeed = nmAgent.speed;
        nmPath = new NavMeshPath();
    }

    // Update is called once per frame
    void Update() {
        updateNavDestination();
        updateMaterial();
    }

    public void OnDamage(float damaveValue) {
        //just changing material 
        setMaterial(stinkyMat);
        nmAgent.speed  = initSpeed / 2;
        prevStinkyTime = Time.time;
    }

    private void updateMaterial() {
        if (Time.time - prevStinkyTime > STINKY_PERIOD) {
            if (setMaterial(healthyMat)) {
                nmAgent.speed = initSpeed;
            }
        }
    }

    private void updateNavDestination() {
        if (Time.time - prevDestinationUpdateTime > destinationRefreshTime) {
            float xMin = fieldBounds.bounds.min.x;
            float xMax = fieldBounds.bounds.max.x;
            float zMin = fieldBounds.bounds.min.z;
            float zMax = fieldBounds.bounds.max.z;
            
            int attempts = 0;
            bool pathWasFound = false;
            do {
                Vector3 randDest = new Vector3(Random.Range(xMin, xMax), 0, Random.Range(zMin, zMax));
                pathWasFound = nmAgent.CalculatePath(randDest, nmPath);
                attempts++;
            } while (!pathWasFound && attempts < RANDOM_DESTINATION_ATTEMPTS);
            if (pathWasFound) {
                nmAgent.SetPath(nmPath);
                prevDestinationUpdateTime = Time.time;
            } else {
                Debug.LogFormat(this, "{0} could not find new navmesh path", this);
            }
        }
    }

    private bool setMaterial(Material mat) {
        if (renderers == null || renderers.Length == 0) {
            Debug.LogWarningFormat(this, "{0} has no renderer!", this);
            return false;
        }
        foreach (var rend in renderers) {
            if (rend == null) {
                continue;
            }
            //guessing there is just one material
            if (rend.sharedMaterials[0] != mat) {
                //Debug.LogFormat(this, "{0} is changing {1} material to {2}", this, rrendererr.sharedMaterials[0], mat);
                Material[] rendMats = rend.sharedMaterials;
                rendMats[0] = mat;
                rend.sharedMaterials = rendMats;
                return true;
            }
        }
        return false;
    }
}
