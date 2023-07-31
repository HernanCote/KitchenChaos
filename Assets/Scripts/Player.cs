using System;
using UnityEngine;
using Events;
using Interfaces;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    
    private const float ROTATE_SPEED = 12f;
    
    private const float PLAYER_RADIUS = .7f;
    private const float PLAYER_HEIGHT = 2f;
    private const float INTERACT_DISTANCE = 2f;
    
    [SerializeField]
    private float moveSpeed = 7f;
    [SerializeField]
    private GameInput gameInput;
    [SerializeField]
    private LayerMask countersLayerMask;
    [SerializeField] 
    private Transform kitchenObjectHoldPoint;

    private bool _isWalking;
    private Vector3 _lastInteractDirection;
    private ClearCounter _selectedCounter;
    private KitchenObject _kitchenObject;
    
    public event EventHandler<OnSelectedCounterEventArgs> OnSelectedCounterChanged;


    private void Awake()
    {
        if (Instance is not null)
            Debug.LogError("There is more than one Player instance in the scene!");
        
        Instance = this;
    }
    
    private void Start()
    {
        gameInput.OnInteractAction += GameInputOnInteractAction;
    }
    
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking => _isWalking;

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        
        var currentPosition = transform.position;
        var moveDistance = moveSpeed * Time.deltaTime;
        
        if (!PlayerCanMove(currentPosition, moveDirection, moveDistance))
        {
            // Attempt move direction on the x axis
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0f, 0f).normalized;

            if (PlayerCanMove(currentPosition, moveDirectionX, moveDistance))
            {
                moveDirection = moveDirectionX;    
            }
            else
            {
                // Attempt move direction on the z axis
                Vector3 moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;

                if (PlayerCanMove(currentPosition, moveDirectionZ, moveDistance))
                    moveDirection = moveDirectionZ;
            }
        }
        
        if (PlayerCanMove(currentPosition, moveDirection, moveDistance))
            transform.position += moveDirection * moveDistance;
        
        _isWalking = moveDirection != Vector3.zero;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * ROTATE_SPEED);
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        
        if (moveDirection != Vector3.zero)
            _lastInteractDirection = moveDirection;
        
        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, INTERACT_DISTANCE, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != _selectedCounter)
                    SetSelectedCounter(clearCounter);
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

    private static bool PlayerCanMove(Vector3 currentPosition, Vector3 moveDirection, float moveDistance) =>
        !Physics.CapsuleCast(currentPosition, currentPosition + Vector3.up * PLAYER_HEIGHT, PLAYER_RADIUS, moveDirection, moveDistance);

    private void GameInputOnInteractAction(object sender, System.EventArgs e)
    {
        if (_selectedCounter is not null)
        {
            _selectedCounter.Interact(this);
        }
    }


    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterEventArgs { SelectedCounter = _selectedCounter});
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }
    
    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }
    
    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }
    
    public bool HasKitchenObject()
    {
        return _kitchenObject is not null;
    }
}
