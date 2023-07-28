using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCoins : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private int amount;
    //[SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform parent;
    private GameObject[] coins;
    private Vector3[] randomPosition;
    private float explodeDuration = 0.3f;
    private float flyDuration = 0.8f;
    
    void Start()
    {
        //StartCoroutine(SendCoins());
        
    }

    public void OnClickSendCoins(Transform clickPoint)
    {
        StartCoroutine(SendCoins(clickPoint));
    }

    IEnumerator SendCoins(Transform startPoint)
   {
       //Vector3 startPosition = startPoint.localPosition;
       coins = new GameObject[amount];
       randomPosition = new Vector3[amount];

       for (int i = 0; i < amount; i++)
       {
           coins[i] = Instantiate(coin, startPoint.position, new Quaternion());
           coins[i].transform.SetParent(parent, false);
           coins[i].transform.position = startPoint.position;
           var generatePosition = startPoint.position;
           generatePosition.x += Random.Range(-100, 100);
           generatePosition.y += Random.Range(-100, 100);
           randomPosition[i] = generatePosition;
           
           
       }
       
       

       float elaspedTime = 0;

       while (elaspedTime < explodeDuration)
       {
           
           elaspedTime += Time.deltaTime;
           var percentage = elaspedTime / explodeDuration;
           for (int i = 0; i < amount; i++) 
           {
               coins[i].transform.position = Vector3.Lerp(startPoint.position, randomPosition[i], percentage);
           }
           yield return null;
       }

       //yield return new WaitForSeconds((float)0.1);
       
       Vector3[] currentPosition = new Vector3[amount];
       for (int i = 0; i < amount; i++)
       {
           currentPosition[i] = coins[i].transform.position;
       }

       elaspedTime = 0;

       while (elaspedTime < flyDuration)
       {
           elaspedTime += Time.deltaTime;
           var percentage = elaspedTime / flyDuration;
           for (int i = 0; i < amount; i++)
           {
               coins[i].transform.position = Vector3.Lerp(currentPosition[i], endPoint.position, percentage);
           }

           yield return null;
       }

       
       yield return new WaitForSeconds((float)0.1);
       
       
       for (int i = 0; i < amount; i++)
       {
           GameObject.Destroy(coins[i]);
       }

   }
}
