using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.VisualNovelSM;

public class InGameSMReadLineState : VisualNovelSMStateBase {

    public override void Enter()
    {
        Debug.Log("Funzia");
    }

    public override void Tick()
    {
        Debug.Log("Tick");
    }

}
