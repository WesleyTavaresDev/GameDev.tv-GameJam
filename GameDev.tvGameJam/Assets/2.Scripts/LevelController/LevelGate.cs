using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGate : MonoBehaviour
{
    public int sceneIndex;

    public delegate void ChangeLevel(int sceneIndex);
    public static event ChangeLevel changeLevel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            changeLevel?.Invoke(this.sceneIndex);
    }
}
