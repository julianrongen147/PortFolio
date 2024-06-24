using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.UI;

public class GridSystem : MonoBehaviour
{
    public static GridSystem current;

    public GridLayout gridLayout;
    public Tilemap mainTileMap;
    public Tilemap tempTileMap;

    public Dictionary<TilesType, TileBase> tileBases = new Dictionary<TilesType, TileBase>();

    private Building temp;
    private Vector3 prevPos;
    private BoundsInt prevArea;

    public bool canClickUpgrade = true;

    public bool buildingButtonPrevent;

    private Vector3Int selectionStart;
    private Vector3Int selectionEnd;
    private bool isSelecting = false;
    private Rect selectionRect;

    public Button confirmButton;
    public Button cancelButton;
    public TextMeshProUGUI costText; // Reference to the TextMeshPro text element

    private List<Vector3Int> selectedDestroyedTiles = new List<Vector3Int>();

    public int costPerTile = 100;

    private Punten punten; // Reference to the Punten script

    Building building;

    private void Awake()
    {
        current = this;
        string tilePath = @"Tiles\";

        tileBases.Add(TilesType.Empty, null);
        tileBases.Add(TilesType.White, Resources.Load<TileBase>(tilePath + "white"));
        tileBases.Add(TilesType.Green, Resources.Load<TileBase>(tilePath + "green"));
        tileBases.Add(TilesType.Red, Resources.Load<TileBase>(tilePath + "red"));
        tileBases.Add(TilesType.Destroyed, Resources.Load<TileBase>(tilePath + "destroyed"));
        tileBases.Add(TilesType.Select, Resources.Load<TileBase>(tilePath + "select"));

        confirmButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        costText.gameObject.SetActive(false); // Initially hide the cost text

        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);

        // Find the Punten script in the scene
        punten = FindObjectOfType<Punten>();
        building = FindObjectOfType<Building>();
    }

    private void Update()
    {
        if (!temp)
        {
            HandleSelection();
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return;
        }
        if (!temp.Placed)
        {
            canClickUpgrade = false;
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellpos = gridLayout.LocalToCell(touchPos);

            if (prevPos != cellpos)
            {
                temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellpos + new Vector3(.5f, .5f, 0f));
                prevPos = cellpos;
                FollowBuilding();
            }

            if (Input.GetMouseButtonDown(1))
            {
                buildingButtonPrevent = false;
                canClickUpgrade = true;
                ClearArea();
                Destroy(temp.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (temp.CanBePlaced())
            {
                canClickUpgrade = true;
                temp.Place();
            }
        }
    }

    private void HandleSelection()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selectionStart = gridLayout.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            selectionStart.z = 0;
            isSelecting = true;
        }

        if (Input.GetMouseButton(1) && isSelecting)
        {
            selectionEnd = gridLayout.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            selectionEnd.z = 0;
            DrawSelectionBox();
        }

        if (Input.GetMouseButtonUp(1) && isSelecting)
        {
            isSelecting = false;
            SelectDestroyedTiles();
            ClearSelectionBox();

            if (selectedDestroyedTiles.Count > 0)
            {
                int totalCost = selectedDestroyedTiles.Count * costPerTile;
                costText.text = "Cost: " + totalCost + " points";
                costText.gameObject.SetActive(true);

                confirmButton.gameObject.SetActive(true);
                cancelButton.gameObject.SetActive(true);
            }
        }
    }

    private void DrawSelectionBox()
    {
        ClearSelectionBox();
        BoundsInt selectionBounds = new BoundsInt(
            Vector3Int.Min(selectionStart, selectionEnd),
            Vector3Int.Max(selectionStart, selectionEnd) - Vector3Int.Min(selectionStart, selectionEnd) + Vector3Int.one);

        SetTilesBlock(selectionBounds, TilesType.Select, tempTileMap);

        Vector3 startScreenPos = Camera.main.WorldToScreenPoint(gridLayout.CellToWorld(selectionStart));
        Vector3 endScreenPos = Camera.main.WorldToScreenPoint(gridLayout.CellToWorld(selectionEnd));

        selectionRect = new Rect(
            Mathf.Min(startScreenPos.x, endScreenPos.x),
            Screen.height - Mathf.Max(startScreenPos.y, endScreenPos.y),
            Mathf.Abs(startScreenPos.x - endScreenPos.x),
            Mathf.Abs(startScreenPos.y - endScreenPos.y)
        );
    }

    private void ClearSelectionBox()
    {
        tempTileMap.ClearAllTiles();
    }

    private void SelectDestroyedTiles()
    {
        selectedDestroyedTiles.Clear();
        BoundsInt selectionBounds = new BoundsInt(
            Vector3Int.Min(selectionStart, selectionEnd),
            Vector3Int.Max(selectionStart, selectionEnd) - Vector3Int.Min(selectionStart, selectionEnd) + Vector3Int.one);

        TileBase[] selectedTiles = GetTilesBlock(selectionBounds, mainTileMap);

        for (int i = 0; i < selectedTiles.Length; i++)
        {
            if (selectedTiles[i] == tileBases[TilesType.Destroyed])
            {
                Vector3Int selectionOffset = new Vector3Int(i % selectionBounds.size.x, (i / selectionBounds.size.x), 0);
                Vector3Int tilePos = selectionBounds.min + selectionOffset;
                selectedDestroyedTiles.Add(tilePos);
            }
        }
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    public void SetTilesBlock(BoundsInt area, TilesType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, tileBases, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private void FillTiles(TileBase[] arr, Dictionary<TilesType, TileBase> tileBases, TilesType type)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }

    public void InitializeWithBuilding(GameObject building)
    {
        temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        buildingButtonPrevent = true;
        FollowBuilding();
    }

    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, tileBases, TilesType.Empty);
        tempTileMap.SetTilesBlock(prevArea, toClear);
    }

    private void FollowBuilding()
    {
        ClearArea();

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTileMap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TilesType.White])
            {
                tileArray[i] = tileBases[TilesType.Green];
            }
            else
            {
                tileArray[i] = tileBases[TilesType.Red];
            }
        }

        tempTileMap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, mainTileMap);
        foreach (var b in baseArray)
        {
            if (b != tileBases[TilesType.White])
            {
                Debug.Log("Cannot Place");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TilesType.Empty, tempTileMap);
        SetTilesBlock(area, TilesType.Green, mainTileMap);
    }

    private void OnConfirmButtonClick()
    {
        int totalCost = selectedDestroyedTiles.Count * costPerTile;

        if (punten.currentAmount >= totalCost)
        {
            punten.currentAmount -= totalCost;

            foreach (var tilePos in selectedDestroyedTiles)
            {
                mainTileMap.SetTile(tilePos, tileBases[TilesType.White]);
            }

            // Update LandManager's stored tiles
            LandManager.current.UpdateStoredTiles(selectedDestroyedTiles, TilesType.White);

            selectedDestroyedTiles.Clear();
            confirmButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            costText.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough points to convert tiles.");
        }
    }

    private void OnCancelButtonClick()
    {
        selectedDestroyedTiles.Clear();
        confirmButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        costText.gameObject.SetActive(false);
    }

    public enum TilesType
    {
        Empty,
        White,
        Green,
        Red,
        Destroyed,
        Select
    }
}
