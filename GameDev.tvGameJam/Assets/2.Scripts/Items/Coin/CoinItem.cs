using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour, ICollectableItem
{
    public delegate void Collected(int value);
    public static event Collected collected;
    

    [SerializeField] private CoinAttributes attributes;
    Animator anim;

    void Awake() => anim = GetComponent<Animator>();
    
    public void OnCollect()
    {
        anim.SetTrigger("Collected");    
    }

    void DestroyCoin() => Destroy(this.gameObject);

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            OnCollect();
            collected?.Invoke(attributes.coinPoint);
        }
    }
}
