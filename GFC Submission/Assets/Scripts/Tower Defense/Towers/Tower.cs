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

    [SerializeField] private int maxLevel;
    public int MaxLevel { get => maxLevel; private set => maxLevel = value; }
    [SerializeField] private float[] firerates;
    [SerializeField] private float[] damages;
    [SerializeField] private float[] ranges;
    [Tooltip("Cost for each upgrade")]
    [SerializeField] private int[] upgradeCosts;
    public int[] UpgradeCosts {get => upgradeCosts; private set => upgradeCosts = value; }
    private int level;
    public int Level {get => level; private set => level = value; }

    private float attackCooldown;
    private float moneySpent = 0;
    [SerializeField] private LayerMask enemyLayer;

    private void Start()
    {
        if (firerates.Length != maxLevel || damages.Length != maxLevel || ranges.Length != maxLevel || upgradeCosts.Length != maxLevel - 1)
            Debug.LogError("One or more upgrade arrays on tower: \"" + gameObject.name + "\" are of the wrong size!");
    }

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
        if (level < maxLevel - 1)
        {
            if (MoneyManager.instance.Money >= upgradeCosts[level])
            {
                MoneyManager.instance.ChangeMoney(-upgradeCosts[level]);
                moneySpent += upgradeCosts[level];

                level++;
                firerate = firerates[level];
                damage = damages[level];
                range = ranges[level];
            }
            else
            {
                //Add in message saying the player ain't rich enough.
                print("Not enough funds.");
            }
        }
        else
            print("Tower is already max level!");
    }
}
