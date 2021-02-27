using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; 

public class NourritureItem : MonoBehaviour, Item
{
    [HideInInspector] public static float speed = 2f;
    private bool move = true; 
    public Helper.directions directionActuelle; 
    public Sprite sprite;  
    public Helper.nourriture type;
    public GameManager gm; 

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
            if(other.transform.CompareTag("Player"))
            {
                if(Commande.commandeActuelle < 4)
                {
                    if(Commande.commande[Commande.commandeActuelle].GetComponent<NourritureItem>().type == this.type)
                    {
                        Inventaire.addItemInventaire(gameObject.GetComponent<NourritureItem>());
                        Helper.addPoints(20, false);
                        move = false;  
                        Commande.commandeActuelle++; 
                    }
                    else
                    {
                        gm.dechetToPlayer();
                    }
                }
            }
            else if(other.transform.CompareTag("Bound"))
            {
                move = false; 
            }
        }
       
    }
}
