using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float reloadSpeed;
    public float startReloadSpeed;
    
    public Transform  target; // attach a target child object to the main enemy
    public GameObject projectile;

    void Start()
    {
        reloadSpeed = startReloadSpeed;
    }

    void Update()
    {
        if (reloadSpeed <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            reloadSpeed = startReloadSpeed;
        }
        else
        {
            reloadSpeed -= Time.deltaTime;
        }
    }
}
