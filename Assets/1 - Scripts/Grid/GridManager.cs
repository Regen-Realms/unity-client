using System.Collections.Generic;
using UnityEngine;

public enum GridLayer
{
    Ground,
    Layer1,
    Layer2,
    Air
}

public class GridManager : MonoBehaviour
{
    private Vector2 mapSize = new Vector2(10, 10);

    public Grid grid;

    private Dictionary<(GridLayer, Vector2), GridEntity> GridEntities { get; set; }
    private Dictionary<GridEntity, (GridLayer, Vector2)> GameObjectToGridEntityRegistration { get; set; }

    public void RegisterGridEntity(GridEntity gridEntity, GridLayer layer, Vector2 position)
    {
        // Check if another GridEntity is already registered here
        if (GridEntities.ContainsKey((layer, position)))
        {
            var existingGameObject = GridEntities[(layer, position)];
            if (existingGameObject != null && !existingGameObject.Equals(gridEntity))
            {
                Debug.LogError("Something is already here!!!");
            }
        }

        // Unregister previous position if necessary
        if (GameObjectToGridEntityRegistration.ContainsKey(gridEntity))
        {
            GridEntities[GameObjectToGridEntityRegistration[gridEntity]] = null;
        }

        // Register grid entity to grid position
        GridEntities[(layer, position)] = gridEntity;
        GameObjectToGridEntityRegistration[gridEntity] = (layer, position);
    }

    public bool IsGridCellEmpty(GridLayer layer, Vector2 position)
    {
        return GridEntities?[(layer, position)] == null;
    }

    public GridEntity GetGridEntityAt(GridLayer layer, Vector2 position)
    {
        return GridEntities?[(layer, position)];
    }

    // public void InteractWithGameEntity(GridLayer layer, Vector2 position)
    // {
    //     var gridEntity = GridEntities?[(layer, position)];
    //
    //     if (gridEntity == null)
    //     {
    //         Debug.Log("There is nothing to interact with!");
    //     }
    //     else
    //     {
    //         var script = gridEntity.GetComponent<GridEntity>()._interactable;
    //         script?._interactable.OnInteract();
    //     }
    // }

    // public void UnregisterGridEntity(GameObject gameObj)
    // {
    //     if (!GameObjectToGridEntityRegistration.ContainsKey(gameObj)) return;
    //     
    //     var registrationKey = GameObjectToGridEntityRegistration[gameObj];
    //     GridEntities[registrationKey] = null;
    //     GameObjectToGridEntityRegistration.Remove(gameObj);
    // }

    public void Awake()
    {
        GameObjectToGridEntityRegistration = new Dictionary<GridEntity, (GridLayer, Vector2)>();
        GridEntities = new Dictionary<(GridLayer, Vector2), GridEntity>();
        for (var x = 0; x < mapSize.x; x++)
        for (var y = 0; y < mapSize.y; y++)
        {
            GridEntities[(GridLayer.Ground, new Vector2(x, y))] = null;
            GridEntities[(GridLayer.Layer1, new Vector2(x, y))] = null;
            GridEntities[(GridLayer.Layer2, new Vector2(x, y))] = null;
            GridEntities[(GridLayer.Air, new Vector2(x, y))] = null;
        }
    }

    public void OnDestroy()
    {
        //UnregisterGridEntity(gameObject);
    }

    private void OnDrawGizmos()
    {
        DrawLayerGizmo(GridLayer.Ground, new Vector2(0.2f, 0.2f));
        DrawLayerGizmo(GridLayer.Layer1, new Vector2(0.2f, 0.4f));
        DrawLayerGizmo(GridLayer.Layer2, new Vector2(0.2f, 0.6f));
        DrawLayerGizmo(GridLayer.Air, new Vector2(0.2f, 0.8f));
    }

    private void DrawLayerGizmo(GridLayer gridLayer, Vector2 offset)
    {
        for (var x = 0; x < mapSize.x; x++)
        for (var y = 0; y < mapSize.y; y++)
        {
            var gameObj = GridEntities?[(gridLayer, new Vector2(x, y))];
            Gizmos.color = gameObj != null ? Color.blue : Color.gray;

            var worldPos = grid.CellToWorld(new Vector3Int(x, y, 0));
            Gizmos.DrawCube(worldPos + (Vector3)offset, new Vector3(0.1f, 0.1f, 0));
        }
    }
}