using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private float distance;

    [SerializeField] private GameObject enemyReward;
    [Range(0, 20)] [SerializeField] private int rewardNumber;

    Rigidbody2D rb;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    } 

    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(rb.worldCenterOfMass, GetDirection(), distance, 1 << 7); 
        Debug.DrawRay(rb.worldCenterOfMass, GetDirection() * distance, Color.cyan);

        if(ray.collider != null)   
        {
            anim.SetBool("Dead", true);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void EnemyReward()
    {
        if(enemyReward != null)            
        {
            for(int i = 0; i < rewardNumber; i++)
                Instantiate(enemyReward, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Abism"))
            Destroy(this.gameObject);
    }

    Vector2 GetDirection()
    {
        if(transform.localScale.x > 0)
            return Vector2.left;
        else if(transform.localScale.x < 0)
            return Vector2.right;
        return Vector2.right;
    }   
}
