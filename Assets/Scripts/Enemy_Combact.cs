using UnityEngine;

public class Enemy_Combact : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask playerLayer;
    public float knockBackForce;
    public float stunTime;
    
    //req for enemy body dealing damage
    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);}
    }*/
    

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            hits[0].GetComponent<PlayerMovement_RPG>().Knockback(transform, knockBackForce, stunTime);
        }
    }
}
