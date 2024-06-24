using UnityEngine;

public class TowerClickHandler : MonoBehaviour
{
    GridSystem gridSystem;
    BuildingXP currentSelectedTower;

    private void Start()
    {
        gridSystem = FindObjectOfType<GridSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && gridSystem.canClickUpgrade)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Building"));

            if (hit.collider != null)
            {
                BuildingXP clickedTower = hit.collider.GetComponent<BuildingXP>();
                //BuildingXP clickTower = hit.collider.GetComponent<BuildingXP>();
                if (clickedTower != null)
                {
                    if (currentSelectedTower != null && currentSelectedTower != clickedTower)
                    {
                        currentSelectedTower.HideUpgradeMenu();
                    }
                    clickedTower.OnTowerClicked();
                    currentSelectedTower = clickedTower;
                }
            }
            else
            {
                if (currentSelectedTower != null)
                {
                    currentSelectedTower.HideUpgradeMenu();
                    currentSelectedTower = null;
                }
            }
        }
        //if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && gridSystem.canClickUpgrade)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    int layerMask = LayerMask.GetMask("Building");

        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        //    {
        //        BuildingXP clickedTower = hit.collider.GetComponent<BuildingXP>();
        //        if (clickedTower != null)
        //        {
        //            if (currentSelectedTower != null && currentSelectedTower != clickedTower)
        //            {
        //                currentSelectedTower.HideUpgradeMenu();
        //            }

        //            clickedTower.OnTowerClicked();
        //            currentSelectedTower = clickedTower;
        //        }
        //    }
        //    else
        //    {
        //        if (currentSelectedTower != null)
        //        {
        //            currentSelectedTower.HideUpgradeMenu();
        //            currentSelectedTower = null;
        //        }
        //    }
        //}
    }
}



