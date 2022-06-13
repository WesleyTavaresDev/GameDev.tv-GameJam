using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelEnemy : MonoBehaviour
{
    enum SentinelState { Idle, Shooting};
    [SerializeField] private SentinelState sentinelState;

    [SerializeField] private float shootDelay;
    [SerializeField] private AnimationClip shootAnimation;


    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrel;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();    

        InvokeRepeating("OnShoot", 2f, shootDelay);
    }

    void Update()
    {


        switch(sentinelState)
        {
            case SentinelState.Idle:
                anim.SetBool("Idle", true);
                anim.SetBool("Shooting", false);

            break;

            case SentinelState.Shooting:
                anim.SetBool("Shooting", true);
                anim.SetBool("Idle", false);
            break;
        }
    }

    void OnShoot() => StartCoroutine(Shoot());
    
    void Shooting() 
    {   
        var tempBullet = Instantiate(bullet, barrel.position, Quaternion.identity);
        tempBullet.transform.localScale = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y);
    }

    IEnumerator Shoot()
    {
        sentinelState = SentinelState.Shooting;

        yield return new WaitForSeconds(shootAnimation.length);
        sentinelState = SentinelState.Idle;


    }
}
