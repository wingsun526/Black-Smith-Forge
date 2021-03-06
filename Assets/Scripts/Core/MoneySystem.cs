using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private float gold = 0;

    [SerializeField] private Text goldText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = gold.ToString();
    }
    
    public void AddGold(int amount)
    {
        gold += amount;
    }
    
    public void SubtractGold(int amount)
    {
        if(HasMoney(amount))
        {
            gold -= amount;
        }
        else
        {
            throw new Exception("not enough money");
        }
    }
    
    public bool HasMoney(int amount)
    {
        return gold >= amount;
    }
    //visual effect
    private void floatingText()
    {
        
    }
}
