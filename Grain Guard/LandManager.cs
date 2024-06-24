using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LandManager : MonoBehaviour
{
    public static LandManager current;

    public GameObject[] landObjects;
    public int activationRangeX;
    public int activationRangeY;

    GridSystem gridSystem;
    Building building;

    public Dictionary<Vector3Int, GridSystem.TilesType>[] storedTilesArray;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        gridSystem = FindObjectOfType<GridSystem>();
        building = FindObjectOfType<Building>();
        InitializeStoredTilesArray();
        StoreOriginalLandTiles();
    }

    private void InitializeStoredTilesArray()
    {
        storedTilesArray = new Dictionary<Vector3Int, GridSystem.TilesType>[landObjects.Length];
        for (int i = 0; i < storedTilesArray.Length; i++)
        {
            storedTilesArray[i] = new Dictionary<Vector3Int, GridSystem.TilesType>();
        }
    }

    public void StoreOriginalLandTiles()
    {
        for (int i = 0; i < landObjects.Length; i++)
        {
            GameObject landObject = landObjects[i];
            if (landObject != null)
            {
                Vector3Int landPosition = gridSystem.mainTileMap.WorldToCell(landObject.transform.position);

                BoundsInt bounds = new BoundsInt(
                    landPosition.x - activationRangeX,
                    landPosition.y - activationRangeY,
                    0,
                    activationRangeX * 2,
                    activationRangeY * 2,
                    1
                );

                foreach (Vector3Int position in bounds.allPositionsWithin)
                {
                    if (gridSystem.mainTileMap.GetTile(position) != null) // Check if tile is not empty
                    {
                        GridSystem.TilesType originalTileType = GridSystem.TilesType.White;
                        storedTilesArray[i][position] = originalTileType;
                    }
                }

                // Set tiles to empty after iterating through all positions within bounds
                gridSystem.SetTilesBlock(bounds, GridSystem.TilesType.Empty, gridSystem.mainTileMap);
            }
        }
    }

    private void Update()
    {
        UpdateLandTiles();
    }

    public void UpdateLandTiles()
    {
        for (int i = 0; i < landObjects.Length; i++)
        {
            GameObject landObject = landObjects[i];
            if (landObject == null) // If land object is deleted
            {
                Dictionary<Vector3Int, GridSystem.TilesType> storedTiles = storedTilesArray[i];
                foreach (KeyValuePair<Vector3Int, GridSystem.TilesType> entry in storedTiles)
                {
                    gridSystem.mainTileMap.SetTile(entry.Key, gridSystem.tileBases[entry.Value]);
                }
            }
        }
    }

    public void UpdateStoredTiles(List<Vector3Int> positions, GridSystem.TilesType newType)
    {
        for (int i = 0; i < storedTilesArray.Length; i++)
        {
            Dictionary<Vector3Int, GridSystem.TilesType> storedTiles = storedTilesArray[i];
            foreach (Vector3Int pos in positions)
            {
                if (storedTiles.ContainsKey(pos))
                {
                    storedTiles[pos] = newType;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (landObjects != null)
        {
            Gizmos.color = Color.green;
            foreach (GameObject landObject in landObjects)
            {
                if (landObject != null)
                {
                    Vector3 landPosition = landObject.transform.position;

                    float sizeX = activationRangeX * 2;
                    float sizeY = activationRangeY * 2;

                    Gizmos.DrawWireCube(landPosition, new Vector3(sizeX, sizeY, 0));
                }
            }
        }
    }
}
