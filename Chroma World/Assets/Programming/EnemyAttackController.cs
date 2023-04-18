using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public float projectileSpeed;

    private Transform target;
    private Vector2 aimAtTarget;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target").transform;

        aimAtTarget = new Vector2(target.position.x, target.position.y);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, aimAtTarget, projectileSpeed * Time.deltaTime);
    }
}
