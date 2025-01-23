using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    [SerializeField] private float money;
    public float Money { get => money; private set => money = value; }

    [SerializeField] private TMP_Text moneyText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ChangeMoney(500);
    }

    public void ChangeMoney(float amt)
    {
        money += amt;
        moneyText.text = "Money: " + Mathf.RoundToInt(money); 
    }
}
