using UnityEngine;
using System.Collections;
using Ink.Runtime;
using System.IO;

/**
 * Line to print in the middle:
 *  0 $ central $ message
 *  
 * Place Actor:
 *  1 $ LeftActorName $ RightActorName $ TransitionTime
 *  
 * Change Background:
 *  2 $ BackgroundName $ TransitionTime
 */

namespace StoryManagerNS
{
    public class StoryManager : MonoBehaviour
    {
        #region Delegates
        public delegate LineElement StoryEvent();
        public StoryEvent ReadLine;
        #endregion

        [Header("Ink File")]
        [SerializeField]
        [Tooltip("Story File")]
        private TextAsset inkJSONAsset;
        [SerializeField]
        private Story story;

        [Header("Parsing Options")]
        [SerializeField]
        [Tooltip("Character used to separate different parts of the command string in the json file")]
        private char delimiter = '$';

        /// <summary>
        /// last read line
        /// </summary>
        private LineElement currentLine;

        #region API
        /// <summary>
        /// Initialize this object
        /// </summary>
        public void Init()
        {
            if (inkJSONAsset != null)
            {
                story = new Story(inkJSONAsset.text);
            }

            ReadLine += HandleReadLine;
        }
        #endregion

        #region Delegated
        /// <summary>
        /// Read the next Line from the story
        /// </summary>
        /// <returns>Line Elements with the just read informations</returns>
        private LineElement HandleReadLine()
        {
            if (story.canContinue)
            {
                string _rawtext = story.Continue();
                _rawtext = _rawtext.Trim();

                LineElement _lineElement = ScriptableObject.CreateInstance(typeof(LineElement)) as LineElement;
                _lineElement.Init(_rawtext.Split(delimiter));

                currentLine = _lineElement;

                return _lineElement;
            }

            return null;
        }
        #endregion

        #region Getters
        public LineElement GetCurrentLine()
        {
            return currentLine;
        }
        #endregion

    }

    public class LineElement : ScriptableObject
    {
        /// <summary>
        /// id of this line's command
        /// </summary>
        private int id;

        /// <summary>
        /// array with the command options
        /// </summary>
        private string[] data;

        /// <summary>
        /// Initialize this object
        /// </summary>
        /// <param name="_rawdata"></param>
        public void Init (string[] _rawdata)
        {
            data = new  string[_rawdata.Length - 1];

            for (int i = 0; i < _rawdata.Length; i++)
            {
                if (i == 0)
                {
                    id = int.Parse(_rawdata[0]);
                }
                else
                {
                    data[i - 1] = _rawdata[i];
                }
            }
        }

        #region Getters
        public int GetId()
        {
            return id;
        }

        public string[] GetData()
        {
            return data;
        }
        #endregion
    }
}



