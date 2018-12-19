using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.VisualNovelSM;
using StoryManagerNS;

public class VisualNovelSMPlaceActorState : VisualNovelSMStateBase
{
    /// <summary>
    /// Total animation time
    /// Value got from the story line
    /// </summary>
    private int animationTime = 10000;
    /// <summary>
    /// Internal timer
    /// </summary>
    private float timer = 0;
    /// <summary>
    /// bool to signel when the timer can start
    /// </summary>
    private bool start = false;

    /// <summary>
    /// Get the Current line from the story manager and request to place the actors
    /// </summary>
    public override void Enter()
    {
        LineElement _currentLine = context.story.GetCurrentLine();
        animationTime = int.Parse(_currentLine.GetData()[2]);
        start = true;
        context.characters.PlaceActor(_currentLine.GetData());

    }

    /// <summary>
    /// wait for the animation to complete
    /// TODO: Replace this state with submachine and make another state where the system waits
    /// </summary>
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

    /// <summary>
    /// Reset default state values
    /// </summary>
    public override void Exit()
    {
        start = false;
        timer = 0;
        animationTime = 10000;
    }

}
