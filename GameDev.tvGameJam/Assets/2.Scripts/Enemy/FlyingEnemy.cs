using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    enum EnemyState {Idle, Flying};
    [SerializeField] private EnemyState enemyState;
    [SerializeField] private GameObject player;

    [SerializeField] private float speed;
    [SerializeField] private float triggerDistance;

    private Vector3 startPos;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        startPos = transform.position;
    }

    void Update()
    {
        ChangeState();

        switch (enemyState)
        {
            case EnemyState.Idle:
                Flip(startPos);
                if(this.transform.position != startPos)    
                    Fly(startPos);
                anim.SetBool("Attacking", false);
                    break;

            case EnemyState.Flying:
                Flip(player.transform.position);
                Fly(player.transform.position);

                anim.SetBool("Attacking", IsTargetClose(player.transform.position, triggerDistance / 2f) ? true : false );
                    break;
        }
    }

    void ChangeState() => enemyState = IsTargetClose(player.transform.position, triggerDistance) ? EnemyState.Flying : EnemyState.Idle;

    void Fly(Vector2 target) => transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

    void OnColliderEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player") && enemyState == EnemyState.Flying)
            enemyState = EnemyState.Idle;
    }

    void Flip(Vector2 target) => transform.localScale = new Vector2((IsFacingTheSameWay(target) ? 1 : -1), transform.localScale.y);
    void Test_Flip(Vector2 target) => transform.localScale = new Vector2(transform.localScale.x * )
    bool IsTargetClose(Vector2 target, float distance) => Vector2.Distance(player.transform.position, this.gameObject.transform.position) <= distance;
    
    bool IsFacingTheSameWay(Vector2 target) => Mathf.Round(Vector2.Dot(this.transform.position, target)) >= 0;
    
}
