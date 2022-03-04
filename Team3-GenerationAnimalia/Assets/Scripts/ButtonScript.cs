using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonScript : MonoBehaviour
{
    public bool buttonOn;
    public TileBase onTile, offTile;

    private Tilemap tilemap;
    private TileBase[] allTile;

    // Start is called before the first frame update
    void Start()
    {
        buttonOn = false;
        tilemap = GetComponent<Tilemap>();
        allTile = tilemap.GetTilesBlock(tilemap.cellBounds);
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonOn)
        {
            foreach (TileBase tile in allTile)
            {

            }
        }

        /*if (Input.GetKeyDown("E") == true)
        {

        }*/
    }
}
