using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;
    
    private Vector3 targetPosition;

    private float stoppingDistance = 0.1f;
    private float rotateSpeed = 10.0f;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }
    
    private void Update()
    {
        if (!isActive) return;
        
        var moveDirection = (targetPosition - transform.position).normalized;
        
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            transform.position += moveDirection * (Time.deltaTime * moveSpeed);

            unitAnimator.SetBool("isWalking", true);
        }
        else
        {
            unitAnimator.SetBool("isWalking", false);
            isActive = false;
        }
        
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }
    
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete) 
    {
        this.onActionComplete = onActionComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
        onActionComplete();
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        
        GridPosition unitGridPosition = unit.GetGridPosition();
        
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                var offsetGridPosition = new GridPosition(x, z);
                var testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;

                if (unitGridPosition == testGridPosition) continue; // Same grid position where unit is already at

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue; // There is already a unit on this grid position
                
                validGridPositionList.Add(testGridPosition);
            }    
        }
        
        return validGridPositionList;
    }

    public override string GetActionName() => "Move";
}
