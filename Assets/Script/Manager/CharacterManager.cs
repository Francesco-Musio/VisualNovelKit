using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Characters
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("Actors")]
        [SerializeField]
        private List<Actor> actorList = new List<Actor>();

        #region API
        public void Init()
        {
            foreach (Actor actor in actorList)
            {
                actor.Init();
            }
        }
        #endregion
    }
}
