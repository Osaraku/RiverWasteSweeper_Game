using UnityEngine;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions playerInputAction;

    private void Awake()
    {
        playerInputAction = new InputSystem_Actions();

        playerInputAction.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
