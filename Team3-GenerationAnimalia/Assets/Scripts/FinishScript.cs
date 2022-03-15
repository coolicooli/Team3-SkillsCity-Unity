using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{ 

    void Start()
    {
        GetComponent<TilemapCollider2D>().isTrigger = true;
    }
    private void Update()
    {
     
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<movement.MovementScript2D>().isWin = true;
         
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<movement.MovementScript2D>().isWin = false;
        }
    }
}
