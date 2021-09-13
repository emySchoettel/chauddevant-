using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 2f; 
    private bool move = true; 

    public static bool isFired = false; 

    private void Start() 
    {
        PlayerMouvement.tirs++; 
    }
    
    public void Move()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void Update()
    {
        if(move)   
            Move(); 
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        isFired = false;
        PlayerMouvement.tirs--;
        if(other.transform.CompareTag("Dechet"))
        {
            Destroy(other.gameObject);
            Helper.addPoints(10, true, Helper.item.dechet);
            move = false; 
        }
        else if(other.transform.CompareTag("Nourriture"))
        {
            Destroy(other.gameObject);
            Helper.addPoints(5, true, Helper.item.nourriture);
            move = false; 
        }
        else
            move = false; 
        
        
    }
}
