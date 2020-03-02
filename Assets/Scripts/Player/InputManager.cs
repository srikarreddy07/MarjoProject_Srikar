using UnityEngine;

public enum Buttons
{
    Left,
    Right,
    Up,
    Down,
    A,
    B,
    C,
    D
}

public enum Condition
{
    GreaterThan,
    LessThan
}

[System.Serializable]
public class InputAxisSate
{
    public string axisName; // mapped to input key
    public float offValue;
    public Buttons button;
    public Condition condition;

    public bool Value
    {
        get
        {
            var val = Input.GetAxis(axisName);

            switch(condition)
            {
                case Condition.GreaterThan:
                    return val > offValue;
                case Condition.LessThan:
                    return val < offValue;
            }

            return false;
        }
    }
}

public class InputManager : MonoBehaviour
{
    public InputAxisSate[] inputs;
    public InputState inputState;
    
    void Update()
    {
        foreach(var input in inputs)
        {
            inputState.SetButtonValue(input.button, input.Value);
        }
    }
}
