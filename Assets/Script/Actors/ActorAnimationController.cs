using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace Characters.Animations
{

    public class ActorAnimationController : MonoBehaviour
    {

        SpriteRenderer spr;

        #region API
        public void Init()
        {
            spr = GetComponent<SpriteRenderer>();
        }

        #region Horizontal Transition
        public YieldInstruction HorizontalTransition(Vector3 _targetPosition, int _duration)
        {
            return transform.DOMove(_targetPosition, _duration).SetEase(Ease.Linear).WaitForCompletion();
        }

        private void HorizontalTransition(Vector3 _targetPosition, int _duration, out Tweener _animation)
        {
            _animation = transform.DOMove(_targetPosition, _duration).SetEase(Ease.Linear);
        }
        #endregion

        #region Fade In
        public YieldInstruction FadeIn(int _duration)
        {
            return spr.DOFade(1.0f, _duration).SetEase(Ease.Linear).WaitForCompletion();
        }

        private void FadeIn(int _duration, out Tweener _animation)
        {
            _animation = spr.DOFade(1.0f, _duration).SetEase(Ease.Linear);
        }
        #endregion

        #region Fade In
        public YieldInstruction FadeOut(int _duration)
        {
            return spr.DOFade(0.0f, _duration).SetEase(Ease.Linear).WaitForCompletion();
        }

        private void FadeOut(int _duration, out Tweener _animation)
        {
            _animation = spr.DOFade(0.0f, _duration).SetEase(Ease.Linear);
        }
        #endregion

        #region Horizontal Transition w/ Fade In
        public YieldInstruction HorizontalTransitionFadeIn (Vector3 _targetPosition, int _duration)
        {
            Sequence _sequence = DOTween.Sequence();
            _sequence.Pause();
            Tweener _temp = null;
            HorizontalTransition(_targetPosition, _duration, out _temp);
            _sequence.Append(_temp);
            FadeIn(_duration, out _temp);
            _sequence.Insert(0, _temp);

            return _sequence.Play().WaitForCompletion();
        }
        #endregion

        #region Horizontal Transition w/ Fade Out
        public YieldInstruction HorizontalTransitionFadeOut(Vector3 _targetPosition, int _duration)
        {
            Sequence _sequence = DOTween.Sequence();
            _sequence.Pause();
            Tweener _temp = null;
            HorizontalTransition(_targetPosition, _duration, out _temp);
            _sequence.Append(_temp);
            FadeOut(_duration, out _temp);
            _sequence.Insert(0, _temp);

            return _sequence.Play().WaitForCompletion();
        }
        #endregion

        #endregion

    }

}
