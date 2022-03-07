using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonScript : MonoBehaviour
{    
    private Tilemap tilemap;
    private TilemapCollider2D tileColl;
    private TileBase[] allTile;
    private BoundsInt tileBounds;
    private bool triggerStatus;


    [SerializeField]
    private bool buttonOn;
    [SerializeField]
    private TileBase onTile, offTile;
    [SerializeField]
    private GameObject[] affectedTilemaps;

 
    // Start is called before the first frame update
    void Start()
    {
        buttonOn = false;
        tilemap = GetComponent<Tilemap>();
        tileColl = GetComponent<TilemapCollider2D>();
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

        if (triggerStatus)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggerStatus = true;
            Debug.Log("Entered trigger");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggerStatus = false;
            Debug.Log("Exited trigger");
        }
    }

    private void UpdateTiles(bool state)
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
