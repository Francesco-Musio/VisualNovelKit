using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VisualNovelLayersArea : MonoBehaviour
{
    #region Delegates
    public delegate int BackgroundEvent(string[] _target);
    public BackgroundEvent ChangeBackground;
    #endregion

    [Header("Background List")]
    [SerializeField]
    private List<Sprite> backgroundList = new List<Sprite>();

    [Header("Active Background")]
    [SerializeField]
    private string activeBackground;

    [Header("Layers")]
    [SerializeField]
    private Image background;
    [SerializeField]
    private Image foreground;

    #region API
    public void Init()
    {
        ChangeBackground += HandleChangeBackground;

        activeBackground = "null";
    }
    #endregion

    #region Handlers
    private int HandleChangeBackground(string[] _target)
    {
        if (activeBackground != _target[0])
        {
            if (activeBackground == "null")
            {
                foreach (Sprite _current in backgroundList)
                {
                    if (_current.name == _target[0])
                    {
                        activeBackground = _current.name;
                        StartCoroutine(CPlaceBackground(_current, int.Parse(_target[1])));
                        return 1;
                    }
                }
            }

            foreach (Sprite _current in backgroundList)
            {
                if (_current.name == _target[0])
                {
                    activeBackground = _current.name;
                    StartCoroutine(CSwitchBackground(_current, int.Parse(_target[1])));
                    return 2;
                }
            }
        }

        return 0;
    }
    #endregion

    #region Coroutines
    IEnumerator CPlaceBackground(Sprite _target, int _duration)
    {
        background.sprite = _target;
        // fade in new background for duration
        yield return null;
    }

    IEnumerator CSwitchBackground (Sprite _target, int _duration)
    {
        // fade to black for _duration
        background.sprite = _target;
        // fade in new background for duration
        yield return null;
    }
    #endregion
}
