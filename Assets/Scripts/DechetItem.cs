﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
public class DechetItem : MonoBehaviour, Item
{
    [HideInInspector] public static float speed = 2f;

    private bool move = true; 

    private GameManager gm; 

    private void Awake() 
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void Move()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
            Move();
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!gm.GetItemController().getEndGame())
        {
            if(other.transform.CompareTag("Bound"))
            {
                move = false;
                Destroy(gameObject);
            }
            else if(other.transform.CompareTag("Player"))
            {
                if(gm.getPlayer().GetComponent<PlayerVies>().haslifes())
                {
                    gm.dechetToPlayer();
                }
                move = false; 
            }
        }
        if(other.transform.CompareTag("Player"))
        {
            if(gm.getPlayer().GetComponent<PlayerVies>().haslifes())
            {
                gm.dechetToPlayer();
            }
            move = false; 
        }
    }
}
