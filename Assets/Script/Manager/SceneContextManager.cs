using UnityEngine;
using System.Collections;
using StateMachine.VisualNovelSM;

[RequireComponent(typeof(VisualNovelSMController))]
public class SceneContextManager : MonoBehaviour
{
    
    private VisualNovelSMController visualNovelSMController;

    public void Init ()
    {
        visualNovelSMController.Init(this);
    }
}
