using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    private readonly PlayerGridEntity _ctx;
    private readonly PlayerStateFactory _playerStateFactory;

    private Vector3 _targetPosition;

    public PlayerWalkState(PlayerGridEntity currentContext, PlayerStateFactory playerStateFactory) : base(
        currentContext, playerStateFactory)
    {
        _ctx = currentContext;
        _playerStateFactory = playerStateFactory;
    }

    public override void EnterState()
    {
#pragma warning disable CS4014
        OnEnterState();
#pragma warning restore CS4014
    }

    private async UniTaskVoid OnEnterState()
    {
        Debug.Log("Walk state!");

        while (_ctx.MovementInput.sqrMagnitude > 0)
        {
            _ctx.Direction = (Vector3)_ctx.MovementInput;
            if (_ctx.Direction == Vector2.right)
            {
                _ctx.ChangeAnimationState("Walk_Right");
            }
            else if (_ctx.Direction == Vector2.left)
            {
                _ctx.ChangeAnimationState("Walk_Left");
            }
            else if (_ctx.Direction == Vector2.up)
            {
                _ctx.ChangeAnimationState("Walk_Up");
            }
            else if (_ctx.Direction == Vector2.down)
            {
                _ctx.ChangeAnimationState("Walk_Down");
            }
            
            // Check next cell is empty
            var desiredTargetPosition =  _ctx.transform.position + (Vector3)_ctx.MovementInput;
            var desiredTargetGridPos = _ctx.gridManager.grid.WorldToCell(desiredTargetPosition);
            if (!_ctx.gridManager.IsGridCellEmpty(GridLayer.Layer1, (Vector3)desiredTargetGridPos))
            {
                Debug.Log("Blocked!");
                SwitchState(_playerStateFactory.Idle());
                return;
            }
            
            _targetPosition = _ctx.transform.position + (Vector3)_ctx.MovementInput;

            // Register player to grid cell
            var targetGridPos = _ctx.gridManager.grid.WorldToCell(_targetPosition);
            _ctx.gridManager.RegisterGridEntity(_ctx, GridLayer.Layer1, new Vector2(targetGridPos.x, targetGridPos.y));
            
            while (_ctx.transform.position != _targetPosition)
            {
                var step = _ctx.speed * Time.deltaTime;
                _ctx.transform.position = Vector3.MoveTowards(_ctx.transform.position, _targetPosition, step);
                await UniTask.Yield();
            }
        }

        SwitchState(_playerStateFactory.Idle());
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void OnDrawGizmos()
    {
        // Gizmos.color = Color.green;
        // Gizmos.DrawSphere(_targetPosition, 0.2f);
    }
}