using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemy : MonoBehaviour
{
    enum RollingState {Idle, Rolling};
    [SerializeField] RollingState state; 
    [SerializeField] private float distance;

    [SerializeField] private float speed;

    Rigidbody2D rb;
    Animator anim;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch(state)
        {
            case RollingState.Idle:
               PlayerCheckDistance();
            break;

            case RollingState.Rolling:
                Roll();
                anim.SetBool("Rolling", true);
            break;
        }

    }


    void PlayerCheckDistance()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.localScale.x > 0 ? Vector2.left : Vector2.right, distance, 1 << 8);
        Debug.DrawRay(transform.position, (transform.localScale.x > 0 ? Vector2.left : Vector2.right) * distance, Color.green);    

        if(ray.collider != null)
            state = RollingState.Rolling;    
    }

    void Roll()
    {
        rb.velocity = new Vector2(((transform.localScale.x > 0 ? -1 : 1) * speed * Time.deltaTime), rb.velocity.y);
    }
}
