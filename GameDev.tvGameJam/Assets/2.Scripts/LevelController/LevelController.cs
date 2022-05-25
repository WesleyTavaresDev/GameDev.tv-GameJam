using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{   
    [SerializeField] private LevelTransition transition;

    void Start() => transition.Fade(1.5f, 0);

    void ChangeLevel(int sceneIndex)
    {
        StartCoroutine(LevelChanger(sceneIndex));
    }

    IEnumerator LevelChanger(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        transition.FillScreen(transition.EffectTime);
        yield return new WaitForSeconds(transition.EffectTime);
        
        operation.allowSceneActivation = true;
    }

    void OnEnable() => LevelGate.changeLevel += ChangeLevel;

    void OnDisable() => LevelGate.changeLevel -= ChangeLevel;
}
