public class PlayerStateFactory
{
    private readonly PlayerGridEntity _context;

    public PlayerStateFactory(PlayerGridEntity currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle() => new PlayerIdleState(_context, this);
    public PlayerBaseState Walk() => new PlayerWalkState(_context, this);
}