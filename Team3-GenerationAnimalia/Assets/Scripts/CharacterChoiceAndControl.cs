using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using movement;

public class CharacterChoiceAndControl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characters = new GameObject[3];
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChooseWhichCharacterToControl();
    }

    void ChooseWhichCharacterToControl()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            characters[0].GetComponent<MovementScript2D>().enabled = true;
            characters[1].GetComponent<MovementScript2D>().enabled = false;
            characters[2].GetComponent<MovementScript2D>().enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            characters[0].GetComponent<MovementScript2D>().enabled = false;
            characters[1].GetComponent<MovementScript2D>().enabled = true;
            characters[2].GetComponent<MovementScript2D>().enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            characters[0].GetComponent<MovementScript2D>().enabled = false;
            characters[1].GetComponent<MovementScript2D>().enabled = false;
            characters[2].GetComponent<MovementScript2D>().enabled = true;
        }
    }

}

