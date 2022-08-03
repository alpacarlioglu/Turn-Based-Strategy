using System;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
   [SerializeField] private Transform actionButtonprefab;
   [SerializeField] private Transform actionButtonContainerTransform;
   
   private void Start()
   {
      UnitActionSystem.Instance.onSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
      CreateUnitActionButtons();
   }

   private void CreateUnitActionButtons()
   {
      foreach (Transform buttonTransform in actionButtonContainerTransform)
      {
         Destroy(buttonTransform.gameObject);
      }
      var selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

      foreach (var baseAction in selectedUnit.GetBaseActionArray())
      {
         Instantiate(actionButtonprefab, actionButtonContainerTransform);
      }
   }
   
   private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
   {
      CreateUnitActionButtons();
   }
}
