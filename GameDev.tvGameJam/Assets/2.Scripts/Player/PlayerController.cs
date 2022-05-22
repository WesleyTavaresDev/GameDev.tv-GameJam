using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;

    bool isLookingLeft;

    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        OnMove();
    }

    void OnMove()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);

        if(horizontal > 0 && isLookingLeft)
            Flip();
        else if (horizontal < 0 && !isLookingLeft)
            Flip();
    }

    void Flip()
    {
        isLookingLeft = !isLookingLeft;

        Vector2 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }
}
