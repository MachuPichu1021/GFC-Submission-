using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTower : MonoBehaviour 
{
    [SerializeField] private GameObject selectedTower;
    [SerializeField] private GameObject selectedTowerPreview;
    private Vector2 mousePos;
    private bool onClick = false;

    public void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log(mousePos.x + " " + mousePos.y);
        
        selectedTowerPreview.transform.position = mousePos;
        
        if (onClick)
        {
           if (MoneyManager.instance.Money >= selectedTower.GetComponent<Tower>().PlacementCost && Input.GetMouseButtonDown(0))
            {
                Instantiate(selectedTower, mousePos, Quaternion.identity);
                MoneyManager.instance.ChangeMoney(-selectedTower.GetComponent<Tower>().PlacementCost);
                selectedTowerPreview.SetActive(false);
                onClick = false;
            } 
        }


    }

    public void PreviewTower()
    {
        selectedTowerPreview.GetComponent<SpriteRenderer>().sprite = selectedTower.GetComponent<SpriteRenderer>().sprite;
        onClick = true;
        selectedTowerPreview.SetActive(true);
    }
}
