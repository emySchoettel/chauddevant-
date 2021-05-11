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
        Helper.updateArgent(Helper.pointActuel);
        scoring_actuel.text = Helper.pointActuel.ToString();
        if(PlayerPrefs.HasKey("meilleurScore"))
        {
            scoring_best.text = PlayerPrefs.GetInt("meilleurScore").ToString();
        }
        else
        {
            scoring_best.text = "0";
        }

        argent.text = Helper.getArgent().ToString();
    }

    public static void setTitre(string etatPartie)
    {
        titre_str = etatPartie;
    }
}
