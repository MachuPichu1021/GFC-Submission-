using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    [SerializeField] private float health ;
    public float Health {get => health; private set => health = value; }
    [SerializeField] private TMP_Text healthText;


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        AddBaseHP(100);
    }

    public void SubtractBaseHP(float amt)
    {
        health -= amt;
        healthText.text = "Health: " + Mathf.RoundToInt(health);
    }

    public void AddBaseHP(float amt)
    {
        health += amt;
        healthText.text = "Health: " + Mathf.RoundToInt(health);
    }
}
