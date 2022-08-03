using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        public static GridSystemVisual Instance { get; private set; }
        
        [SerializeField] private Transform gridSystemVisualSinglePrefab;
        
        private GridSystemVisualSingle[,] gridSystemVisualSingleArray;

        private void Awake()
        {
            if (Instance != null)
            {
                print("GridSystemVisual already exists");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            gridSystemVisualSingleArray = new GridSystemVisualSingle[
                LevelGrid.Instance.GetWidth(), 
                LevelGrid.Instance.GetHeight()];
            
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    
                    Transform gridSystemVisualSingleTransform = 
                        Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                    
                    gridSystemVisualSingleArray[x, z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
                }
            }
        }

        private void Update()
        {
            UpdateGradeVisual();
        }

        public void HideAllGridPositions()
        {
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    gridSystemVisualSingleArray[x, z].Hide();
                }
            }
        }
    
        public void ShowGridPositionList(List<GridPosition> gridPositionList)
        {
            foreach (GridPosition gridPosition in gridPositionList)
            {
                gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
            }
        }

        private void UpdateGradeVisual()
        {
           HideAllGridPositions();
           
           var selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
           ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
        }
    }
}
