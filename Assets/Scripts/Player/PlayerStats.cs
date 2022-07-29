using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int currentExp = 0;
        [SerializeField] ExpBarUI expBarUI;
        [Header("UI")] 
        [SerializeField] private Text levelText;

        
        
        //private int startingLevel = 1;
        private int currentLevel;
        private readonly int[] expRequireToLevelUp = new[] { -1, 100, 250, 400, 550, 700, 850 };
        private readonly int[] CulmulativeExp = new[] { 0, 100, 350, 750, 1300, 2000};
        
        

        private void Awake()
        {
            currentLevel = CalculateLevel();
            RefreshLevelText();
        }
        
        public void GainExp(int exp)
        {
            currentExp += exp;
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                // level up effects
            }

            RefreshLevelText();
            RefreshExpBarUI();
        }
        
        public int GetLevel()
        {
            return currentLevel;
        }

        private void RefreshExpBarUI()
        {
            int expToLevelUP = expRequireToLevelUp[currentLevel];
            int currentExpOnThisLevel = currentExp - CulmulativeExp[currentLevel - 1];
            float expLevelEndPoint = (float)currentExpOnThisLevel / expToLevelUP;
            
            //expSlider.value = ((float)currentExpOnThisLevel / expToLevelUP);
            expBarUI.AnimateChanges(expLevelEndPoint);
        }
        
        private void RefreshLevelText()
        {
            levelText.text = currentLevel.ToString();
        }
        private int CalculateLevel()
        {
            for (int i = 0; i < CulmulativeExp.Length; i++)
            {
                if(CulmulativeExp[i] > currentExp)
                {
                    return i;
                }
            }

            return CulmulativeExp.Length;
        }
    }
}