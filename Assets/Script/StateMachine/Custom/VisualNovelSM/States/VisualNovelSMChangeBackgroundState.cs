using UnityEngine;
using UnityEditor;
using StoryManagerNS;
using StateMachine.VisualNovelSM;

public class VisualNovelSMChangeBackgroundState : VisualNovelSMStateBase
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
    /// miltiplies time to wait in case two animations needs to happen
    /// </summary>
    private int multiplier = 1;
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
        animationTime = int.Parse(_currentLine.GetData()[1]);
        start = true;
        multiplier = context.ui.ChangeBackground(_currentLine.GetData());

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

            if (timer >= animationTime * multiplier)
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