using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 2f; 
    private bool move = true; 
    
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
        if(other.transform.CompareTag("Dechet"))
        {
            Destroy(other.gameObject);
            Helper.addPoints(15, true, Helper.item.dechet);
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
