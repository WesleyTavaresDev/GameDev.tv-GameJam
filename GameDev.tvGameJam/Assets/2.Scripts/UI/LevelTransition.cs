using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelTransition : MonoBehaviour
{   
    [SerializeField] private CanvasGroup canvasTransition;
    [SerializeField] private Image transitionImage;

    [Range(0f, 10f)]
    [SerializeField] private float effectTime;
    public float EffectTime 
    {
        get{ return effectTime; } 
    }
    
    public void FillScreen(float endValue) 
    { 
        canvasTransition.alpha = 1;
        transitionImage.fillAmount = 0;
        DOTweenModuleUI.DOFillAmount(transitionImage, endValue, effectTime);
    
    }

    public void Fade(float fadeTime, float fadeValue)
    {
        transitionImage.fillAmount = 1;
        DOTweenModuleUI.DOFade(canvasTransition, fadeValue, fadeTime);
    }
}
