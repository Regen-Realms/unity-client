using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private readonly PlayerGridEntity _ctx;
    private readonly PlayerStateFactory _playerStateFactory;

    public PlayerIdleState(PlayerGridEntity currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        _ctx = currentContext;
        _playerStateFactory = playerStateFactory;
    }
    
    public override void EnterState()
    {
        Debug.Log("Idle state!");
        
        if (_ctx.Direction == Vector2.right)
        {
            _ctx.ChangeAnimationState("Idle_Right");
        }
        else if (_ctx.Direction == Vector2.left)
        {
            _ctx.ChangeAnimationState("Idle_Left");
        }
        else if (_ctx.Direction == Vector2.up)
        {
            _ctx.ChangeAnimationState("Idle_Up");
        }
        else if (_ctx.Direction == Vector2.down)
        {
            _ctx.ChangeAnimationState("Idle_Down");
        }
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        // Interaction
        if (_ctx.InteractInput)
        {
            _ctx.InteractInput = false;
            
            var desiredTargetPosition =  _ctx.transform.position + (Vector3)_ctx.Direction;
            var desiredTargetGridPos = _ctx.gridManager.grid.WorldToCell(desiredTargetPosition);

            var targetGridEntity = _ctx.gridManager.GetGridEntityAt(GridLayer.Layer1, (Vector3)desiredTargetGridPos);
            var targetGridEntityInteraction = targetGridEntity.GetComponent<IInteractable>();
            if (targetGridEntityInteraction != null)
            {
                targetGridEntityInteraction.OnInteract(targetGridEntity);
                return;
            }

            if (_ctx.Direction == Vector2.right)
            {
                _ctx.ChangeAnimationState("Idle_Right");
            }
            else if (_ctx.Direction == Vector2.left)
            {
                _ctx.ChangeAnimationState("Idle_Left");
            }
            else if (_ctx.Direction == Vector2.up)
            {
                _ctx.ChangeAnimationState("Idle_Up");
            }
            else if (_ctx.Direction == Vector2.down)
            {
                _ctx.ChangeAnimationState("Idle_Down");
            }
        }
        
        // Movement
        if (_ctx.MovementInput.sqrMagnitude != 0)
        {
            _ctx.Direction = (Vector3)_ctx.MovementInput;

            // Check next cell is empty
            var desiredTargetPosition = _ctx.transform.position + (Vector3)_ctx.MovementInput;
            var desiredTargetGridPos = _ctx.gridManager.grid.WorldToCell(desiredTargetPosition);
            if (!_ctx.gridManager.IsGridCellEmpty(GridLayer.Layer1, (Vector3)desiredTargetGridPos))
            {
                //Debug.Log("Blocked!");

                if (_ctx.Direction == Vector2.right)
                {
                    _ctx.ChangeAnimationState("Idle_Right");
                }
                else if (_ctx.Direction == Vector2.left)
                {
                    _ctx.ChangeAnimationState("Idle_Left");
                }
                else if (_ctx.Direction == Vector2.up)
                {
                    _ctx.ChangeAnimationState("Idle_Up");
                }
                else if (_ctx.Direction == Vector2.down)
                {
                    _ctx.ChangeAnimationState("Idle_Down");
                }

                return;
            }

            SwitchState(_playerStateFactory.Walk());
        }
    }
}