using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoin : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(80 * Time.deltaTime, 100 * Time.deltaTime), ForceMode2D.Impulse);       
    }


}
