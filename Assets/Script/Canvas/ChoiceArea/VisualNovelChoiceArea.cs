using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(ChoiceAnimationController))]
public class VisualNovelChoiceArea : MonoBehaviour
{
    #region Delegates
    public delegate void CreateChoicesEvent(List<Choice> _choices);
    public CreateChoicesEvent CreateChoices;

    public delegate void ChoiceEvent(int _index);
    public ChoiceEvent Choice;

    public Action ResetButtons;
    #endregion

    [Header("Choice Options")]
    [SerializeField]
    private Transform buttonsContainer;
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private List<ChoiceButton> activeButtons = new List<ChoiceButton>();

    [Header("Button Pool Options")]
    [SerializeField]
    private int maxChoices = 4;
    [SerializeField]
    private Transform buttonPoolContainer;

    [Header("Choice Background")]
    [SerializeField]
    private Image choiceBackground;

    private List<GameObject> buttonPool = new List<GameObject>();

    private UIManager ui;
    private ChoiceAnimationController choiceAnimationCtrl;

    private GameObject getButton()
    {
        foreach (GameObject _current in buttonPool)
        {
            if (!_current.activeInHierarchy)
            {
                return _current;
            }
        }
        return null;
    }

    #region API
    public void Init(UIManager _manager)
    {
        for (int i = 0; i < maxChoices; i++)
        {
            GameObject _newButton = GameObject.Instantiate(buttonPrefab, buttonPoolContainer.position, buttonPoolContainer.rotation, buttonPoolContainer);
            _newButton.SetActive(false);
            buttonPool.Add(_newButton);
            _newButton.GetComponent<ChoiceButton>().Init(this);
        }

        ui = _manager;

        choiceAnimationCtrl = GetComponent<ChoiceAnimationController>();
        if (choiceAnimationCtrl != null)
        {
            choiceAnimationCtrl.Init(choiceBackground, 0.5f);
        }

        CreateChoices += HandleCreateChoices;
        Choice += HandleChoice;
        ResetButtons += HandleResetButtons;
    }
    #endregion

    #region Handlers
    private void HandleCreateChoices (List<Choice> _choices)
    {
        StartCoroutine(CCreateChoices(_choices));
    }

    private void HandleChoice(int _index)
    {
        ui.Choice(_index);
    }

    private void HandleResetButtons()
    {
        StartCoroutine(CResetButtons());
    }
    #endregion

    #region Coroutines
    private IEnumerator CCreateChoices (List<Choice> _choices)
    {
        yield return choiceAnimationCtrl.FadeIn();

        foreach (Choice _current in _choices)
        {
            GameObject _button = getButton();
            _button.transform.SetParent(buttonsContainer);
            _button.GetComponent<ChoiceButton>().SetupButton(_current.text, _current.index);
            activeButtons.Add(_button.GetComponent<ChoiceButton>());
            _button.SetActive(true);
        }
    }  

    private IEnumerator CResetButtons ()
    {
        yield return choiceAnimationCtrl.FadeOut();

        foreach (ChoiceButton _current in activeButtons)
        {
            _current.gameObject.SetActive(false);
            _current.ResetButton();
            _current.transform.SetParent(buttonPoolContainer);
        }

        activeButtons.Clear();
    }
    #endregion

}
