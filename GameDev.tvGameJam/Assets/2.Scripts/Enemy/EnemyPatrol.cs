using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isLookingLeft;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isLookingLeft)
            rb.velocity = new Vector2(-speed * Time.fixedDeltaTime, rb.velocity.y);
        else
            rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);

        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PatrolLimit"))
        {
            Flip();
        }
    }


    void Flip()
    {
        isLookingLeft = !isLookingLeft;

        Vector2 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }
}


