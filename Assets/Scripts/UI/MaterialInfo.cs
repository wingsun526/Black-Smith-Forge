using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Info", menuName = "New MaterialInfo", order = 0 )]
public class MaterialInfo : ScriptableObject
{
    [SerializeField] private PickMaterial[] pickMaterials = null;

    private Dictionary<MaterialEnum, Dictionary<object, object>> lookupTable = null;

    private void BuildLookup()
    {
        if (lookupTable != null) return;
        lookupTable = new Dictionary<MaterialEnum, Dictionary<object, object>>();
        foreach (PickMaterial pickMaterial in pickMaterials)
        {
            var temp = new Dictionary<object, object>();
            foreach (PickSwordRank pickSwordRank in pickMaterial.productRank)
            {
                temp[pickSwordRank.swordRank] = pickSwordRank.swordsInThisRank;
            }
            temp["price"] = pickMaterial.price;
            lookupTable[pickMaterial.materialEnum] = temp;
        }
    }
    
    public int GetMaterialPrice(MaterialEnum materialEnum)
    {
        BuildLookup();
        return (int)lookupTable[materialEnum]["price"];
    }

    public SwordEnum[] GetPossibleProduct(MaterialEnum materialEnum, SwordRank swordRank)
    {
        BuildLookup();
        return (SwordEnum[])lookupTable[materialEnum][swordRank];
    }
    [Serializable]
    class PickMaterial
    {
        public MaterialEnum materialEnum;

        public int price;

        public PickSwordRank[] productRank;
    }

    [Serializable]
    class PickSwordRank
    {
        public SwordRank swordRank;
        public SwordEnum[] swordsInThisRank;

    }
}
