using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    enum PlayerState {Alive, Attacking,Dead};
    [Header("Life system", order = 1)]
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Transform blackScreen;
    
    [Header("Movementation", order = 2)]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckLength;

    bool isJumping;
    bool isLookingLeft;

    [Header("Attack", order = 3)]
    [SerializeField] private AnimationClip attackClip;
    [SerializeField] private float animationOffSet;
    [SerializeField] private Collider2D attackCollider;

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
        
        if(playerState == PlayerState.Alive) {
            OnMove();
            Jump();
            
            OnAttack();
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
        playerState = PlayerState.Dead;

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

#region Attack

    void OnAttack()
    {
        if(Input.GetMouseButtonDown(0))
            StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        playerState = PlayerState.Attacking;

        anim.SetBool("Attacking", true);
        attackCollider.enabled = true;

        yield return new WaitForSeconds(attackClip.length + animationOffSet);

        attackCollider.enabled = false;
        anim.SetBool("Attacking", false);
  
        playerState = PlayerState.Alive;
    }
#endregion

}
