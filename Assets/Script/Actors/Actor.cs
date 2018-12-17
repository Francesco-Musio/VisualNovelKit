using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace Characters
{
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
        private string name = "Actor";

        [Header("Current State")]
        [SerializeField]
        private string currentEmotion;
        [SerializeField]
        private ActorState position;

        [Header("Emotions List")]
        [SerializeField]
        private Sprite[] emotions;

        private SpriteRenderer spriteRenderer;

        #region API
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

        #region Delegated
        private void HandleRemoveActor(int _duration)
        {
            StartCoroutine(CRemoveActor(_duration));
        }

        private void HandleInsertActor(int _duration, Vector3 _target, ActorState _newPosition)
        {
            this.gameObject.SetActive(true);
            StartCoroutine(CInsertActor(_duration, _target, _newPosition));
        }
        #endregion

        #region Coroutines
        private IEnumerator CRemoveActor (int _duration)
        {
            // remove animation for _duration
            this.gameObject.SetActive(false);
            this.position = ActorState.OffScene;
            this.transform.position = new Vector3(1000, 1000, 1000);
            yield return null;
        }

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
