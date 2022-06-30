using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private float speed;


    void Update()
    {
        transform.Translate(new Vector2((transform.localScale.x * -1) * Time.deltaTime * speed, 0));    
    }
}
