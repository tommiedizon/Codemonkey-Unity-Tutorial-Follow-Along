using UnityEngine;
using System;
using System.Linq.Expressions;

public class PlayerMovement : MonoBehaviour, IKitchenObjectParent
{

    public static PlayerMovement Instance { get; private set; } //only for singleton pattern

    public event EventHandler<OnSelectedCounterchangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterchangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float speed = 8f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private Vector3 lastMovementDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    private bool isWalking;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }

        Instance = this;
    }

    private void Start()
    {
        // always listen on start() and not awake()
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAltAction += GameInput_OnInteractAltAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAltAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlt(this);
        }
    }

    private void Update(){
        HandleMovement();
        HandleInteractions();
        return;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastMovementDirection = moveDir;
        }

        float interactDistance = 2f;

        bool result = Physics.Raycast(transform.position,
                            lastMovementDirection,
                            out RaycastHit hitInfo, //output parameter
                            interactDistance,
                            counterLayerMask);
       if(result)
        {
            if(hitInfo.transform.TryGetComponent(out BaseCounter baseCounter))
            {

                if (baseCounter != selectedCounter)
                    SetSelectedCounter(baseCounter);
            } else
            {
                SetSelectedCounter(null);
            }
        } else
        {
            SetSelectedCounter(null);
        }
        
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        isWalking = (moveDir != Vector3.zero);

        float moveDistance = Time.deltaTime * speed;
        float playerHeight = 2f;
        float playerRadius = 0.7f;
        Vector3 playerHead = transform.position + Vector3.up * playerHeight;
        Vector3 playerPosition = transform.position;

        bool canMove = !Physics.CapsuleCast(playerPosition, playerHead, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // Cannot move towards moveDir 

            //Can't move in moveDir, try x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(playerPosition, playerHead, playerRadius, moveDirX, moveDistance);

            if(canMove) {
                moveDir = moveDirX;
            } else {
                // Can't move in x or moveDir, Try z movement
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(playerPosition, playerHead, playerRadius, moveDirZ, moveDistance);

                if(canMove) {
                    moveDir = moveDirZ;
                } else {
                    //can't move at all
                }
            }

        }   
        if (canMove)
            transform.position += moveDir * moveDistance;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterchangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint.transform;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public bool IsWalking() {
        return isWalking;
    }
}

