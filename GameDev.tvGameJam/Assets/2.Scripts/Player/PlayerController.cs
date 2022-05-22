using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckLength;

    bool isJumping;

    bool isLookingLeft;

    Rigidbody2D rb;
    Animator anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround();

        OnMove();
        Jump();
    }
#region Movementation
    #region Move
    void OnMove()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);

        anim.SetInteger("Movement", (int)rb.velocity.x);

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
    #endregion

    void Jump()
    {
        anim.SetBool("Jumping", isJumping);
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
        }
    }

    void CheckGround()
    {
        RaycastHit2D ray = Physics2D.Raycast(rb.worldCenterOfMass, Vector2.down, groundCheckLength, 1 << 6);
        Debug.DrawRay(rb.worldCenterOfMass, Vector2.down * groundCheckLength, Color.magenta);

        if(ray.collider != null)
            isJumping = false;
        else
            isJumping = true;
    }

#endregion
}
