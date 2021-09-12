using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EcranCreditManager : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> credits; 
    void Start()
    {
        credits[0].text = Translation.Get("credits.musique.titre");
        credits[1].text = Translation.Get("credits.musique.bgm.ecrantitre");
        credits[2].text = Translation.Get("credits.musique.bgm.jeu");
        credits[5].text = Translation.Get("credits.assets");

        credits[3].text = Translation.Get("credits.equipe.titre");
        credits[4].text = Translation.Get("credits.equipe.equipe");
    }
}
