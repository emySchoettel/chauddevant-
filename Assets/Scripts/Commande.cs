using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commande : MonoBehaviour
{
    public static List<GameObject> commande = new List<GameObject>(); 

    public static ItemController itemController; 

    public static Helper helper; 

    private static  GameObject[] nourrituresTab, positionsCommande;

    public static int commandeActuelle = 0; 

    private void Update() 
    {
        if(commandeActuelle == 3)
        {
            itemController.setEndGame(true);
            Sc_Fin.setTitre("Vous avez gagné !");
            GameObject.Find("Helper").GetComponent<Helper>().GoToLevelFin();
        }
    }

    private void OnEnable() 
    {
        helper = GameObject.Find("Helper").GetComponent<Helper>();
        itemController = GameObject.FindObjectOfType<ItemController>().GetComponent<ItemController>();
    }
    
    public static void preparerCommande()
    {
        Debug.Log("préparer commande");
        itemController = GameObject.FindObjectOfType<ItemController>(); 
        nourrituresTab = itemController.getTabNourritures(); 
        positionsCommande = itemController.getTabPositionsCommande(); 

        for(int i = 0; i < 3; i++)
        {
            //Preparer le numero aleatoire
            int indexItemCommande = Random.Range(0,nourrituresTab.Length);
            //Ajouter a la liste
            commande.Add(nourrituresTab[indexItemCommande]);
            positionsCommande[i].GetComponent<Image>().sprite = commande[i].GetComponent<SpriteRenderer>().sprite;
            positionsCommande[i].GetComponent<Image>().enabled = true;
            Helper.nourriture type = nourrituresTab[i].GetComponent<NourritureItem>().type;
            positionsCommande[i].AddComponent<CommandeItem>(); 
            positionsCommande[i].GetComponent<CommandeItem>().nourriture = type;  
        }
    }
}
