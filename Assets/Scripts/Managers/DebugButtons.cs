using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButtons : MonoBehaviour
{
    const float baseScreenWidth = 257.0f;
    const float baseScreenHeight = 456.0f;
    const int buttonFontSize = 12;

    const float _buttonHeight = 26f;

    
    float buttonHeight
    {
        get { return Mathf.Max(_buttonHeight, _buttonHeight * (Screen.height / baseScreenHeight)); }
    }

    const float _buttonWidth = 20.0f;

    float buttonWidth
    {
        get { return Mathf.Max(_buttonWidth, _buttonWidth * (Screen.width / baseScreenWidth)); }
    }

    const float padding = 10;
    const float _debugButtonWidth = 27f;

    protected List<DebugButton> topMidButtons = new List<DebugButton>();


    public bool showDebugButtons = false;

    private void Start()
    {
        topMidButtons.AddRange(new DebugButton[]
        {
            // // Two ways of adding debug button,  this
            // //new DebugButton("dmg player", () => PlayerController.Instance.GetHit(new DamageData(1))),
            // new DebugButton("Invincible", () => { TestGameplay.Instance.PlayerInvincible = !TestGameplay.Instance.PlayerInvincible; }),
            //
            // // or this,  if your code needs to be longer
            // new DebugButton("Boss", () => { EnemyManagerLocator.GetCurrentEnemyManager.SpawnBoss(); }),
            new DebugButton("Testing", () => { Debug.Log(transform.childCount);})
        });
    }

    void OnGUI()
    {
        // just button drawing logic   

        if (!showDebugButtons) return;

        int xSize = Screen.width / (int)buttonWidth;
        for (int i = 0; i < topMidButtons.Count; i++)
        {
            int column = i % xSize;
            int row = i / xSize;

            float xPos = column * buttonWidth;
            float yPos = row * buttonHeight;

            if (GUI.Button(new Rect(padding + xPos, yPos, buttonWidth, buttonHeight), topMidButtons[i].functionName))
            {
                topMidButtons[i].action();
            }
        }
    }


    public class DebugButton
    {
        public string functionName;
        public Action action;

        public DebugButton(string functionName, Action action)
        {
            this.functionName = functionName;
            this.action = action;
        }
    }
}