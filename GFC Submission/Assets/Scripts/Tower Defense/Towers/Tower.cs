using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Tooltip("The time in seconds between each shot")]
    [SerializeField] private float firerate;
    [Tooltip("How much damage the tower does with each projectile")]
    [SerializeField] private float damage;
    [Tooltip("How far in units the tower can see (radial wise)")]
    [SerializeField] private float range;
    private float attackCooldown;
    private float moneySpent = 0;

    [SerializeField] private LayerMask enemyLayer;
    // Matthew add a collider to Test Tower
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hovering = Physics2D.OverlapPoint(mousePos);
        
        if (hovering != null && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(WaitForUserInput());
        }
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

    private IEnumerator WaitForUserInput()
    {   
        while (!Input.GetKeyDown(KeyCode.X))
        {
            yield return null;
        }

        SellTower();
    }
    
    private void Attack(Enemy target)
    {
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        target.TakeDamage(damage);
        attackCooldown = firerate;
    }

    private void SellTower()
    {
        MoneyManager.instance.ChangeMoney(moneySpent / 2);
        Destroy(this.gameObject);
    }
}
