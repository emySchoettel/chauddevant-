using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{

    public static List<NourritureItem> inventaire = new List<NourritureItem>(); 
    public static int position = 1; 
    private static GameObject[] tab_inventaire_position; 

    public static ItemController itemController;

    public static void createInventaire()
    {
        itemController = GameObject.FindObjectOfType<ItemController>();
        tab_inventaire_position = itemController.getTabPositionsInventaire(); 
    }

    public static void addItemInventaire(NourritureItem item)
    {
        //Ajouter dans l'inventaire 
        inventaire.Add(item); 

       // mettre à jour le visuel 
        switch(position)
        {
            //Gauche
            case 1: 
               tab_inventaire_position[position -1].SetActive(true);
                position++;
            break; 
            case 2: 
                tab_inventaire_position[position -1].SetActive(true);
                position++;
            break; 
            case 3: 
                tab_inventaire_position[position -1].SetActive(true);
            break; 
        }
    }

    // public static void clearIntentaire()
    // {
    //     foreach(GameObject inventaire in tab_inventaire_position)
    //     {
    //         inventaire.SetActive(false);
    //     }
    // }
}
