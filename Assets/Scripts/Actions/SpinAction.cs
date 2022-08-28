using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public class SpinAction : BaseAction
    {
        private float totalSpinAmount;
        
        private void Update()
        {
            if (!isActive) return;
        
            var spinAddAmount = Time.deltaTime * 360.0f;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        
            totalSpinAmount += spinAddAmount;
            if (totalSpinAmount >= 360f)
            {
                isActive = false;
                onActionComplete();
            }
        }
    
        public override void TakeAction(GridPosition gridPosition, Action onSpinComplete)
        {
            onActionComplete = onSpinComplete;
            isActive = true;
            totalSpinAmount = 0;
        }
        
        public override string GetActionName() => "Spin";

        public override List<GridPosition> GetValidActionGridPositionList()
        {
            GridPosition unitGridPosition = unit.GetGridPosition();

            return new List<GridPosition>()
            {
                unitGridPosition
            };
        }
    }
}
