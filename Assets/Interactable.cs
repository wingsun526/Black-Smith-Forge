using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void startInteract()
    {
        Debug.Log($"interacting with {gameObject.name}");
    }
}
