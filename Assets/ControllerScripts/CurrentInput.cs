using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes in the current InputControls instance and maps current input actions to bools designating if an input action has been fired
/// </summary>
public class CurrentInput
{
    public bool Up { get; }
    public bool Down { get; }
    public bool Left { get; }
    public bool Right { get; }
    public bool Attack { get; }
    public bool Jump { get; }
    public bool Defend { get; }
    public Vector2 MovementVector { get; }


    public CurrentInput(InputControls inputControls)
    {
        Up = MapFloatToBool(inputControls.Player.direction_up.ReadValue<float>());
        Down = MapFloatToBool(inputControls.Player.direction_down.ReadValue<float>());
        Left = MapFloatToBool(inputControls.Player.direction_left.ReadValue<float>());
        Right = MapFloatToBool(inputControls.Player.direction_right.ReadValue<float>());
        Attack = MapFloatToBool(inputControls.Player.attack.ReadValue<float>());
        Jump = MapFloatToBool(inputControls.Player.jump.ReadValue<float>());
        Defend = MapFloatToBool(inputControls.Player.defend.ReadValue<float>());
        MovementVector = inputControls.Player.movement.ReadValue<Vector2>();
    }

    private bool MapFloatToBool(float value)
    {
        if (value == 0) return false;
        if (value == 1) return true;
        else throw new Exception("Mapfloattobool, something bad happened");
    }
}
