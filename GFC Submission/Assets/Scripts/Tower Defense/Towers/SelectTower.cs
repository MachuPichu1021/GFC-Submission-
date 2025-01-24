using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{
    private Tower selectedTower;

    [SerializeField] private GameObject rangeIndicatorPrefab;
    private GameObject rangeIndicator;

    private void Update()
    {
        if (selectedTower != null)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                selectedTower.Sell();
                OnDeselectTower();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.transform != null && hit.transform.TryGetComponent(out Tower t))
            {
                OnDeselectTower();
                selectedTower = t;
                OnSelectTower();
            }
            else if (selectedTower != null)
                OnDeselectTower();
        }
    }

    private void OnSelectTower()
    {
        rangeIndicator = Instantiate(rangeIndicatorPrefab, selectedTower.transform);
        rangeIndicator.transform.localScale *= selectedTower.Range;
    }

    private void OnDeselectTower()
    {
        selectedTower = null;
        Destroy(rangeIndicator);
    }
}
