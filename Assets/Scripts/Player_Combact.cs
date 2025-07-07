using UnityEngine;

public class Player_Combact : MonoBehaviour
{
    private Animator anim;
    
    public float cooldown = 2f;
    public Transform attackPoint;
    public float weaponRange = 1;
    public LayerMask enemyLayer;
    public int damage = 1;
    public float knockbackForce = 5f;
    public float stunTime = 1f;

    private float timer;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);
            
            timer = cooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, stunTime);
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }
}
