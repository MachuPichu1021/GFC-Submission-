using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float attackInterval;
    [SerializeField] private int attackMagnitude;
    [SerializeField] private float range;
    private float attackCooldown;
    private Enemy target;
    private Collider2D[] enemiesInRange;
    private bool isAttacking = false;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        enemiesInRange = Physics2D.OverlapCircleAll(transform.position, range);
        enemiesInRange = enemiesInRange.OrderByDescending(Enemy => Enemy.GetComponent<Enemy>().TrackDistance()).ToArray();

        isAttacking = (enemiesInRange.Length != 0);

        if(isAttacking)
        {
            target = enemiesInRange[0].GetComponent<Enemy>();
        }

        if(target != null)
        {
            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }
        
        attackCooldown -= Time.deltaTime; 

        if (attackCooldown == 0 && isAttacking)
        {
            target.TakeDamage(attackMagnitude);
            attackCooldown = attackInterval;
        }
    }
}
