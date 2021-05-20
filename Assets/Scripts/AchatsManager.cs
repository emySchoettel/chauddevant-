using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class AchatsManager : MonoBehaviour
{
 
    [SerializeField]
    private List<TextMeshProUGUI> achats;


    private void Start() 
    {
        achats[0].text = Translation.Get("achats.bouton.skins");
        achats[1].text = Translation.Get("achats.bouton.competences");
    }
}
