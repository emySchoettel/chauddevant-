using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
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

    public GameObject parent; 

    [Header("Buttons")]
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button previousButton; 
    public GameObject[] lines; 

     [SerializeField]
    private Button movementButton;
    [SerializeField]
    private Button tapButton; 

    [Header("Sound Options")]
    [SerializeField]
    private Slider sliderSoundOptions;
    [SerializeField]
    private TextMeshProUGUI valueSoundOptions; 

    private void Start() 
    {
        helper = GameObject.FindObjectOfType<Helper>() ?  GameObject.FindObjectOfType<Helper>() : null; 
        if(PlayerPrefs.HasKey("option_mode_jeu"))
        {
            string modejeustr = PlayerPrefs.GetString("option_mode_jeu");
            if(modejeustr == modejeu.boutons.ToString())   
            {
                locales[4].text = Translation.Get("options.description.boutons");
                tapButton.Select();

            }
            else if(modejeustr == modejeu.mouvements.ToString())
            {
                locales[4].text = Translation.Get("options.description.mouvements");
                movementButton.Select();
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

        //set volume music
        lines[2].GetComponentInChildren<Slider>().value = Helper.verifyVolumeMusic() >= 0 ? Helper.verifyVolumeMusic() : 50;
        GameObject.Find("Musique").GetComponent<AudioSource>().volume = (float)Helper.verifyVolumeMusic() / 100.0f;

        //attacher les skins aux gameobjects
        List<GameObject> parents = new List<GameObject>();
        
        for (int i = 1; i < 5; i++)
        {
            parents.Add(parent.transform.GetChild(i).gameObject);    
            Achats_v2.Skin skin = parent.transform.GetChild(i).transform.gameObject.AddComponent<Achats_v2.Skin>(); 
        }
        
        parents[0].GetComponent<Achats_v2.Skin>().changeTypeSkinEnum(Achats_v2.Skin.typeSkins.poele);
        parents[1].GetComponent<Achats_v2.Skin>().changeTypeSkinEnum(Achats_v2.Skin.typeSkins.planche);
        parents[2].GetComponent<Achats_v2.Skin>().changeTypeSkinEnum(Achats_v2.Skin.typeSkins.grille);
        parents[3].GetComponent<Achats_v2.Skin>().changeTypeSkinEnum(Achats_v2.Skin.typeSkins.bolo);

        //vérifier les achats des 3 skins 
        if(Helper.verifyAchat("achats.skin.planche"))
        {
            parents[2].SetActive(true);
        }
        if(Helper.verifyAchat("achats.skin.grille"))
        {
            parents[3].SetActive(true);
        }
        if(Helper.verifyAchat("achats.skin.bolo"))
        {
            parents[4].SetActive(true);
        }


        
        getAvatar(Helper.getNumberAvatar());
      
    }
    public void saveOptions()
    {
        //sauvegarder les options
        PlayerPrefs.SetString("option_mode_jeu",modedejeu.ToString());

        //Sound options 
        int volume = int.Parse(valueSoundOptions.text);
        PlayerPrefs.SetInt("options.musique.volume", volume);

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

    public void avatardClicked()
    {
        GameObject objectClicked = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if(objectClicked.transform.parent.GetComponent<Achats_v2.Skin>())
        {
            switch(objectClicked.transform.parent.GetComponent<Achats_v2.Skin>().typeSkinsEnum)
            {
                case Achats_v2.Skin.typeSkins.poele:
                    if(Helper.verifyAvatar("avatar.skin") != 1)
                    {
                        PlayerPrefs.SetInt("avatar.skin", 1);
                        getAvatar(1);
                    }
                break;

                case Achats_v2.Skin.typeSkins.planche:
                    if(Helper.verifyAvatar("avatar.skin") != 2)
                    {
                        PlayerPrefs.SetInt("avatar.skin.", 2);
                        getAvatar(2);
                    }
                break; 

                case Achats_v2.Skin.typeSkins.grille:
                    if(Helper.verifyAvatar("avatar.skin") != 3)
                    {
                        PlayerPrefs.SetInt("avatar.skin", 3);
                        getAvatar(3);
                    }
                break; 

                case Achats_v2.Skin.typeSkins.bolo:
                    if(Helper.verifyAvatar("avatar.skin") != 4)
                    {
                        PlayerPrefs.SetInt("avatar.skin", 4);
                        getAvatar(4);
                    }
                break; 
            }
        }   
    }

    private void getAvatar(int avatarNumber)
    {
        switch(avatarNumber)
        {
            case 1:
                parent.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                parent.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                parent.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                parent.transform.GetChild(4).GetChild(1).gameObject.SetActive(false);
               break;
            
            case 2:
                parent.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                parent.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                parent.transform.GetChild(4).GetChild(1).gameObject.SetActive(false);
                parent.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
               break;

            case 3:
                parent.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                parent.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                parent.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                parent.transform.GetChild(4).GetChild(1).gameObject.SetActive(false);
               break;

            case 4:
                parent.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
                parent.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                parent.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                parent.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            break;
       }
       Debug.Log(Helper.getNumberAvatar());
    }


    #region click and slider

    public void previousClick()
    {
        lines[2].SetActive(false);
        lines[0].SetActive(true);
        lines[1].SetActive(true);

        nextButton.interactable = true; 
        previousButton.interactable = false;
    }

    public void nextClick()
    {
        lines[2].SetActive(true);
        lines[0].SetActive(false);
        lines[1].SetActive(false);

        nextButton.interactable = false; 
        previousButton.interactable = true; 
    }

    public void changeValueSlider()
    {
        valueSoundOptions.text = sliderSoundOptions.value.ToString();
    }

    #endregion

}
