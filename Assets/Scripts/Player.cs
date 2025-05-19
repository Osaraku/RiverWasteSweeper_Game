using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private int maxTrashValue = 15;


    private bool isWalking;
    private int currentTrashValue = 0;
    private Vector3 lastInteractDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        // HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    // private void HandleInteractions()
    // {
    //     Vector2 inputVector = gameInput.GetMovementVectorNormalized();

    //     Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

    //     if (moveDir != Vector3.zero)
    //     {
    //         lastInteractDir = moveDir;
    //     }
    //     float interactDistance = 2f;
    //     if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
    //     {
    //         if (raycastHit.transform.TryGetComponent(out BaseCounter counter))
    //         {
    //             // Has BaseCounter
    //             if (counter != selectedCounter)
    //             {
    //                 SetSelectedCounter(counter);

    //             }
    //         }
    //         else
    //         {
    //             SetSelectedCounter(null);
    //         }
    //     }
    //     else
    //     {
    //         SetSelectedCounter(null);
    //     }
    // }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 1f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Cant move toward moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    // Cannot move any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 5f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        // if (selectedCounter != null)
        // {
        //     selectedCounter.Interact(this);
        // }
    }

    public int GetCurrentTrashValue()
    {
        return currentTrashValue;
    }

    public int GetMaxTrashValue()
    {
        return maxTrashValue;
    }

    public void IncreaseTrashValue(int trashValue)
    {
        currentTrashValue += trashValue;
        Debug.Log(currentTrashValue);
    }


}
