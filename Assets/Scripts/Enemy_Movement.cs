using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;
    //private bool isChasing;
    private int facingDirection = -1;
    private float attackCooldownTimer;
    
    public float speed = 5f;
    public float attackRange = 2;
    public EnemyState enemyState;
    public float attackCooldown = 2f;
    public float playerDetectionRange = 5f;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        if (enemyState != EnemyState.Knockback)
        {
            CheckForPlayer();
            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
            }
            else if (enemyState == EnemyState.Chasing)
            {
                Chasing();
            }

            else if (enemyState == EnemyState.Attacking)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    void Chasing()
    { 
        if ((player.position.x < transform.position.x && facingDirection == -1) ||
          (player.position.x > transform.position.x && facingDirection == 1))
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectionRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            //isChasing = true;

            //if player is in attack range and cooldown is ready
            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }

            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        //exit anim
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);
        
        //update current anim
        enemyState = newState;
        
        //update new anim
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockback,
}