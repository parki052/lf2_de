using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuedInput
{
    public float InputTime { get; }
    public bool WasMovementBlocked { get; }
    public ActionInput Input { get; }

    public QueuedInput(float inputTime, ActionInput input, bool wasMovementBlocked)
    {
        InputTime = inputTime;
        Input = input;
        WasMovementBlocked = wasMovementBlocked;
    }
}
