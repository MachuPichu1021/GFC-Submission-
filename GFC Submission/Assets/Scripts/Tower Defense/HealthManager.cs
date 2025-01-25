using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    [SerializeField] private float health;
    [SerializeField] private TMP_Text healthText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ChangeHealth(100);
    }

    public void ChangeHealth(float amt)
    {
        health += amt;
        healthText.text = "Health: " + Mathf.RoundToInt(health);
    }
}
