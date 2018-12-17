using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour { 

    [Header("Scene Context Manager")]
    [SerializeField]
    private SceneContextManager sceneContextManager;

	// Use this for initialization
	void Start () {

        if (sceneContextManager != null)
        {
            sceneContextManager.Init();
        }
        
	}
}
