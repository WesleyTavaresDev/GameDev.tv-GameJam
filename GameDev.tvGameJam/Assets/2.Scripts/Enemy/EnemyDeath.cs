using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private float distance;

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
            Debug.Log("Vou esquecer, não to bricando não");
            anim.SetBool("Dead", true);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void knockout()
    {
        rb.AddForce(new Vector2((transform.localScale.x > 0 ? 50 : 50 * -1) * Time.fixedDeltaTime, 200 * Time.fixedDeltaTime), ForceMode2D.Impulse);
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
