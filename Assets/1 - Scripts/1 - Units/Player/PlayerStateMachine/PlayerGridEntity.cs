using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerGridEntity : GridEntity
{
    private PlayerInputActions _playerInputActions;
    private PlayerBaseState _currentState;
    private PlayerStateFactory _playerStateFactory;
    
    public Vector2 Direction { get; set; }
    public Vector2 MovementInput { get; set; }
    public bool InteractInput { get; set; }

    public float speed;
    
    private void Awake()
    {
        _playerStateFactory = new PlayerStateFactory(this);
        _playerInputActions = new PlayerInputActions();
        _currentState = _playerStateFactory.Idle();
        _currentState.EnterState();
    }

    void Update()
    {
        _currentState.UpdateState();
    }

    public void SwitchState(PlayerBaseState state)
    {
        _currentState.ExitState();
        _currentState = state;
        state.EnterState();
    }

    public void OnDrawGizmos()
    {
        _currentState?.OnDrawGizmos();
    }

    private void OnEnable()
    {
        _playerInputActions.CharacterControls.Movement.started += HandleMovement;
        _playerInputActions.CharacterControls.Movement.performed += HandleMovement;
        _playerInputActions.CharacterControls.Movement.canceled += HandleMovement;
        _playerInputActions.CharacterControls.Movement.Enable();

        _playerInputActions.CharacterControls.Interact.started += HandleInteract;
        _playerInputActions.CharacterControls.Interact.Enable();
    }
    
    private void OnDisable()
    {
        _playerInputActions.CharacterControls.Interact.Disable();
        _playerInputActions.CharacterControls.Movement.Disable();
    }
    
    private void HandleInteract(InputAction.CallbackContext obj)
    {
        InteractInput = true;
    }

    private void HandleMovement(InputAction.CallbackContext obj)
    {
        // var previousInput = MovementInput;
        var currentInput = _playerInputActions.CharacterControls.Movement.ReadValue<Vector2>();
        if (currentInput.sqrMagnitude == 0)
        {
            MovementInput = Vector2.zero;
            return;
        }
        
        if (currentInput.x > 0)
        {
            MovementInput = Vector2.right;
        }
        else if (currentInput.x < 0)
        {
            MovementInput = Vector2.left;
        }
        else if (currentInput.y > 0)
        {
            MovementInput = Vector2.up;
        }
        else if (currentInput.y < 0)
        {
            MovementInput = Vector2.down;
        }
    }

    public void ChangeAnimationState(string animationName)
    {
        var animator = GetComponent<Animator>();
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(animationName)) return;
        
        animator.Play(animationName);
    }
}