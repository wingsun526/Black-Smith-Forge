using System;
using UnityEngine;

namespace UI.General_UI
{
    public interface IDisplayableItem
    {
        String GetDisplayName();
        Sprite GetDisplaySprite();

        int GetDisplayOrder();
    }
}