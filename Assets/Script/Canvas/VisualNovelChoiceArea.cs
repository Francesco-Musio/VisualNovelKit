using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisualNovelChoiceArea : MonoBehaviour
{

    [Header("Choice Options")]
    [SerializeField]
    private int maxChoices = 4;
    [SerializeField]
    private Transform buttonsContainer;
    [SerializeField]
    private GameObject buttonPrefab;

    private List<GameObject> buttonPool = new List<GameObject>();

    public void Init()
    {
        for (int i = 0; i < maxChoices; i++)
        {
            GameObject _newButton = GameObject.Instantiate(buttonPrefab);
            _newButton.SetActive(false);
            buttonPool.Add(_newButton);
        }
    }

}
