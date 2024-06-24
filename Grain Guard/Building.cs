using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; set; }
    public BoundsInt area;
    public GameObject realTower;
    public int buildingCost;

    Punten punten;
    GridSystem gridSystem;
    private AudioManager audioManager;

    private void Start()
    {
        punten = FindObjectOfType<Punten>();
        gridSystem = FindObjectOfType<GridSystem>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public void Place()
    {
        Vector3Int positionInt = GridSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        GridSystem.current.TakeArea(areaTemp);
        foreach (var pos in areaTemp.allPositionsWithin)
        {
            if (LandManager.current != null && LandManager.current.storedTilesArray != null)
            {
                foreach (var storedTiles in LandManager.current.storedTilesArray)
                {
                    if (storedTiles.ContainsKey(pos))
                    {
                        storedTiles[pos] = GridSystem.TilesType.Green;
                    }
                }
            }
        }
        gridSystem.buildingButtonPrevent = false;
        punten.currentAmount = punten.currentAmount - buildingCost;
        Instantiate(realTower, transform.position, Quaternion.identity);
        audioManager.PlaySFX(audioManager.buildingPlacedSFX, 1);
        Destroy(gameObject);
    }
}
