using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    enum EnemyState {Idle, Flying};
    [SerializeField] private EnemyState enemyState;
    [SerializeField] private GameObject player;
    [SerializeField] private float triggerDistance;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {

        if(Vector2.Distance(player.transform.position, this.gameObject.transform.position) <= triggerDistance)
            enemyState = EnemyState.Flying;
        else    
            enemyState = EnemyState.Idle;

        switch (enemyState)
        {
            case EnemyState.Idle:
                Flip(startPos);
                if(this.transform.position != startPos)    
                {
                    Fly(startPos);
                }
            break;
            
            case EnemyState.Flying:
                Flip(player.transform.position);
                Fly(player.transform.position);
            break;
        }

    }

    void Fly(Vector2 target) => transform.position = Vector2.MoveTowards(transform.position, target, 0.1f * Time.deltaTime);



    void OnColliderEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player") && enemyState == EnemyState.Flying)
            enemyState = EnemyState.Idle;
        
    }

    void Flip(Vector2 target)
    {   
        Vector2 scale = transform.localScale;
        
    /*    if(Mathf.Round(Vector2.Dot(this.transform.position, target)) <= 0)
            scale.x = 1;

        else if(Mathf.Round(Vector2.Dot(this.transform.position, target)) > 0)
            scale.x = -1;
    */
            
        if(Mathf.Round(Vector2.Dot(this.transform.position, target)) <= 0)
            scale.x = 1;

        else if(Mathf.Round(Vector2.Dot(this.transform.position, target)) > 0)
            scale.x = -1;
    

        transform.localScale = scale;
    }
}
