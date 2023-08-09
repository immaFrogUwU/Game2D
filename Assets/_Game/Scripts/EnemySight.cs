using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
            //Player va cham Enemy
        {
            enemy.SetTarget(collision.GetComponent<Character>());    
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //khi 2 dua het va cham thi khong set target nua
        if (collision.tag == "Player")
        { 
            enemy.SetTarget(null);
        }

    }
}
