using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace Characters
{
    /// <summary>
    /// Enum to signal the position of this actor in the scene
    /// </summary>
    public enum ActorState {
        OffScene,
        Left,
        Right
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class Actor : MonoBehaviour
    {
        #region Delegates
        public delegate void ActorRemoveEvent(int _duration);
        public ActorRemoveEvent RemoveActor;

        public delegate void ActorInsertEvent(int _duration, Vector3 _target, ActorState _newPosition);
        public ActorInsertEvent InsertActor;
        #endregion

        [Header("General Info")]
        [SerializeField]
        [Tooltip("Name of the actor. This is used to identify this actor")]
        private string name = "Actor";

        [Header("Current State")]
        [SerializeField]
        [Tooltip("Current emotion of this actor")]
        private string currentEmotion;
        [SerializeField]
        [Tooltip("Current position of this actor")]
        private ActorState position;

        [Header("Emotions List")]
        [SerializeField]
        [Tooltip("List of emotions that the actor can assume")]
        private Sprite[] emotions;

        /// <summary>
        /// Reference to object's Sprite Renderer
        /// </summary>
        private SpriteRenderer spriteRenderer;

        #region API
        /// <summary>
        /// Initialize this object
        /// Set current emotion at 0, disable the object and initialize events
        /// </summary>
        public void Init()
        {
            position = ActorState.OffScene;

            spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = emotions[0];

            currentEmotion = spriteRenderer.sprite.name;

            this.transform.position = new Vector3(1000, 1000, 1000);
            this.gameObject.SetActive(false);

            RemoveActor += HandleRemoveActor;
            InsertActor += HandleInsertActor;
        }

        /// <summary>
        /// Changes the current emotion of the actor
        /// </summary>
        /// <param name="newState">id of the new emotion</param>
        private void ChangeState(int newState)
        {
            if (emotions.Length > newState && newState >= 0)
            {
                spriteRenderer.sprite = emotions[newState];
            }

            currentEmotion = spriteRenderer.sprite.name;
        }
        #endregion

        #region Getters
        public string GetName()
        {
            return name;
        }

        public ActorState GetPosition()
        {
            return position;
        }
        #endregion

        #region Handlers
        /// <summary>
        /// Remove this actor from the scene
        /// </summary>
        /// <param name="_duration">duration of the animation</param>
        private void HandleRemoveActor(int _duration)
        {
            StartCoroutine(CRemoveActor(_duration));
        }

        /// <summary>
        /// Insert the actor in the scene
        /// </summary>
        /// <param name="_duration">duration of the animation</param>
        /// <param name="_target">target position for the actor</param>
        /// <param name="_newPosition">new ActorState</param>
        private void HandleInsertActor(int _duration, Vector3 _target, ActorState _newPosition)
        {
            this.gameObject.SetActive(true);
            StartCoroutine(CInsertActor(_duration, _target, _newPosition));
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// Coroutine that animate the actor's exit and disable the object
        /// </summary>
        /// <param name="_duration">duration of the animation</param>
        /// <returns></returns>
        private IEnumerator CRemoveActor (int _duration)
        {
            // remove animation for _duration
            this.gameObject.SetActive(false);
            this.position = ActorState.OffScene;
            this.transform.position = new Vector3(1000, 1000, 1000);
            yield return null;
        }

        /// <summary>
        /// Coroutine that insert the actor in the scene
        /// </summary>
        /// <param name="_duration">duration of the animation</param>
        /// <param name="_target">Target position of the actor</param>
        /// <param name="_newPosition">new ActorState of this actor</param>
        /// <returns></returns>
        private IEnumerator CInsertActor(int _duration,  Vector3 _target, ActorState _newPosition)
        {
            //set pre animation position
            //insert animation for _duration
            this.transform.position = _target; //TO REMOVE
            this.position = _newPosition;
            yield return null;
        }
        #endregion

    }
}
