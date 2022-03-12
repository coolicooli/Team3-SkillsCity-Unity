using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToggleButtonScript : MonoBehaviour
{    
    [Header("Private Variables")]
    private Tilemap tilemap;
    private TilemapCollider2D tileColl;
    private TileBase[] allTile;
    private BoundsInt tileBounds;
    private bool triggerStatus;

    [Header("States")]
    [SerializeField]
    private bool buttonOn;
    
    [Header("Refrences")]
    [SerializeField]    
    private TileBase onTile, offTile;
    [SerializeField]
    private GameObject[] affectedTilemaps;

    [SerializeField]
    private LayerMask onMask, offMask;

 
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tileColl = GetComponent<TilemapCollider2D>();
        tileColl.isTrigger = true;
        tileBounds = tilemap.cellBounds;
        allTile = tilemap.GetTilesBlock(tileBounds);
        Debug.Log("Tile Bounds are: " + tileBounds);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                if (buttonOn)
                {
                    Vector3Int coordinates = new Vector3Int(pos.x, pos.y, pos.z);
                    Color tileColour = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    tilemap.SetTile(coordinates, onTile);
                    UpdateAffectedTiles(true, tileColour, onMask);
                }
                else
                {
                    Vector3Int coordinates = new Vector3Int(pos.x, pos.y, pos.z);
                    Color tileColour = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                    tilemap.SetTile(coordinates, offTile);
                    UpdateAffectedTiles(false, tileColour, offMask);
                }
            }
        }

        

        if (triggerStatus)
        {
            if (Input.GetKeyDown("e"))
            {
                buttonOn = !buttonOn;
            }
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

    private void UpdateAffectedTiles(bool colState, Color visibleColour, LayerMask mask)
    {
        foreach (GameObject GameObj in affectedTilemaps)
        {
            TilemapCollider2D tempCollider = GameObj.GetComponent<TilemapCollider2D>();
            Tilemap tempTilemap = GameObj.GetComponent<Tilemap>();

            if (tempCollider != null)
            {
                tempCollider.enabled = colState;
                GameObj.layer = mask;
            }
            else
            {
                Debug.Log("No Collider Attached");
            }

            if (tempTilemap != null)
            {
                tempTilemap.color = visibleColour;
            }
            else
            {
                Debug.Log("No Tilemap Attached");
            }
        }
    }
}
