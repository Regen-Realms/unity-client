using UnityEditor;
using UnityEngine;

public abstract class PlayerBaseState
{
    private readonly PlayerGridEntity _currentContext;
    private readonly PlayerStateFactory _playerStateFactory;

    public PlayerBaseState(PlayerGridEntity currentContext, PlayerStateFactory playerStateFactory)
    {
        _currentContext = currentContext;
        _playerStateFactory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();

    protected void SwitchState(PlayerBaseState newState)
    {
        _currentContext.SwitchState(newState);
    }

    public virtual void OnDrawGizmos()
    {
        // Debug.Log($"Drawings Gizmos from {this}");
    }
}