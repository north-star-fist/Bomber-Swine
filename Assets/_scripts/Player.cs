using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, Damageable
{

    const int MAX_EATEN_BOMBS = 10;
    const float TIME_TO_BE_STINKY = 10f;
    const string DETAIL_ALBEDO_WEIGHT_PROP = "_DetailAlbedoMapScale";

    public float speed = 1f;
    //better to be pooled but.. who cares. It's just test task
    public GameObject bombPrefab;
    public Transform bombSpawnPosition;

    public Text bombsCounterText;
    public string eatableTag = "OmNomNom";

    public Renderer[] renderersForDirtyMask;

    [SerializeField]//optional just to see it in editor
    int bombsNumber;

    float curSpeed;

    float stinkyTimer;

    CharacterController charControl;

    // Start is called before the first frame update
    void Start() {
        charControl = GetComponent<CharacterController>();
        curSpeed = speed;
    }

    private void Update() {
        if (bombsCounterText != null) {
            bombsCounterText.text = bombsNumber.ToString();
        }

        float slowDownCoeff = (1 - 0.8f * (float)Mathf.Min(bombsNumber, MAX_EATEN_BOMBS) / MAX_EATEN_BOMBS);
        curSpeed = speed * slowDownCoeff + stinkyTimer / TIME_TO_BE_STINKY;

        if (stinkyTimer > 0) {
            stinkyTimer -= Time.deltaTime;
            if (stinkyTimer <= 0) {
                setMatDetailMaskWeight(0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        //check if it's something that could be eaten
        if (!other.CompareTag(eatableTag)) {
            return;
        }
        bombsNumber++;
        other.enabled = false;
        Destroy(other.gameObject);
    }

    public void Move(Vector2 dir) {
        if (dir.sqrMagnitude < 0.001f) {
            return;
        }
        Vector3 dir3d = new Vector3(dir.x, 0, dir.y);
        transform.rotation = Quaternion.LookRotation(dir3d);
        charControl.SimpleMove(dir3d * curSpeed);
    }

    public void DropTheBomb() {
        if (bombsNumber == 0) {
            return;
        }
        Vector3 bombPos = bombSpawnPosition != null ? bombSpawnPosition.position : transform.position;
        Instantiate(bombPrefab, bombPos, transform.rotation);
        bombsNumber--;
    }


    public void OnDamage(float damage) {
        if (renderersForDirtyMask == null || renderersForDirtyMask.Length == 0) {
            return;
        }

        stinkyTimer = TIME_TO_BE_STINKY;

        setMatDetailMaskWeight(1f);
    }

    private void setMatDetailMaskWeight(float maskWeight) {
        foreach (var rend in renderersForDirtyMask) {
            if (rend == null) {
                continue;
            }
            Material mat = rend.material;
            mat.SetFloat(DETAIL_ALBEDO_WEIGHT_PROP, maskWeight);
        }
    }
}
