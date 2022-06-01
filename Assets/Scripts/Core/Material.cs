using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "New Material", order = 0)]
    public class Material : ScriptableObject
    {
        [SerializeField] private string materialName;
        [SerializeField] private Sprite materialSprite;
        [SerializeField] private int price;
        [SerializeField] private float craftingTime;
        [SerializeField] private float fusionTimeHours;
        [SerializeField] private AllSwordsInThisRank[] ranks = null;

        private Dictionary<SwordRank, Sword[]> swordLookUpTable = null;

        public string GetMaterialName()
        {
            return materialName;
        }
        
        public Sprite GetMaterialSprite()
        {
            return materialSprite;
        }
        
        public float GetCraftingTime()
        {
            return craftingTime;
        }
        public Sword[] GetSwordsInThisRank(SwordRank swordRank)
        {
            BuildLookUpTable();
            return swordLookUpTable[swordRank];
        }
        
        private void BuildLookUpTable()
        {
            if (swordLookUpTable != null) return;
            swordLookUpTable = new Dictionary<SwordRank, Sword[]>();

            foreach (var item in ranks)
            {
                swordLookUpTable[item.swordRank] = item.listOfSwords;
            }
        }

        [Serializable]
        class AllSwordsInThisRank
        {
            public SwordRank swordRank;
            public Sword[] listOfSwords;
        }
    }
}