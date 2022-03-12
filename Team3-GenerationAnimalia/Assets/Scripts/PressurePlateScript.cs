using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PressurePlateScript : MonoBehaviour
{
    [Header("Private Variables")]
    private Tilemap tilemap;
    private TilemapCollider2D tileColl;
    private TileBase[] allTile;
    private BoundsInt tileBounds;
    private bool triggerStatus;

    [Header("Debug")]
    [SerializeField]
    private bool plateState;

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
        plateState = false;
        tilemap = GetComponent<Tilemap>();
        tileColl = GetComponent<TilemapCollider2D>();
        tileColl.isTrigger = true;
        tileBounds = tilemap.cellBounds;
        allTile = tilemap.GetTilesBlock(tileBounds);
    }

    // Update is called once per frame
    void Update()
    {
       foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                if (plateState)
                {
                    Vector3Int coordinates = new Vector3Int(pos.x, pos.y, pos.z);
                    Color tileColour = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                    tilemap.SetTile(coordinates, onTile);
                    UpdateAffectedTiles(false, tileColour, onMask);
                }
                else
                {
                    Vector3Int coordinates = new Vector3Int(pos.x, pos.y, pos.z);
                    Color tileColour = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    tilemap.SetTile(coordinates, offTile);
                    UpdateAffectedTiles(true, tileColour, offMask);
                }
            }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "PushableObject")
        {
            plateState = true;
            Debug.Log("Presure Plate Active");
        }
    }

        private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "PushableObject")
        {
            plateState = false;
            Debug.Log("Presure Plate Deactive");
        }
    }
}
