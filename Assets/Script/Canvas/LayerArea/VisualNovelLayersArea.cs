using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(LayerAnimaitonController))]
public class VisualNovelLayersArea : MonoBehaviour
{
    #region Delegates
    public delegate void BackgroundEvent(string[] _target, out int _multiplier);
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

    private LayerAnimaitonController layerAnimaitonCtrl;

    #region API
    /// <summary>
    /// Initialize the Layer's Area
    /// </summary>
    public void Init()
    {
        layerAnimaitonCtrl = GetComponent<LayerAnimaitonController>();

        ChangeBackground += HandleChangeBackground;

        activeBackground = "null";
    }
    #endregion

    #region Handlers
    private void HandleChangeBackground(string[] _target, out int _multiplier)
    {

        string _newBgrName = _target[0];
        int _duration = int.Parse(_target[1]);

        _multiplier = 0;

        if (activeBackground != _newBgrName)
        {
            if (activeBackground == "null")
            {
                _multiplier = 1;

                foreach (Sprite _current in backgroundList)
                {
                    if (_current.name == _newBgrName)
                    {
                        activeBackground = _current.name;
                        StartCoroutine(CPlaceBackground(_current, _duration));
                        return;
                    }
                }
            }
            else if (_newBgrName == "null")
            {
                _multiplier = 1;
                StartCoroutine(CRemoveBackground(_duration));
            }
            else
            {
                _multiplier = 2;
                foreach (Sprite _current in backgroundList)
                {
                    if (_current.name == _newBgrName)
                    {
                        activeBackground = _current.name;
                        StartCoroutine(CSwitchBackground(_current, _duration));
                        return;
                    }
                }
            }
            
        }

        return;
    }
    #endregion

    #region Coroutines
    IEnumerator CPlaceBackground(Sprite _target, int _duration)
    {
        background.sprite = _target;
        Color _tempColor = Color.white;
        background.color = _tempColor;
        layerAnimaitonCtrl.FadeOut(foreground, _duration);
        yield return null;
    }

    IEnumerator CSwitchBackground (Sprite _target, int _duration)
    {
        yield return layerAnimaitonCtrl.FadeIn(foreground, _duration);
        Color _tempColor = Color.white;
        background.color = _tempColor;
        background.sprite = _target;
        layerAnimaitonCtrl.FadeOut(foreground, 0);
        yield return null;
    }

    IEnumerator CRemoveBackground (int _duration)
    {
        yield return layerAnimaitonCtrl.FadeIn(foreground, _duration);
        Color _tempColor = Color.black;
        background.color = _tempColor;
        background.sprite = null;
        layerAnimaitonCtrl.FadeOut(foreground, _duration);
        yield return null;
    }
    #endregion
}
