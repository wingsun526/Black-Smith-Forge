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

        static Dictionary<string, Material> materialLookupCache;
        
        private Dictionary<SwordRank, Sword[]> swordLookUpTable = null;

        public static Material GetFromNameOfMaterial(string nameOfMaterial)
        {
            if (materialLookupCache == null)
            {
                materialLookupCache = new Dictionary<string, Material>();
                var itemList = Resources.LoadAll<Material>("MaterialSO");
                foreach (var item in itemList)
                {
                    if (materialLookupCache.ContainsKey(item.materialName))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", materialLookupCache[item.materialName], item));
                        continue;
                    }
        
                    materialLookupCache[item.materialName] = item;
                }
            }
        
            if (nameOfMaterial == null || !materialLookupCache.ContainsKey(nameOfMaterial)) return null;
            return materialLookupCache[nameOfMaterial];
        }
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
        
        public List<Sword> GetAllPossibleSwords()
        {
            List<Sword> allSwords = new List<Sword>();
            foreach (var rank in ranks)
            {
                foreach (var sword in rank.listOfSwords)
                {
                    allSwords.Add(sword);
                }
            }

            return allSwords;
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