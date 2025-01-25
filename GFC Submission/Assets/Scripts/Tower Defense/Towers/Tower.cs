using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private string towerName;
    public string Name { get => towerName; private set => towerName = value; }

    [Tooltip("The time in seconds between each shot")]
    [SerializeField] private float firerate;
    public float Firerate { get => firerate; private set => firerate = value; }
    [Tooltip("How much damage the tower does with each projectile")]
    [SerializeField] private float damage;
    public float Damage { get => damage; private set => damage = value; }
    [Tooltip("How far in units the tower can see (radial wise)")]
    [SerializeField] private float range;
    public float Range {get => range; private set => range = value; }
    [SerializeField] private float firerateIncrease;
    [SerializeField] private float damageIncrease;
    [SerializeField] private float rangeIncrease;
    [SerializeField] private float upgradeCostIncrease;
    [Tooltip("Cost for each upgrade")]
    [SerializeField] private float upgradeCost = 100f;
    public float UpgradeCost {get => upgradeCost; private set => upgradeCost = value; }
    [SerializeField] private int upgradeCount = 0; 
    public int UpgradeCount {get => upgradeCount; private set => upgradeCount = value; }

    private float attackCooldown;
    private float moneySpent = 0;
    [SerializeField] private LayerMask enemyLayer;

    private void FixedUpdate()
    {
        if (attackCooldown <= 0)
        {
            Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
            List<Enemy> enemies = new List<Enemy>();
            foreach (Collider2D coll in colls)
                enemies.Add(coll.GetComponent<Enemy>());

            if (enemies.Count != 0)
            {
                IOrderedEnumerable<Enemy> enemiesSorted = enemies.OrderByDescending(e => e.TrackDistance());
                Enemy target = enemiesSorted.First();
                Attack(target);
            }
        }
        else
            attackCooldown -= Time.fixedDeltaTime;
    }

    private void Attack(Enemy target)
    {
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        target.TakeDamage(damage);
        attackCooldown = firerate;
    }

    public void Sell()
    {
        MoneyManager.instance.ChangeMoney(moneySpent / 2);
        Destroy(gameObject);
    }

    public void Upgrade()
    {
        if (upgradeCount < 6)
        {
            if (MoneyManager.instance.Money >= upgradeCost)
            {
                MoneyManager.instance.ChangeMoney(-upgradeCost);
                upgradeCount++;
                
                moneySpent += upgradeCost; 
                firerate *= (1 - firerateIncrease);
                damage *= damageIncrease; 
                range += rangeIncrease;
                upgradeCost *= upgradeCostIncrease;

                UpdateRange();
                
                firerate = (float)Math.Round(firerate, 2);
                damage = (float)Math.Round(damage, 2);
                upgradeCost = (float)Math.Round(upgradeCost, 0);
            }
        }
    }

    private void UpdateRange()
    {
        transform.GetChild(0).localScale = new Vector3(range, range, range);
    }
}
