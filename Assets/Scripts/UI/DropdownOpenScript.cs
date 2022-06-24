using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownOpenScript : MonoBehaviour
{
    // Public variables
    [SerializeField] GameObject self;
    [SerializeField] GameObject controller;      //This is the object containing my controller script

    // Start is called before the first frame update
    void Start()
    {
        if (self.name == "Dropdown List")
        {
            //ControllScript is my main script controlling my program opperation.
            //It contains a public boolean bDropdownOpen to indicate whether the
            //dropdown list is open or not
            controller.GetComponent<OldShopUI>().bDropdownOpen = true;
        }
    }

    private void OnDestroy()
    {
        if (self.name == "Dropdown List")
        {
            controller.GetComponent<OldShopUI>().bDropdownOpen = false;
        }
    }
}
