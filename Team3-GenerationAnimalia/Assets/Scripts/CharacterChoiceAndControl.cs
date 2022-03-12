using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using movement;

public class CharacterChoiceAndControl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characters = new GameObject[3];

    [SerializeField]
    private Camera camera;
    private Vector3 CameraPos = new Vector3(0.0f, 0.0f, -0.5f);
    private bool P1Control = false, P2Control = true, P3Control = false;

    public bool P1Win, P2Win, P3Win;


    void Start()
    {
        characters[0].GetComponent<MovementScript2D>().enabled = true;
        CameraPos = characters[0].transform.position;
        CameraPos.z =- 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            P1Control = true;
            P2Control = false;
            P3Control = false;
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            P1Control = false;
            P2Control = true;
            P3Control = false;
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            P1Control = false;
            P2Control = false;
            P3Control = true;           
        }

        UpdateCharactersCamera();
    }

    void UpdateCharactersCamera()
    {
        if (P1Control)
        {
            characters[0].GetComponent<MovementScript2D>().enabled = true;
            characters[1].GetComponent<MovementScript2D>().enabled = false;
            characters[2].GetComponent<MovementScript2D>().enabled = false;
            CameraPos = characters[0].transform.position;
            CameraPos.z =- 1.5f;


        }

        if (P2Control)
        {
            characters[0].GetComponent<MovementScript2D>().enabled = false;
            characters[1].GetComponent<MovementScript2D>().enabled = true;
            characters[2].GetComponent<MovementScript2D>().enabled = false;

            CameraPos = characters[1].transform.position;
            CameraPos.z =- 1.5f;

        }

        if (P3Control)
        {
            characters[0].GetComponent<MovementScript2D>().enabled = false;
            characters[1].GetComponent<MovementScript2D>().enabled = false;
            characters[2].GetComponent<MovementScript2D>().enabled = true;

            CameraPos = characters[2].transform.position;
            CameraPos.z =- 1.5f;

        }

        camera.GetComponent<CameraFollow>().target = CameraPos;
    }
}

