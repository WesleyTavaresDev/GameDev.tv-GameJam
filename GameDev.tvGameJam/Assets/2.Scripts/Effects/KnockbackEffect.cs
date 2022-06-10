using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    [SerializeField] private Vector2 knockbackForce;

    Rigidbody2D rb;

    void Start() => rb = GetComponent<Rigidbody2D>();    

    public void Knockback() => rb.AddForce(new Vector2((transform.localScale.x > 0 ? knockbackForce.x : knockbackForce.x * -1) * Time.fixedDeltaTime, knockbackForce.y * Time.fixedDeltaTime), ForceMode2D.Impulse);

}
