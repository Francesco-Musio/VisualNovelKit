using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace Characters.Animations
{

    public class ActorAnimationController : MonoBehaviour
    {
        /// <summary>
        /// if not specified, the animation controller will use this sprite renderer for the animations
        /// </summary>
        private SpriteRenderer defaultSpr;

        #region API
        /// <summary>
        /// Initialize the animator controller
        /// </summary>
        /// <param name="_spr">default Sprite Renderer</param>
        public void Init(SpriteRenderer _spr)
        {
            DOTween.Init(true, true, LogBehaviour.Verbose);
            defaultSpr = _spr;
        }

        #region Horizontal Transition
        /// <summary>
        /// Horizontal Movement from position to target position
        /// </summary>
        /// <param name="_targetPosition">last point of the animation</param>
        /// <param name="_duration">duration of the animation</param>
        /// <returns>wait time for a yield</returns>
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
        /// <summary>
        /// Fade in the object
        /// </summary>
        /// <param name="_duration">duration of the animation</param>
        /// <param name="_spr">target Sprite Renderer if it's not the default one</param>
        /// <returns>wait time for a yield</returns>
        public YieldInstruction FadeIn(int _duration, SpriteRenderer _spr = null)
        {
            if (_spr == null)
                return defaultSpr.DOFade(1.0f, _duration).SetEase(Ease.Linear).WaitForCompletion();
            else 
                return _spr.DOFade(1.0f, _duration).SetEase(Ease.Linear).WaitForCompletion();
        }

        private void FadeIn(int _duration, out Tweener _animation, SpriteRenderer _spr = null)
        {
            _animation = (_spr == null)? defaultSpr.DOFade(1.0f, _duration).SetEase(Ease.Linear) : _spr.DOFade(1.0f, _duration).SetEase(Ease.Linear);
        }
        #endregion

        #region Fade Out
        /// <summary>
        /// Fade out the object
        /// </summary>
        /// <param name="_duration">duration of the animation</param>
        /// <param name="_spr">target Sprite Renderer if it's not the default one</param>
        /// <returns>wait time for a yield</returns>
        public YieldInstruction FadeOut(int _duration, SpriteRenderer _spr = null)
        {
            if (_spr == null)
                return defaultSpr.DOFade(0.0f, _duration).SetEase(Ease.Linear).WaitForCompletion();
            else
                return _spr.DOFade(0.0f, _duration).SetEase(Ease.Linear).WaitForCompletion();
        }

        private void FadeOut(int _duration, out Tweener _animation, SpriteRenderer _spr = null)
        {
            _animation = (_spr == null)? defaultSpr.DOFade(0.0f, _duration).SetEase(Ease.Linear) : _spr.DOFade(0.0f, _duration).SetEase(Ease.Linear);
        }
        #endregion

        #region Horizontal Transition w/ Fade In
        /// <summary>
        /// Combine the Horizontal Transition with a fade in
        /// </summary>
        /// <param name="_targetPosition">last point of the animation</param>
        /// <param name="_duration">duration of the animation</param>
        /// <param name="_spr">target Sprite Renderer if it's not the default one</param>
        /// <returns>wait time for a yield</returns>
        public YieldInstruction HorizontalTransitionFadeIn (Vector3 _targetPosition, int _duration, SpriteRenderer _spr = null)
        {
            Sequence _sequence = DOTween.Sequence();
            _sequence.Pause();
            Tweener _temp = null;
            HorizontalTransition(_targetPosition, _duration, out _temp);
            _sequence.Append(_temp);

            if (_spr == null)
                FadeIn(_duration, out _temp);
            else
                FadeIn(_duration, out _temp, _spr);

            _sequence.Insert(0, _temp);

            return _sequence.Play().WaitForCompletion();
        }
        #endregion

        #region Horizontal Transition w/ Fade Out
        /// <summary>
        /// Combine the Horizontal Transition with a fade out
        /// </summary>
        /// <param name="_targetPosition">last point of the animation</param>
        /// <param name="_duration">duration of the animation</param>
        /// <param name="_spr">target Sprite Renderer if it's not the default one</param>
        /// <returns>wait time for a yield</returns>
        public YieldInstruction HorizontalTransitionFadeOut(Vector3 _targetPosition, int _duration, SpriteRenderer _spr = null)
        {
            Sequence _sequence = DOTween.Sequence();
            _sequence.Pause();
            Tweener _temp = null;
            HorizontalTransition(_targetPosition, _duration, out _temp);
            _sequence.Append(_temp);

            if (_spr == null)
                FadeOut(_duration, out _temp);
            else
                FadeOut(_duration, out _temp, _spr);

            _sequence.Insert(0, _temp);

            return _sequence.Play().WaitForCompletion();
        }
        #endregion

        #endregion

        #region Setters
        public void SetSpriteRenderer (SpriteRenderer _spr)
        {
            defaultSpr = _spr;
        }
        #endregion

    }

}
