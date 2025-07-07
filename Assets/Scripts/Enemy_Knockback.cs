using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Knockback : MonoBehaviour
{
    private Enemy_Movement enemyMovement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<Enemy_Movement>();
    }
    
    public void Knockback(Transform playerTransform, float knockbackForce, float stunTime)
    {
        enemyMovement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunTime(stunTime));
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.linearVelocity = direction * knockbackForce;
    }

    IEnumerator StunTime(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        enemyMovement.ChangeState(EnemyState.Idle);
    }
}

