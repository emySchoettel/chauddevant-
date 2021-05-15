using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sc_Fin : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI titre, scoring_actuel, scoring_best, argent; 
    private static string titre_str = "";

    [SerializeField]
    private List<TextMeshProUGUI> locales; 
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

        generateLocales(); 
    }

    public static void setTitre(string etatPartie)
    {
        titre_str = etatPartie;
    }

    private void generateLocales()
    {
        locales[0].text = Translation.Get("scoreactuel.titre");
        locales[1].text = Translation.Get("meilleurscore.titre"); 
        locales[2].text = Translation.Get("bouton.achats");
        locales[3].text = Translation.Get("bouton.menuprincipal");
    }
}
