using System;
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
    
        public void Spin(Action onSpinComplete)
        {
            onActionComplete = onSpinComplete;
            isActive = true;
            totalSpinAmount = 0;
        }
    }
}
