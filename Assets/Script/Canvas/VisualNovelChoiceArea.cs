using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

public class VisualNovelChoiceArea : MonoBehaviour
{
    #region Delegates
    public delegate void CreateChoicesEvent(List<Choice> _choices);
    public CreateChoicesEvent CreateChoices;
    #endregion

    [Header("Choice Options")]
    [SerializeField]
    private Transform buttonsContainer;
    [SerializeField]
    private GameObject buttonPrefab;

    [Header("Button Pool Options")]
    [SerializeField]
    private int maxChoices = 4;
    [SerializeField]
    private Transform buttonPoolContainer;

    private List<GameObject> buttonPool = new List<GameObject>();

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
    public void Init()
    {
        for (int i = 0; i < maxChoices; i++)
        {
            GameObject _newButton = GameObject.Instantiate(buttonPrefab, buttonPoolContainer.position, buttonPoolContainer.rotation, buttonPoolContainer);
            _newButton.SetActive(false);
            buttonPool.Add(_newButton);
        }

        CreateChoices += HandleCreateChoices;
    }
    #endregion

    #region Handlers
    private void HandleCreateChoices (List<Choice> _choices)
    {
        foreach (Choice _current in _choices)
        {
            GameObject _button = getButton();
            _button.transform.SetParent(buttonsContainer);
            //_button.Init()
            _button.SetActive(true);
        }
    }
    #endregion

}
