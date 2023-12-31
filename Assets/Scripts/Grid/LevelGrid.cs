using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{

    public static LevelGrid Instance {get; private set;}

    public event EventHandler OnAnyUnitMoveGridPosition;
    [SerializeField] private Transform gridDebugObjectPrefab;
    private GridSystem gridSystem;
 
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There is more than one instance of LevelGrid" + transform + " - " + Instance); 
            Destroy(gameObject);
            return;
        }
        Instance = this;
        gridSystem = new GridSystem(10, 10, 2f);   
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void AddUnitatGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }
    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }


    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);

        AddUnitatGridPosition(toGridPosition, unit);

        OnAnyUnitMoveGridPosition?.Invoke (this, EventArgs.Empty);
    }


    public GridPosition GetGridPosition(Vector3 worldPosition) 
    {
        return gridSystem.GetGridPosition(worldPosition);
    }


    public Vector3 GetWorldPosition(GridPosition gridPosition) 
    {
        return gridSystem.GetWorldPosition(gridPosition);
    }


    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridSystem.IsValidGridPosition(gridPosition);
    }

    public int GetWidth() 
    {
        return gridSystem.GetWidth();
    }

        public int GetHeight() 
    {
        return gridSystem.GetHeight();
    }
    public bool HasUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }
    
    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

}

