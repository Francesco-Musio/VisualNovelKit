using UnityEngine;
using System.Collections;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelSMWaitState : VisualNovelSMStateBase
{
    /// <summary>
    /// Internal timer of this state
    /// </summary>
    float timer = 0;

    /// <summary>
    /// Time to wait in this state
    /// </summary>
    int timeToWait = -1;

    /// <summary>
    /// if true, the timer can start
    /// </summary>
    bool canStart = false;

    /// <summary>
    /// Get the time to wait and then start waiting
    /// </summary>
    public override void Enter()
    {
        LineElement _currentLine = context.story.GetCurrentLine();
        string[] _data = _currentLine.GetData();
        timeToWait = int.Parse(_data[_data.Length - 1]);
        canStart = true;
    }

    /// <summary>
    /// Check time every update and then read the next line from the ink file
    /// </summary>
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

    /// <summary>
    /// reset the default value
    /// </summary>
    public override void Exit()
    {
        timer = 0;
        timeToWait = -1;
        canStart = false;
    }


}
