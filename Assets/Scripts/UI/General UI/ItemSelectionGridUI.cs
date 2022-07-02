using System.Collections;
using System.Collections.Generic;
using UI.General_UI;
using UnityEngine;

public class ItemSelectionGridUI : MonoBehaviour
{
    [SerializeField] private Object someInventory;
    public IInventory targetInventory => someInventory as IInventory;
    
    //[SerializeField] private IInventory targetInventory;
    //[SerializeField] private BaseInventory BI;
    [SerializeField] private SingleSelectionItemUI itemPrefab;
    
    private void Awake()
    {
        //BI.OnInventoryChanged += DrawItems;//
        DrawItems();
        
    }
    
    private void DrawItems()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var listOfItems = targetInventory.GetListOfItemsInOrder();
        //list should be already sorted.

        for (int i = 0; i < listOfItems.Count; i++)
        {
            var item = Instantiate(itemPrefab, transform);
            item.Setup(listOfItems[i]);
            
        }
    }
   
    // private void DrawItems()
    // {
    //     foreach (Transform child in transform)
    //     {
    //         Destroy(child.gameObject);
    //     }
    //
    //     var listOfItems = targetInventory.GetListOfItemsInOrder();
    //     //list should be already sorted.
    //
    //     for (int i = 0; i < listOfItems.Count; i++)
    //      {
    //          var item = Instantiate(itemPrefab, transform);
    //          item.Setup(listOfItems[i]);
    //          // var material = Material.GetFromNameOfMaterial(listOfMaterials[i]);
    //          // item.Setup(material, materialInventory, moneySystem);
    //      }
    // }
}


