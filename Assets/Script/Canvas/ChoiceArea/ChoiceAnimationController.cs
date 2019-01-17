using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class ChoiceAnimationController : MonoBehaviour
{

    private Image choiceBackground;
    private float duration;

    public void Init(Image _background, float _duration)
    {
        choiceBackground = _background;
        duration = _duration;
    }

    #region FadeIn
    public YieldInstruction FadeIn()
    {
        // fade in to 50/255 alpha
        return choiceBackground.DOFade(0.5f, duration).SetEase(Ease.Linear).WaitForCompletion();
    }

    public void FadeIn(out Tweener _animation)
    {
        // fade in to 50/255 alpha
        _animation = choiceBackground.DOFade(0.5f, duration).SetEase(Ease.Linear);
    }
    #endregion

    #region Fade Out
    public YieldInstruction FadeOut()
    {
        // fade in to 50/255 alpha
        return choiceBackground.DOFade(0.0f, duration).SetEase(Ease.Linear).WaitForCompletion();
    }

    public void FadeOut(out Tweener _animation)
    {
        // fade in to 50/255 alpha
        _animation = choiceBackground.DOFade(0.0f, duration).SetEase(Ease.Linear);
    }
    #endregion

}
