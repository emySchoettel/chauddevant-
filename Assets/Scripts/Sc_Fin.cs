using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sc_Fin : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI titre, scoring_actuel, scoring_best, argent; 
    private static string titre_str = "";
    private void Awake() 
    {
        titre.text = titre_str; 
        Helper.updateScoreFinal();
        scoring_actuel.text = Helper.pointActuel.ToString();
        scoring_best.text = Helper.meilleurScore.ToString();
        
    }

    private void addArgent()
    {
            
    }
    public static void setTitre(string etatPartie)
    {
        titre_str = etatPartie;
    }
}
