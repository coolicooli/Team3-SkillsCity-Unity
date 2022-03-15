using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players = new GameObject[3];

    void Update()
    {
        if (players[0].GetComponent<movement.MovementScript2D>().isWin == true && players[1].GetComponent<movement.MovementScript2D>().isWin == true && players[2].GetComponent<movement.MovementScript2D>().isWin == true)
        {
            SceneManager.LoadScene(sceneName: "GameOver");
        }

        // Update is called once per frame
    }
}