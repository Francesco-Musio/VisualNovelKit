using UnityEngine;
using System.Collections;
using Ink.Runtime;
using System.IO;

/**
 * Line to print in the middle:
 *  0 $ 
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
        private TextAsset inkJSONAsset;
        [SerializeField]
        private Story story;

        [Header("Parsing Options")]
        [SerializeField]
        private char delimiter = '$';

        private LineElement currentLine;

        #region API
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
        private int id;
        private string[] data;

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



