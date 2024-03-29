﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EcranTitreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txt_argent;  

    [SerializeField]
    private GameObject ms_canvas, tuto_mouvement, tuto_boutons;

    [SerializeField]
    private List<TextMeshProUGUI> UI; 

    [SerializeField]
    private GameObject[] pages; 
    
    [SerializeField]
    private Button[] btnsTuto; 


    private void Start() 
    {
        if(txt_argent != null)
            updateMoney();

        //ajout locales
        UI[0].text = Translation.Get("titreJeu"); 
        UI[1].text = Translation.Get("bouton.jouer"); 
        UI[2].text = Translation.Get("bouton.tutoriel"); 
        UI[3].text = Translation.Get("bouton.achats"); 
        UI[4].text = Translation.Get("bouton.astuces"); 
        UI[5].text = Translation.Get("bouton.scores"); 
        UI[6].text = Translation.Get("bouton.options"); 
        UI[7].text = Translation.Get("bouton.credits"); 
        UI[8].text = Translation.Get("bouton.quitter"); 

        //tutoriel swipe
        UI[9].text = Translation.Get("tutoriel.commandes.titre");
        UI[10].text = Translation.Get("tutoriel.swipe.description");
        UI[11].text = Translation.Get("tutoriel.tap.description");
        UI[12].text = Translation.Get("tutoriel.item.titre");
        UI[13].text = Translation.Get("tutoriel.arecolter.titre");
        UI[14].text = Translation.Get("tutoriel.aeliminer.titre");
        UI[15].text = Translation.Get("bouton.modetutoriel");

        UI[16].text = Translation.Get("tutoriel.secondepage.commande.explications");
        UI[17].text = Translation.Get("tutoriel.secondepage.score.explications");

    }

    private void Awake() 
    {
        if(ms_canvas != null && ms_canvas.activeSelf == true)
            ms_canvas.SetActive(false);

        GameObject.Find("Helper").GetComponent<Helper>().checkTutorielPanel();

    }
    

    public void afficherMeilleurScore()
    {
        TextMeshProUGUI textMS = ms_canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        textMS.text = Helper.getMeilleurScore().ToString(); 

        int astuceduchef = getScoreAstuce();
        TextMeshProUGUI txt_astuce = ms_canvas.transform.GetChild(2).transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        txt_astuce.text = astuceduchef.ToString() + "/6";
        
    }

    private int getScoreAstuce()
    {
        int res = 0; 
        if(Helper.verifyAstuce("astuce.beurre"))
            res++; 

        if(Helper.verifyAstuce("astuce.frites"))
            res++;

        if(Helper.verifyAstuce("astuce.carbonara"))
            res++; 

        if(Helper.verifyAstuce("astuce.pizza"))
            res++; 
        
        if(Helper.verifyAstuce("astuce.sushis"))
            res++; 

        if(Helper.verifyAstuce("astuce.the_anglais"))
            res++; 

        return res; 
    }

    public void afficherTuto()
    {
        //afficher tuto bouton 
        if(tuto_boutons != null && Helper.getModeJeu() == OptionsManager.modejeu.boutons)
        {

        }
        else if(tuto_mouvement != null && Helper.getModeJeu() == OptionsManager.modejeu.mouvements)
        {

        }
    }

    public void updateMoney()
    {
        txt_argent.text = Helper.getArgent().ToString();
    }

     #region buttons_tutoriel

     public void click_suiv_bouton_tuto()
    {
        if(pages[0].activeSelf) // aller page 2
        {
            pages[0].SetActive(false);
            pages[1].SetActive(true);
            btnsTuto[1].interactable = true; 
        }
        else if(pages[1].activeSelf) // aller page 3
        {
            pages[1].SetActive(false);
            pages[2].SetActive(true);
            btnsTuto[0].interactable = false; 
            TextMeshProUGUI textPage = pages[2].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            if(textPage.text == "")
                textPage.text = Translation.Get("tutoriel.troisiemepage.explications");
        }
    }

    public void click_prec_bouton_tuto()
    {
        if(pages[1].activeSelf) //revenir page 1
        {
            pages[1].SetActive(false);
            pages[0].SetActive(true);
            btnsTuto[1].interactable = false; 
            btnsTuto[0].interactable = true; 
        }
        else if(pages[2].activeSelf) //revenir page 2
        {
            pages[2].SetActive(false);
            pages[1].SetActive(true);
            btnsTuto[0].interactable = true; 
            btnsTuto[1].interactable = true; 
        }
    }
    #endregion
}

