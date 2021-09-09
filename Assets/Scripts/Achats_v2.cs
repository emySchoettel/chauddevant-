﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Achats_v2 : MonoBehaviour
{

    // [SerializeField]
    // private List<Article> Competences;
    [SerializeField]
    private Button skinBtn; 

    [SerializeField]
    private Button compBtn; 

    public bool isSkinClicked = false; 

    public bool isCompetencesClicked = false; 

     [SerializeField]
    private Button nextButton; 

    [SerializeField]
    private Button previousButton; 

    [SerializeField]
    private List<Competence> competences; 

    [System.Serializable]
    public class Competence
    {
        public enum typeBonus 
        {
            casserole,
            louche, 
            rappe,
            biere
        }

        public typeBonus typeBonusEnum;
        public string nom;
        public string description;
        public int prix; 
        public Sprite image; 
        public GameObject self;

        public bool isPurchased = false; 

        public void setIsPurchased()
        {
            switch(typeBonusEnum)
            {
                case typeBonus.casserole:
                    if(Helper.verifyAchat("achats.comp.casserole"))
                    {
                        isPurchased = true; 
                    }
                break;
                case typeBonus.louche: 
                    if(Helper.verifyAchat("achats.comp.louche"))
                    {
                        isPurchased = true; 
                    }
                break; 
                case typeBonus.rappe:
                    if(Helper.verifyAchat("achats.comp.rappe"))
                    {
                        isPurchased = true; 
                    }
                break; 
                case typeBonus.biere:
                    if(Helper.verifyAchat("achats.comp.biere"))
                    {
                        isPurchased = true;
                    }
                break;
            }
        }

        public GameObject getSelf()
        {
            return self; 
        }

    }

    [System.Serializable]
    public class Skin
    {

        public enum typeSkins 
        {
            planche,
            grille,
            bolo
        }

        public typeSkins typeSkinsEnum; 
        public string nom; 
        public string description; 

        public int prix; 

        public Sprite image;

        public bool isPurchased;

        public GameObject self; 

        public void setIsPurchased()
        {
            switch(typeSkinsEnum)
            {
                case typeSkins.planche:
                    if(Helper.verifyAchat("achats.skin.planche"))
                    {
                        isPurchased = true; 
                    }
                break;
                case typeSkins.grille: 
                    if(Helper.verifyAchat("achats.skin.grille"))
                    {
                        isPurchased = true; 
                    }
                break; 
                case typeSkins.bolo:
                    if(Helper.verifyAchat("achats.skin.bolo"))
                    {
                        isPurchased = true; 
                    }
                break; 
            }
        }
    }

    [SerializeField]
    private List<Skin> Skins; 

    [SerializeField]
    private TextMeshProUGUI argent_txt; 

    private void Awake() 
    {
        skinBtn.GetComponentInChildren<TextMeshProUGUI>().text = Translation.Get("achats.bouton.skins");
        compBtn.GetComponentInChildren<TextMeshProUGUI>().text = Translation.Get("achats.bouton.competences");
        if(PlayerPrefs.GetInt("argent") != 0)
        {
            argent_txt.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("argent").ToString(); 
        }

        //TODO check if gold is enough 

        Competence actualCompetence; 
        for (int i = 0; i < competences.Count; i++)
        {   actualCompetence = competences[i];
            actualCompetence.setIsPurchased();
        }

        for (int i = 0; i < 3; i++)
        {
            actualCompetence = competences[i];
            if(actualCompetence.isPurchased)
            {
                actualCompetence.getSelf().transform.GetComponentInChildren<Button>().interactable = false; 
            }
            actualCompetence.self.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = actualCompetence.nom;
            actualCompetence.self.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = actualCompetence.description;
            actualCompetence.self.transform.GetChild(3).GetComponent<Image>().sprite = actualCompetence.image;
            actualCompetence.self.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = actualCompetence.prix.ToString();

        }
    }

    public void skinButtonClick()
    {
        nextButton.interactable = false; 
        previousButton.interactable = false; 
        compBtn.interactable = true; 
        skinBtn.interactable = false; 


    }

    public void comptButtonClick()
    {
        nextButton.interactable = true; 
        previousButton.interactable = false; 
        compBtn.interactable = false; 
        skinBtn.interactable = true; 
    }

    public void nextButtonClick()
    {
        previousButton.interactable = true; 
        nextButton.interactable = false; 

        //iteration for competences; 
        int i = 3;
        GameObject current = competences[i].self; 
        //nom
        current.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = competences[i].nom; 
        //description
        current.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = competences[i].description; 
        //image
        current.transform.GetChild(3).GetComponent<Image>().sprite = competences[i].image; 
        //prix
        current.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = competences[i].prix.ToString();

        if(competences[i].isPurchased)
        {
            current.transform.GetComponentInChildren<Button>().interactable = false;
        }
        else
        {
            current.transform.GetComponentInChildren<Button>().interactable = true;
            
        } 

        competences[1].self.SetActive(false); 
        competences[2].self.SetActive(false); 

        //iteration for skins; 
    }

    public void previousButtonClick()
    {
        GameObject current; 

        for (int i = 0; i < 3; i++)
        {
            current = competences[i].self; 
            if(!current.activeSelf)
            {
                current.SetActive(true);
            }
            //nom
            current.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = competences[i].nom; 
            //description
            current.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = competences[i].description; 
            //image
            current.transform.GetChild(3).GetComponent<Image>().sprite = competences[i].image; 
            //prix
            current.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = competences[i].prix.ToString();

            Competence actualCompetence = competences[i];
            if(actualCompetence.isPurchased)
            {
                actualCompetence.getSelf().transform.GetComponentInChildren<Button>().interactable = false; 
            }
            else
            {
                current.transform.GetComponentInChildren<Button>().interactable = true; 
            }
            
        }

        if(!Helper.verifyAchat("achats.casserole") && Helper.getArgent() > competences[0].prix)
        {
            competences[0].self.transform.GetChild(5).GetComponent<Button>().interactable = true; 
        }

        previousButton.interactable = false; 
        nextButton.interactable = true; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}