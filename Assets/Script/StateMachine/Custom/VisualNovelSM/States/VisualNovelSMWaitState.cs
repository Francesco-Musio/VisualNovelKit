using UnityEngine;
using System.Collections;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelSMWaitState : VisualNovelSMStateBase
{

    float timer = 0;
    int timeToWait = -1;

    bool canStart = false;

    public override void Enter()
    {
        LineElement _currentLine = context.story.GetCurrentLine();
        string[] _data = _currentLine.GetData();
        timeToWait = int.Parse(_data[_data.Length - 1]);
        canStart = true;
    }

    public override void Tick()
    {
        if (canStart)
        {
            timer = timer + Time.deltaTime;
            if (timer >= timeToWait)
            {
                context.GoToReadLineCallback();
            }
        }
    }

    public override void Exit()
    {
        timer = 0;
        timeToWait = -1;
        canStart = false;
    }


}
