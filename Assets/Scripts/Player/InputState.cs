using UnityEngine;
using System.Collections.Generic;

public class ButtonState
{
    public bool value;
    public float holdTime = 0f;
}

public enum Direction
{
    Left = -1,
    Right = 1
}

public class InputState : MonoBehaviour
{
    public Direction direction = Direction.Right;
    public float absVelX = 0f;
    public float absVelY = 0f;

    private Rigidbody2D body2D;
    private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();

    private void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        absVelX = Mathf.Abs(body2D.velocity.x);
        absVelY = Mathf.Abs(body2D.velocity.y);
    }

    public void SetButtonValue(Buttons key, bool value)
    {
        if (!buttonStates.ContainsKey(key))
            buttonStates.Add(key, new ButtonState());

        var state = buttonStates[key];

        if(state.value && !value)
        {
            state.holdTime = 0f;

            //Debug.Log("Button " + key + " released" + state.holdTime);
        }
        else if(state.value && value)
        {
            state.holdTime += Time.deltaTime;

            //Debug.Log("Button " + key + " down" + state .holdTime);
        }
        state.value = value;
    }


    public bool GetButtonValue (Buttons key)
    {
        if (buttonStates.ContainsKey(key))
            return buttonStates[key].value;
        else
            return false;
    }

    public float GetButtonHoldTime (Buttons key)
    {
        if (buttonStates.ContainsKey(key))
            return buttonStates[key].holdTime;
        else
            return 0f;
    }
}
