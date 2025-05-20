using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }


    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int maxTrashValue = 15;

    private InputSystem_Actions playerInputAction;
    private InputAction interactAction;

    private bool isWalking;
    private int currentTrashValue = 0;
    private int goldAmount = 0;
    private Vector3 lastInteractDir;

    private void Awake()
    {
        Instance = this;
        playerInputAction = new InputSystem_Actions();
        playerInputAction.Player.Enable();
    }

    private void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        if (interactAction.WasPressedThisFrame())
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out GarbageTruck garbageTruck))
                {
                    garbageTruck.Interact();
                }
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 1f;
        float playerHeight = 2f;

        // Layer Mask untuk mengecualikan layer "Player"
        int layerMask = ~LayerMask.GetMask("Player");

        // Gunakan CapsuleCastAll untuk mendapatkan semua collider yang bersinggungan
        RaycastHit[] hits = Physics.CapsuleCastAll(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDir,
            moveDistance,
            layerMask
        );

        // Cek apakah ada collider yang bukan trigger
        bool canMove = true;
        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.isTrigger)
            {
                canMove = false;
                break;
            }
        }

        if (!canMove)
        {
            // Cant move toward moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

            hits = Physics.CapsuleCastAll(
                transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius,
                moveDirX,
                moveDistance,
                layerMask
            );

            canMove = moveDir.x != 0;
            foreach (RaycastHit hit in hits)
            {
                if (!hit.collider.isTrigger)
                {
                    canMove = false;
                    break;
                }
            }

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
                hits = Physics.CapsuleCastAll(
                    transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    moveDirZ,
                    moveDistance,
                    layerMask
                );

                canMove = moveDir.z != 0;
                foreach (RaycastHit hit in hits)
                {
                    if (!hit.collider.isTrigger)
                    {
                        canMove = false;
                        break;
                    }
                }

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
        float rotateSpeed = 2f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public int GetCurrentTrashValue()
    {
        return currentTrashValue;
    }

    public void SetCurrentTrashValue(int value)
    {
        currentTrashValue = value;
    }

    public int GetMaxTrashValue()
    {
        return maxTrashValue;
    }

    public void IncreaseGoldAmount(int value)
    {
        goldAmount += value;
        Debug.Log("Gold amount:" + goldAmount);
        Debug.Log("Trash value:" + currentTrashValue);
    }

    public void IncreaseTrashValue(int value)
    {
        currentTrashValue += value;
        Debug.Log("Trash value:" + currentTrashValue);
    }


}
