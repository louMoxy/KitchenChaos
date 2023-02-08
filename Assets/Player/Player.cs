using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounters selectedCounter;
    }

    [SerializeField] float moveSpeed = 7f;
    [SerializeField] GameInput gameInput;
    [SerializeField] LayerMask countersLayerMask;
    [SerializeField] Transform kitchenOjbectHoldPoint;

    KitchenObject kitchenObject;
    bool isWalking = false;
    Vector3 lastInteractDir;
    ClearCounters selectedCounter;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("there is more than one player");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter != null )
        {
            selectedCounter.Interact(this);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();

    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if(moveDir !=Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        bool hitItem = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask);
        if(hitItem)
        {
            if(raycastHit.transform.TryGetComponent(out ClearCounters clearCounters))
            {
                if(clearCounters != selectedCounter)
                {
                    SetSelectedCounter(clearCounters);
                } 
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    void SetSelectedCounter(ClearCounters selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    bool CanMove(Vector3 moveDir, float moveDistance)
    {
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
    }

    void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);


        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = CanMove(moveDir, moveDistance);

        if (!canMove)
        {
            // attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = CanMove(moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirY = new Vector3(0, 0, moveDir.y).normalized;
                canMove = CanMove(moveDirY, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirY;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
            isWalking = moveDir != Vector3.zero;

        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenOjbectHoldPoint;
    }

    public void SetKichenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
