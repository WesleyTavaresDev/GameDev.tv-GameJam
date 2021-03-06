using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerState {Alive, Attacking, Dead};
    public delegate void Dead(int pointsLost);
    public static event Dead dead;
    
    [Header("Life system", order = 1)]
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Transform blackScreen;
    [SerializeField] private LevelController levelController;
    
    [Header("Movementation", order = 2)]
    [SerializeField] private GameObject dust;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckLength;

    bool isJumping;
    bool isLookingLeft;

    [Header("Attack", order = 3)]
    [SerializeField] private AnimationClip attackClip;
    
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

        dust.SetActive(horizontal != 0 && !isJumping ? true : false);

        anim.SetInteger("Movement", (int)rb.velocity.x);

        if(horizontal > 0 && isLookingLeft && playerState == PlayerState.Alive)
            Flip();
        else if (horizontal < 0 && !isLookingLeft && playerState == PlayerState.Alive)
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

        StartCoroutine(levelController.RestartLevel(2));
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
            {OnDie(); dead?.Invoke(-5);}
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
        rb.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(attackClip.length);

        playerState = PlayerState.Alive;
        anim.SetBool("Attacking", false);
  
    }
#endregion

}
