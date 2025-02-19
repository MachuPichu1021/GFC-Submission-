using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTower : MonoBehaviour 
{
    [SerializeField] private GameObject[] towers;
    [SerializeField] private GameObject selectedTower;
    [SerializeField] private GameObject selectedTowerPreview;
    private Vector2 mousePos;
    
    private void Start()
    {   

    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        Debug.Log(hit.collider?.tag);
        
        if (hit.collider != null && Input.GetMouseButtonDown(0) && hit.collider.gameObject.layer == 5)
        {
            string tag = hit.collider.tag;

            for (int i = 0; i < towers.Length; i++)
            {
                if (towers[i].CompareTag(tag))
                {
                    selectedTower = towers[i];
                    break;
                }
            }
        }

        if (selectedTower != null)
        {
            PreviewTower();
            
            if (hit.collider == null)
            {
                PlaceTower();
            }
        }
    }

    private void PreviewTower()
    {  
        if (selectedTower != null)
        {
            selectedTowerPreview.GetComponent<SpriteRenderer>().sprite = selectedTower.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            selectedTowerPreview.GetComponent<SpriteRenderer>().sprite = null;
        }

        selectedTowerPreview.transform.position = mousePos;
    }

    private void PlaceTower()
    {
        if (MoneyManager.instance.Money >= selectedTower.GetComponent<Tower>().PlacementCost && Input.GetMouseButtonDown(0))
        {
            Instantiate(selectedTower, mousePos, Quaternion.identity);
            MoneyManager.instance.ChangeMoney(-selectedTower.GetComponent<Tower>().PlacementCost);
            selectedTower = null;
        }
    }
}
