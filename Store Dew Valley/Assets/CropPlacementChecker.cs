using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropPlacementChecker : MonoBehaviour
{
    public Transform point;

    public TilemapCollider2D tilemap;
    public TilemapCollider2D tilemapHoe;

    PlayerMovement pMovement;

    public TileBase hoedTile; 
    public TileBase unHoedTile;
    public TileBase seedPlacedTile;

    [HideInInspector]
    public static CropPlacementChecker instance;

    public GameObject seedPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pMovement = PlayerMovement.instance;

    }


    public void UseThis()
    {
        MousePosChecker();

        // Centering the position
        Vector2 newPos = new Vector2(point.position.x, point.position.y) - new Vector2(0.5f, 0.5f);
        newPos = new Vector2(Mathf.CeilToInt(newPos.x), Mathf.CeilToInt(newPos.y));

        // Checking if we are in the tilemap
        bool contacpoints = tilemap.GetComponent<TilemapCollider2D>().OverlapPoint(newPos);
        if (contacpoints)
        {
            // Chanching from float vector to int vector
            Vector3Int hoedTilePos = new Vector3Int((int)newPos.x - 1, (int)newPos.y - 1, 0);

            // Cheching if this is tile is unhoed tile.
            TileBase tileToCheck = tilemap.GetComponent<Tilemap>().GetTile(hoedTilePos);
            if (tileToCheck == unHoedTile)
            {
                // replaceing tile with new tile
                tilemap.GetComponent<Tilemap>().SetTile(hoedTilePos, hoedTile);
            }
        }
    }

    public void PlaceSeed(Item seedToPlace)
    {
        
        MousePosChecker();

        // Centering the position
        Vector2 newPos = new Vector2(point.position.x, point.position.y) - new Vector2(0.5f, 0.5f);
        newPos = new Vector2(Mathf.CeilToInt(newPos.x), Mathf.CeilToInt(newPos.y));

        // Checking if we are in the tilemap
        bool contacpoints = tilemap.GetComponent<TilemapCollider2D>().OverlapPoint(newPos);
        if (contacpoints)
        {
            // Chanching from float vector to int vector
            Vector3Int hoedTilePos = new Vector3Int((int)newPos.x - 1, (int)newPos.y - 1, 0);

            // Cheching if this is tile is unhoed tile.
            TileBase tileToCheck = tilemap.GetComponent<Tilemap>().GetTile(hoedTilePos);

            // Do we have item in inventory and is the tile hoed.
            if (tileToCheck == hoedTile && Inventory.instance.AskToRemoveItemID(seedToPlace.id, 1))
            {
                // replaceing tile with new tile
                tilemap.GetComponent<Tilemap>().SetTile(hoedTilePos, seedPlacedTile);
                // Place seed on tile
                GameObject instance = Instantiate(seedPrefab, new Vector3(newPos.x, newPos.y, 0), Quaternion.identity);
                Seed seed = SeedDatabase.instance.GetItem(seedToPlace.title);
                // Give seed it's values
                instance.GetComponent<Plant_placed>().ChooseSeed(seed);


                AudioManager.instance.PlayClip("PlaceItem");

            }
        }
    }

    public void UnTileLand(Vector2 tilePos)
    {
        MousePosChecker();

        // Chanching from float vector to int vector
        Vector3Int tilePoss = new Vector3Int((int)tilePos.x - 1, (int)tilePos.y - 1, 0);

        // replaceing tile with new tile
        tilemap.GetComponent<Tilemap>().SetTile(tilePoss, unHoedTile);
    }


    public void MousePosChecker()
    {
        // Cheching mouseposition in screen
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToViewportPoint(Input.mousePosition).x, Camera.main.ScreenToViewportPoint(Input.mousePosition).y);

        // Anything that is 0 and above are top and left curters.
        float mpTotalX = mousePosition.y - mousePosition.x;

        // Anything that is 0 and above are top and right curters.
        float mpTotalY = (mousePosition.x - -(mousePosition.y + 1)) - 2;

        // Getting the center position in witch we compare the pointer position
        Transform centerPos = pMovement.transform;

        float toolOffsetPos = FindObjectOfType<UseItem>().toolPositionOffset;

        // Our mouse is top or left of player.
        if (mpTotalX >= 0)
        {
            
            if (mpTotalY >= 0)
            {
                // Our mouse is top of player.
                point.position = new Vector3(centerPos.position.x, centerPos.position.y + toolOffsetPos, 0);
                //Debug.Log(mpTotalX + " x/y " + mpTotalY);
            }
            else
            {
                // Our mouse is left of player.
                point.position = new Vector3(centerPos.position.x - toolOffsetPos, centerPos.position.y, 0);
            }
        }
        else if (mpTotalY >= 0)
        {
            // Our mouse is Right of player.
            point.position = new Vector3(centerPos.position.x + toolOffsetPos, centerPos.position.y, 0);
        }
        else
        {
            // Our mouse is Down of player.
            point.position = new Vector3(centerPos.position.x, centerPos.position.y - toolOffsetPos, 0);
        }
    }
}

