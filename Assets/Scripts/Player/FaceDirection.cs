using UnityEngine;

public class FaceDirection : AbstractBehaviour
{
    void Update()
    {
        var left = inputState.GetButtonValue(inputButton[0]);
        var right = inputState.GetButtonValue(inputButton[1]);

        if (right)
            inputState.direction = Direction.Right;
        else if (left)
            inputState.direction = Direction.Left;

        transform.localScale = new Vector3((float)inputState.direction, 1f, 1f);
    }
}
