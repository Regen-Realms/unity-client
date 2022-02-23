using UnityEngine;

public class GridEntity : MonoBehaviour
{
    public GridManager gridManager;
    
    public Vector2 GetGridPosition()
    {
        return (Vector3)gridManager.grid.WorldToCell(transform.position);
    }

    private void Start()
    {
        var currentGridPos = gridManager.grid.WorldToCell(transform.position);
        gridManager.RegisterGridEntity(this, GridLayer.Layer1, (Vector3)currentGridPos);
    }

    private void Update()
    {
        var currentGridPos = gridManager.grid.WorldToCell(transform.position);
        gridManager.RegisterGridEntity(this, GridLayer.Layer1, (Vector3)currentGridPos);
    }
}