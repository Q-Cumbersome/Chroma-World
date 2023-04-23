using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject enemyAttackPrefab;
    public Transform target;

    public float projectileSpeed;
    public float fireRate;
    public float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            GameObject enemyAttack = Instantiate(enemyAttackPrefab, transform.position, Quaternion.identity);
            enemyAttack.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

            fireTimer = 0f;
        }
    }
}


