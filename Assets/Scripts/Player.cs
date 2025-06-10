using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }


    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 2f;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private int maxTrashValue = 15;
    [SerializeField] private List<GameObject> boatLevelList;

    private InputSystem_Actions playerInputAction;
    private InputAction interactAction;
    private InputAction sprintAction;
    private InputAction pauseAction;

    private bool forcedStop = false;
    private bool isMoving;
    private bool isGamePaused = false;
    private int currentTrashValue = 0;
    private int totalTrashValue;
    private int currentBoatLevel = 1;
    private int goldAmount = 0;


    private void Awake()
    {
        Instance = this;
        playerInputAction = new InputSystem_Actions();
        playerInputAction.Player.Enable();
    }

    private void OnDestroy()
    {
        playerInputAction.Dispose();
    }

    private void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
        if (pauseAction.WasPressedThisFrame())
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public bool GetIsGamePaused()
    {
        return isGamePaused;
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

                if (collider.TryGetComponent(out BuyableBoat buyableBoat))
                {
                    buyableBoat.Interact();
                }
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        float moveInput = inputVector.y; // Up/Down untuk maju mundur
        float rotateInput = inputVector.x; // Left/Right untuk rotasi

        if (sprintAction.WasPressedThisFrame())
        {
            moveSpeed += sprintSpeed;
            Debug.Log("Sprint");
        }
        else if (sprintAction.WasReleasedThisFrame())
        {
            moveSpeed -= sprintSpeed;
        }

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 1f;
        float playerHeight = 2f;

        // Hitung arah gerak berdasarkan rotasi pemain
        Vector3 moveDir = transform.forward * moveInput;

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

        // Gerakkan pemain jika tidak ada tabrakan dan tidak dalam forced stop
        if (canMove && !forcedStop)
        {
            transform.position += moveDir.normalized * moveDistance;
        }

        // Rotasi pemain berdasarkan input kiri/kanan (X axis)
        if (!forcedStop && rotateInput != 0f)
        {
            float rotationAmount = rotateInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationAmount);
        }

        // Status gerak untuk animasi atau logika lainnya
        isMoving = Mathf.Abs(moveInput) > 0.01f;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public bool GetForcedStop()
    {
        return forcedStop;
    }

    public void SetForcedStop(bool value)
    {
        forcedStop = value;
    }

    public int GetCurrentTrashValue()
    {
        return currentTrashValue;
    }

    public void SetCurrentTrashValue(int value)
    {
        currentTrashValue = value;
    }

    public int GetTotalTrashValue()
    {
        return totalTrashValue;
    }

    public int GetMaxTrashValue()
    {
        return maxTrashValue;
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }

    public void SetGoldAmount(int value)
    {
        goldAmount = value;
        Debug.Log("Gold amount:" + goldAmount);
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
        IncreaseTotalTrashValue(value);
        Debug.Log("Trash value:" + currentTrashValue);
    }
    public void IncreaseTotalTrashValue(int value)
    {
        totalTrashValue += value;
    }

    public void BoatUpgrade(int toLevel, int speedIncrease, int rotationIncrease, int trashStorageIncrease)
    {
        boatLevelList[currentBoatLevel - 1].gameObject.SetActive(false);
        boatLevelList[toLevel - 1].gameObject.SetActive(true);
        currentBoatLevel = toLevel;
        moveSpeed += speedIncrease;
        rotationSpeed += rotationIncrease;
        maxTrashValue += trashStorageIncrease;
    }

    public InputAction GetInteractAction()
    {
        return interactAction;
    }


}




// ========== Old Handle Movement ==============
// private void HandleMovement()
// {
//     Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

//     inputVector = inputVector.normalized;

//     Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

//     float moveDistance = moveSpeed * Time.deltaTime;
//     float playerRadius = 1f;
//     float playerHeight = 2f;

//     // Layer Mask untuk mengecualikan layer "Player"
//     int layerMask = ~LayerMask.GetMask("Player");

//     // Gunakan CapsuleCastAll untuk mendapatkan semua collider yang bersinggungan
//     RaycastHit[] hits = Physics.CapsuleCastAll(
//         transform.position,
//         transform.position + Vector3.up * playerHeight,
//         playerRadius,
//         moveDir,
//         moveDistance,
//         layerMask
//     );

//     // Cek apakah ada collider yang bukan trigger
//     bool canMove = true;
//     foreach (RaycastHit hit in hits)
//     {
//         if (!hit.collider.isTrigger)
//         {
//             canMove = false;
//             break;
//         }
//     }

//     if (!canMove)
//     {
//         // Cant move toward moveDir

//         // Attempt only X movement
//         Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

//         hits = Physics.CapsuleCastAll(
//             transform.position,
//             transform.position + Vector3.up * playerHeight,
//             playerRadius,
//             moveDirX,
//             moveDistance,
//             layerMask
//         );

//         canMove = moveDir.x != 0;
//         foreach (RaycastHit hit in hits)
//         {
//             if (!hit.collider.isTrigger)
//             {
//                 canMove = false;
//                 break;
//             }
//         }

//         if (canMove)
//         {
//             // Can move only on the X
//             moveDir = moveDirX;
//         }
//         else
//         {
//             // Cannot move only on the X

//             // Attempt only Z movement
//             Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
//             hits = Physics.CapsuleCastAll(
//                 transform.position,
//                 transform.position + Vector3.up * playerHeight,
//                 playerRadius,
//                 moveDirZ,
//                 moveDistance,
//                 layerMask
//             );

//             canMove = moveDir.z != 0;
//             foreach (RaycastHit hit in hits)
//             {
//                 if (!hit.collider.isTrigger)
//                 {
//                     canMove = false;
//                     break;
//                 }
//             }

//             if (canMove)
//             {
//                 // Can move only on the Z
//                 moveDir = moveDirZ;
//             }
//             else
//             {
//                 // Cannot move any direction
//             }
//         }
//     }

//     if (canMove && !forcedStop)
//     {
//         transform.position += moveDir * moveDistance;
//     }

//     if (!forcedStop)
//     {
//         float rotateSpeed = 2f;
//         transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
//     }

//     isMoving = moveDir != Vector3.zero;
// }

