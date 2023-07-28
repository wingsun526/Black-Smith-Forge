using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private List<Sprite> healthSprite;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float invincibleTimeAfterDamage;
    
    [SerializeField] private int maxHealth = 6;
    private Rigidbody2D myRigidbody2D;
    private PlayerMovement playerMovement;


    private float lastTimeBeingDamage;

    private int health;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        
        
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }
    private void UpdateHealthBar()
    {
        var listOfHearts = healthBar.GetComponentsInChildren<Image>();
        int quotient = health / 2;
        int remainder = health % 2;
        for (int i = 0; i < 3; i++)
        {
            if (quotient > i)
            {
                listOfHearts[i].sprite = healthSprite[2];
            }
            else if(quotient == i)
            {
                listOfHearts[i].sprite = healthSprite[remainder];
            }
            else
            {
                listOfHearts[i].sprite = healthSprite[0];
            }

            
        }
    }

    
}
