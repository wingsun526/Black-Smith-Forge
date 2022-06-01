using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ForgeButton : MonoBehaviour
{
    private TheForge _theForge;
    

    private void Start()
    {
        _theForge = GetComponent<TheForge>();
    }
    

    public void OnForgeButtonClick()
    {
    }
        
}
