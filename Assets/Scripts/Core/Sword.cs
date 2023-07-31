using System.Collections.Generic;
using UI.General_UI;
using UnityEngine;

namespace UI
{
    /*
     * is this the right way to you a scriptable object
     */
    
    
    [CreateAssetMenu(fileName = "SwordName", menuName = "New Sword", order = 0)]
    public class Sword : ScriptableObject, IDisplayableItem
    {
        [SerializeField] private string swordName;
        [SerializeField] private Sprite swordSprite;
        [SerializeField] private SwordRank swordRank;
        [Range(0, 10)]
        [SerializeField] private int Stars;
        [SerializeField] private SwordElement swordElement;
        [SerializeField] private Sword fusion;
        [SerializeField] private int AP;
        [SerializeField] private int DP;
        [SerializeField] private int sellPrice;
        [SerializeField] private int givesEXP;

        static Dictionary<string, Sword> swordLookupCache;

        public static Sword GetFromNameOfSword(string nameOfSword)
        {
            if (swordLookupCache == null)
            {
                swordLookupCache = new Dictionary<string, Sword>();
                var itemList = Resources.LoadAll<Sword>("SwordsSO");
                foreach (var item in itemList)
                {
                    if (swordLookupCache.ContainsKey(item.swordName))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", swordLookupCache[item.swordName], item));
                        continue;
                    }

                    swordLookupCache[item.swordName] = item;
                }
            }

            if (nameOfSword == null || !swordLookupCache.ContainsKey(nameOfSword)) return null;
            return swordLookupCache[nameOfSword];
        }
        public string GetSwordName()
        {
            return swordName;
        }
        
        
        
        public int GetSellPrice()
        {
            return sellPrice;
        }
        
        public int GetExp()
        {
            return givesEXP;
        }

        
        #region IDisplayableItem

        public string GetDisplayName()
        {
            return swordName;
        }

        public Sprite GetDisplaySprite()
        {
            return swordSprite;
        }

        public int GetDisplayOrder()
        {
            return -1; /* please change */
        }

        #endregion
        
    }
}