using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

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

    }
}
