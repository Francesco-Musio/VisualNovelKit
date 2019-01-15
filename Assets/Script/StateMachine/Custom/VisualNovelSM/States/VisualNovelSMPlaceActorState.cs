﻿using System.Collections;
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
    /// miltiplies time to wait in case two animations needs to happen
    /// </summary>
    private int multiplier = 1;

    /// <summary>
    /// Get the Current line from the story manager and request to place the actors
    /// </summary>
    public override void Enter()
    {
        LineElement _currentLine = context.story.GetCurrentLine();
        animationTime = int.Parse(_currentLine.GetData()[2]);
        context.characters.PlaceActor(_currentLine.GetData(), out multiplier);

        _currentLine.SetTimer(animationTime * multiplier);
        context.GoToWaitCallback();
    }

    /// <summary>
    /// Reset default state values
    /// </summary>
    public override void Exit()
    {
        animationTime = 10000;
    }

}
