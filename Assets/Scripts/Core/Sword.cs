using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "SwordName", menuName = "New Sword", order = 0)]
    public class Sword : ScriptableObject
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

        public string GetSwordName()
        {
            return swordName;
        }
        
        public Sprite GetSwordSprite()
        {
            return swordSprite;
        }
        
        public int GetSellPrice()
        {
            return sellPrice;
        }
    }
}