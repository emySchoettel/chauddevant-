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

    public static int commandesGenerales = 1;

    public static int commandeActuelle = 0; 

    private void Update() 
    {
        // if(commandesGenerales == 3)
        // {
        //     //Time.timeScale = 0f;
        //     Debug.Log("Commande générale 3");
        // }
        if(commandeActuelle == 2)
        {
            // Debug.Log("Commande actuelle 3");
            // renouvellerCommande(); 
            Sc_Fin.setTitre("Vous avez gagné !");
            GameObject.Find("Helper").GetComponent<Helper>().GoToLevelFin();
        }
    }

    private void OnEnable() 
    {
        helper = GameObject.Find("Helper").GetComponent<Helper>();
    }
    
    public static void preparerCommande()
    {
        Debug.Log("préparer commande");
        itemController = GameObject.FindObjectOfType<ItemController>(); 
        nourrituresTab = itemController.getTabNourritures(); 
        positionsCommande = itemController.getTabPositionsCommande(); 
        //Instantiate(new GameObject("Commande"), new Vector3(), Quaternion.identity);
        //GameObject.Find("Commande").transform.SetParent(GameObject.Find("Canvas").transform, false);

        //TODO randomize les instances 
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
    // public static void renouvellerCommande()
    // {
    //     Debug.Log("renouveller commande");
    //     if(commandesGenerales != 3)
    //     {
    //         commandesGenerales++; 
    //         commandeActuelle = 0;
    //         Inventaire.clearIntentaire();
    //         effacerCommande(); 
    //         preparerCommande();
            
    //     }
    //     else
    //     {

    //     }
    // }

    // public static void effacerCommande()
    // {
    //     foreach(GameObject itemCommande in positionsCommande)
    //     {
    //         itemCommande.GetComponent<Image>().sprite = null; 
    //     }
    // }
}
