using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    enum LifeState {Alive, Dead};
    [SerializeField] private LifeState lifeState;
    [SerializeField] private Transform blackScreen;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckLength;

    bool isJumping;

    bool isLookingLeft;

    Rigidbody2D rb;
    Animator anim;
    Collider2D coll;
    void Awake()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround();
        
        if(lifeState == LifeState.Alive) {
            OnMove();
            Jump();
        }
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
        anim.SetFloat("Jump", rb.velocity.y);
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

#region Life
    
    void OnDie()
    {
        lifeState = LifeState.Dead;

        rb.velocity = Vector2.zero;
        rb.AddForce( new Vector2( (isLookingLeft ? 70 : 70 * -1) * Time.fixedDeltaTime, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);

        coll.isTrigger = true;
        anim.SetBool("Dead", true);

      //  blackScreen.DOScale(new Vector3(18, 11, 0), 0.1f);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
            OnDie();
    }
#endregion
}
