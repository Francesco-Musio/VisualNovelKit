using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelSMPlaceActorState : VisualNovelSMStateBase
{
    private int animationTime = 10000;
    private float timer = 0;
    private bool start = false;

    public override void Enter()
    {
        LineElement _currentLine = context.story.GetCurrentLine();
        animationTime = int.Parse(_currentLine.GetData()[2]);
        start = true;
        context.characters.PlaceActor(_currentLine.GetData());

    }

    public override void Tick()
    {
        if (start)
        {
            timer = timer + Time.deltaTime;

            if (timer >= animationTime)
            {
                context.GoToReadLineCallback();
            }
        }
    }

    public override void Exit()
    {
        start = false;
        timer = 0;
        animationTime = 10000;
    }

}
