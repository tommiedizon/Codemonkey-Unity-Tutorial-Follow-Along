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
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
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
                // Has ClearCounter
                // TryGetComponent is a getter w/ in built null check
                // tests if it is of type ClearCounter

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

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 2f, 0.7f, moveDir, Time.deltaTime * speed);

        if (canMove)
            transform.position += moveDir * Time.deltaTime * speed;
        //else
        //    transform.position += SplitMovement(moveDir, speed) * Time.deltaTime * speed;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        // How does Lerp and Slerp work?
    }

    private Vector3 SplitMovement(Vector3 moveDir, float speed)
    {
        Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
        Vector3 moveDirY = new Vector3(0f, 0f, moveDir.y).normalized;

        Debug.Log("X"+moveDirX+",Y:"+moveDirY);

        bool canMoveX = !Physics.Raycast(transform.position, moveDirX, Time.deltaTime*speed);
        bool canMoveY = Physics.Raycast(transform.position, moveDirY, Time.deltaTime*speed);

        if (canMoveX)
        {
            Debug.Log("Success");

            return moveDirX;
        }

        if (canMoveY)
            return moveDirY;

        return Vector3.zero;
    }   

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = baseCounter;
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

    public bool hasKitchenObject()
    {
        return kitchenObject != null;
    }
}

