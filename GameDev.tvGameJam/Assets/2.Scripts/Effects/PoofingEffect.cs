using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofingEffect : MonoBehaviour
{
    [SerializeField] private GameObject poof;

    public void Poofing()
    {
        Instantiate(poof, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
