using System;
using System.Collections.Generic;
using UI.General_UI;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "New Material", order = 0)]
    public class  Material : ScriptableObject, IDisplayableItem
    {
        [SerializeField] private string materialName;
        [SerializeField] private Sprite materialSprite;
        [SerializeField] private int materialSortingOrder;
        [SerializeField] private int price;
        [SerializeField] private float craftingTime;
        [SerializeField] private float fusionTimeHours;
        [SerializeField] private int unlockLevel;
        [SerializeField] private AllSwordsInThisRank[] ranks = null;

        private static Dictionary<string, Material> materialLookupCache;
        
        private Dictionary<SwordRank, Sword[]> swordLookUpTable = null;

        public static Material GetFromNameOfMaterial(string nameOfMaterial)
        {
            if (materialLookupCache == null)
            {
                BuildMaterialLookupCache();
            }
            if (nameOfMaterial == null || !materialLookupCache.ContainsKey(nameOfMaterial)) return null;
            return materialLookupCache[nameOfMaterial];
        }

        public static List<string> GetListOfMaterials()
        {
            if (materialLookupCache == null)
            {
                BuildMaterialLookupCache();
            }

            var theList = new List<string>();
            foreach (KeyValuePair<string,Material> keyValuePair in materialLookupCache)
            {
                theList.Add(keyValuePair.Key);
            }
            
            theList.Sort((x , y) => GetFromNameOfMaterial(x).GetSortingOrder() - GetFromNameOfMaterial(y).GetSortingOrder());

            return theList;
        }
            

        
        public string GetMaterialName()
        {
            return materialName;
        }
        
        public Sprite GetMaterialSprite()
        {
            return materialSprite;
        }
        
        public int GetSortingOrder()
        {
            return materialSortingOrder;
        }
        
        public int GetPrice()
        {
            return price;
        }
        
        public float GetCraftingTime()
        {
            return craftingTime;
        }
        
        public int GetUnlockLevel()
        {
            return unlockLevel;
        }
        public Sword[] GetSwordsInThisRank(SwordRank swordRank)
        {
            BuildSwordLookUpTable();
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
        
        // from interface
        public string GetDisplayName()
        {
            return GetMaterialName();
        }

        public Sprite GetDisplaySprite()
        {
            return GetMaterialSprite();
        }

        public int GetDisplayOrder()
        {
            return GetSortingOrder();
        }

        private static void BuildMaterialLookupCache()
        {
            if (materialLookupCache != null) return;
            
            materialLookupCache = new Dictionary<string, Material>();
            var itemList = Resources.LoadAll<Material>("MaterialsSO");
            foreach (var item in itemList)
            {
                if (materialLookupCache.ContainsKey(item.materialName))
                {
                    Debug.LogError(string.Format(
                        "Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}",
                        materialLookupCache[item.materialName], item));
                    continue;
                }

                materialLookupCache[item.materialName] = item;
            }
        }
        private void BuildSwordLookUpTable()
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