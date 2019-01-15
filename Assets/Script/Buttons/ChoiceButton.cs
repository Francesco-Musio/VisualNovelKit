using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ChoiceButton : MonoBehaviour
{
    #region Delegates
    public delegate void SetupEvent(string _text, int _index);
    public SetupEvent SetupButton;

    public Action ResetButton;
    #endregion

    private Text buttonText;
    private int targetIndex = -1;

    private VisualNovelChoiceArea choiceArea;

    #region API
    public void Init(VisualNovelChoiceArea _choiceArea)
    {
        buttonText = GetComponentInChildren<Text>();

        choiceArea = _choiceArea;

        SetupButton += HandleSetupButton;
        ResetButton += HandleResetButton;
    }
    #endregion

    #region Handlers
    private void HandleSetupButton(string _text, int _index)
    {
        buttonText.text = _text.Trim();

        targetIndex = _index;
    }

    private void HandleResetButton()
    {
        buttonText.text = "";
        targetIndex = -1;
    }
    #endregion

    #region OnClick
    public void OnClick()
    {
        choiceArea.Choice(targetIndex);
    }
    #endregion

}
