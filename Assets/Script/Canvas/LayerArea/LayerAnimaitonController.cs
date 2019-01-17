using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class LayerAnimaitonController : MonoBehaviour
{

    #region FadeIn
    public YieldInstruction FadeIn (Image _img, int _duration)
    {
        return _img.DOFade(1.0f, _duration).SetEase(Ease.Linear).WaitForCompletion();
    }

    public void FadeIn(Image _img, int _duration, out Tweener _animation)
    {
        _animation = _img.DOFade(1.0f, _duration).SetEase(Ease.Linear);
    }
    #endregion

    #region FadeOut
    public YieldInstruction FadeOut(Image _img, int _duration)
    {
        return _img.DOFade(0.0f, _duration).SetEase(Ease.Linear).WaitForCompletion();
    }

    public void FadeOut(Image _img, int _duration, out Tweener _animation)
    {
        _animation = _img.DOFade(0.0f, _duration).SetEase(Ease.Linear);
    }
    #endregion  

}
