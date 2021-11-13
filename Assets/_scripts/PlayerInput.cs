using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    

    public string inputHorAxis = "Horizontal";
    public string inputVerAxis = "Vertical";

    public string bombButton = "Jump";

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float horInp = Input.GetAxis(inputHorAxis);
        float verInp = Input.GetAxis(inputVerAxis);

        Vector3 dir = new Vector3(horInp, verInp);
        player.Move(dir);

        if (Input.GetButtonUp(bombButton)) {
            player.DropTheBomb();
        }
    }

}
