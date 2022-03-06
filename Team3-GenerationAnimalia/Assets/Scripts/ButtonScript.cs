using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonScript : MonoBehaviour
{
    public bool buttonOn;
    public TileBase onTile, offTile;
    public GameObject[] affectedTilemaps;

    private Tilemap tilemap;
    private TileBase[] allTile;
    private BoundsInt tileBounds;

    // Start is called before the first frame update
    void Start()
    {
        buttonOn = false;
        tilemap = GetComponent<Tilemap>();
        tileBounds = tilemap.cellBounds;
        allTile = tilemap.GetTilesBlock(tileBounds);
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < tileBounds.size.x; x++) 
        {
            for (int y = 0; y < tileBounds.size.y; y++) 
            {
                TileBase tile = allTile[x + y * tileBounds.size.x];
                if (tile != null) 
                {
                    if (buttonOn)
                    {
                        Vector3Int coordinates = new Vector3Int(x, y, 0);
                        tilemap.SetTile(coordinates, onTile);
                        UpdateTiles(true);
                    }  
                    else
                    {
                        Vector3Int coordinates = new Vector3Int(x, y, 0);
                        tilemap.SetTile(coordinates, offTile);
                        UpdateTiles(false);
                    }
                } 
            }
        }

        /*if (Input.GetKeyDown("E") == true)
        {

        }*/
    }

    void UpdateTiles(bool state)
    {
        foreach (GameObject GameObj in affectedTilemaps)
        {
            TilemapCollider2D tempCollider = GameObj.GetComponent<TilemapCollider2D>();
            if (tempCollider == null)
                Debug.Log("No Collider Attached");
            else
            {
                tempCollider.enabled = state;
            }
        }
    }
}
