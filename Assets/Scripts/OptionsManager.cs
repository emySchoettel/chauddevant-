using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    private Helper helper; 

    [SerializeField]
    private List<TextMeshProUGUI> locales; 
    public enum modejeu
    {
        boutons,
        mouvements
    }

    [SerializeField]
    private modejeu modedejeu;

    private void Start() 
    {
        helper = GameObject.FindObjectOfType<Helper>() ?  GameObject.FindObjectOfType<Helper>() : null; 
        if(PlayerPrefs.HasKey("option_mode_jeu"))
        {
            string modejeustr = PlayerPrefs.GetString("option_mode_jeu");
            if(modejeustr == modejeu.boutons.ToString())   
            {
                  locales[4].text = Translation.Get("options.description.boutons");
            }
            else if(modejeustr == modejeu.mouvements.ToString())
            {
                  locales[4].text = Translation.Get("options.description.mouvements");
            }
            
        }
        else
        {
            locales[4].text = Translation.Get("options.description.mouvements");
        }

        //locales 
        locales[0].text = Translation.Get("options.titre"); 
        locales[1].text = Translation.Get("options.mode.titre"); 
        locales[2].text = Translation.Get("options.mouvements.titre");
        locales[3].text = Translation.Get("options.boutons.titre"); 
      
    }
    public void saveOptions()
    {
        //sauvegarder les options
        PlayerPrefs.SetString("option_mode_jeu",modedejeu.ToString());

        helper.click_retour_menu_principal();
    }

    public void choixmodedejeu()
    {
        GameObject currentBtn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if(currentBtn.CompareTag("Swipe"))
        {
            modedejeu = modejeu.mouvements;
            locales[4].text = Translation.Get("options.description.mouvements");
        }
        else if(currentBtn.CompareTag("Bouton"))
        {
            modedejeu = modejeu.boutons;
            locales[4].text = Translation.Get("options.description.boutons");
        }

    }
}
