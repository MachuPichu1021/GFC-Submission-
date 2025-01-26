using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectTower : MonoBehaviour
{
    private Tower selectedTower;

    [SerializeField] private GameObject rangeIndicatorPrefab;
    private GameObject rangeIndicator;

    [SerializeField] private GameObject towerSelectUI;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text firerateText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text rangeText;
    [SerializeField] private TMP_Text upgradeCostText;
    [SerializeField] private Button UICloseButton;


    private void Start()
    {
        UICloseButton.onClick.AddListener(OnDeselectTower);
        towerSelectUI.SetActive(false);
    }

    private void Update()
    {
        if (selectedTower != null)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                selectedTower.Sell();
                OnDeselectTower();
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                selectedTower.Upgrade();
                rangeIndicator.transform.localScale = Vector3.one * selectedTower.Range;
                UpdateUI();
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
            else if (selectedTower != null && EventSystem.current.currentSelectedGameObject != towerSelectUI)
                OnDeselectTower();
        }
    }

    private void OnSelectTower()
    {
        rangeIndicator = Instantiate(rangeIndicatorPrefab, selectedTower.transform);
        rangeIndicator.transform.localScale = Vector3.one * selectedTower.Range;
        towerSelectUI.SetActive(true);
        UpdateUI();
    }

    private void OnDeselectTower()
    {
        selectedTower = null;
        Destroy(rangeIndicator);
        towerSelectUI.SetActive(false);
    }

    private void UpdateUI()
    {
        nameText.text = selectedTower.Name;
        firerateText.text = selectedTower.Firerate.ToString();
        damageText.text = selectedTower.Damage.ToString();
        rangeText.text = selectedTower.Range.ToString();
        upgradeCostText.text = (selectedTower.Level < selectedTower.MaxLevel - 1) ? selectedTower.UpgradeCosts[selectedTower.Level].ToString() : "MAX";
    }
}
