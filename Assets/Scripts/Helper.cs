﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Helper : MonoBehaviour
{
    #region parameters
    [SerializeField] private GameObject panel, tutoriel = null, ms_canvas = null;
    [SerializeField] private GameObject tutopage1, tutopage2;  
    [SerializeField] private Button modetutobtn, btnprectuto, btnsuivtuto; 
    private static int rand_position_int;
    public static bool isFade; 

    public GameObject[] tab_nourriture;

    public enum directions
    {
        gauche,
        milieu, 
        droite
    }

    public enum nourriture
    {
        bacon,
        pain,
        oeuf,
        baguette,
        bun, 
        burger,
        burrito,
        cheesecake,
        confiture,
        glace,
        hotdog,
        nachos, 
        omlet, 
        pie,
        pizza,
        ramen,
        sandwich,
        sushi,
        taco
    }

    public enum item
    {
        nourriture, 
        dechet, 
        rien
    }

    public static int pointActuel = 0, meilleurScore = 0;
    private static int itemNourriture, itemDechet, commande;

    private static int argent = 0; 

    public static bool tuto = false; 

    #endregion

    private void Awake() 
    {
        argent = PlayerPrefs.GetInt("argent") != 0 ? PlayerPrefs.GetInt("argent") : 0;
    }

    #region argent
    public static int getArgent()
    {
        if(PlayerPrefs.HasKey("argent"))
        {
            argent = PlayerPrefs.GetInt("argent");
        }
        return argent;
    }
    public static void setArgent(int newcoins)
    {
        if(argent >= 0)
        {
            if(argent - newcoins < 0)
            {
                
                argent = 0;
            }
            else
            {
                argent -= newcoins;
            }
            PlayerPrefs.SetInt("argent", argent);
            PlayerPrefs.Save(); 
        }
    }

    public static void updateArgent(int score)
    {
        int argentactuel = Mathf.RoundToInt(score / 10);
        if(PlayerPrefs.GetInt("argent") != 0)
        {
            argent = getArgent() + argentactuel; 
        }
        else
        {
            argent = argentactuel;
        }
        
        PlayerPrefs.SetInt("argent", argent); 
        PlayerPrefs.Save(); 
    }

    #endregion

    #region Debut du jeu 
    public void GoToLevel()
    {
        StartCoroutine(Fade(true));
        StartCoroutine(WaitForLevel(1, true));
    }

    public void GoToLevel(int level)
    {
        StartCoroutine(Fade(true));
        StartCoroutine(WaitForLevel(level, true));
    }
    public IEnumerator WaitForLevel(int nombrelevel, bool raison)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nombrelevel);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator Fade(bool fade, GameObject newpanel = null) 
    {
        GameObject panelGM; 
        if(newpanel != null)
        {
            panelGM = newpanel;
        }
        else
        {
            panelGM = panel; 
        }
        panelGM.SetActive(true);
      
        Image img = panelGM.GetComponent<Image>();
         if (!fade)
        {
            isFade = false; 
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            isFade = true;
            // loop over 1 second
            for (float i = 0; i <= 255; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }

    #endregion

    
    public void GoToLevelFin()
    {

        StartCoroutine(Fade(true));
        StartCoroutine(WaitForLevel(3, true));
    }
    public static IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    #region fade
    public void Fading(bool fade, GameObject unpanel = null)
    {
        StartCoroutine(Fade(fade, unpanel));
    }

    #endregion


    #region astuceduchef

    public static bool verifyAstuce(string nomAstuce)
    {
        if (PlayerPrefs.HasKey(nomAstuce))
        {
            if(PlayerPrefs.GetInt(nomAstuce) == 1)
            {
                return true; 
            }
            else
            {
                return false; 
            }
    
        }
        else
        {
            PlayerPrefs.SetInt(nomAstuce, 0);
        }
        return false; 
    }

    #endregion

    #region directions items
    public static Vector3 randomPosition()
    {
        Vector3 res = new Vector3();
        rand_position_int = UnityEngine.Random.Range(0,3);

        GameObject[] tab_positions = GameObject.FindObjectOfType<ItemController>().getTabPositions();
        switch(rand_position_int)
        {
            case 0:
                res = tab_positions[0].transform.position;
            break; 
            case 2:
                res = tab_positions[2].transform.position;
            break; 
            default: 
                res = tab_positions[1].transform.position;
            break; 
        }
        return res;
    }

    
    public static Helper.directions setItemPosition()
    {
        Helper.directions res = Helper.directions.droite; 
        switch(rand_position_int)
        {
            case 0:
                res = Helper.directions.gauche; 
            break; 

            case 1:
                res = Helper.directions.milieu; 
            break; 
        }
        return res; 
    }
    #endregion

    #region projectile

    public static void createProjectile(GameObject joueur)
    {
        Projectile proj = Instantiate(joueur.GetComponent<PlayerMouvement>().prefabProjectile, joueur.transform.GetChild(0).position, Quaternion.identity);
    }

    #endregion

    #region scoring
    public static void addPoints(int points, bool choix, item objet = item.rien)
    {
        pointActuel += points; 

        if(choix)
            addItem(objet);

        updateScore();
    }
    public static void updateScore()
    {
        GameObject txt = GameObject.Find("score_text");
        if(txt != null)
        {
            txt.GetComponent<TextMeshProUGUI>().text = pointActuel.ToString();
        }
    }

    public static void updateScoreFinal()
    {
        meilleurScore = PlayerPrefs.GetInt("meilleurScore"); 
        if(pointActuel > meilleurScore)
        {
            meilleurScore = pointActuel; 
            PlayerPrefs.SetInt("meilleurScore", meilleurScore); 
        }     
     
        PlayerPrefs.Save(); 
    }

    public static int getMeilleurScore()
    {
        if(PlayerPrefs.HasKey("meilleurScore"))
            return PlayerPrefs.GetInt("meilleurScore");
        else
            return 0;
    }

    #endregion

    #region items
    public static void addItem(item objet)
    {
        switch(objet)
        {
            case item.dechet:
                itemNourriture++; 
            break; 

            case item.nourriture: 
                itemDechet++;
            break; 
        }
    }
    #endregion

    #region boutons
    public void click_retour()
    {
        //Time.timeScale = 0f; 
        Commande.commandeActuelle = 0;
        SceneManager.LoadScene(0);
    }

    public void click_retour_tutoriel()
    {
        if(tutoriel != null)
        {
            tutoriel.SetActive(false);    
        }
    }

    public void click_tutoriel()
    {
        if(tutoriel != null)
        {
            tutoriel.SetActive(true);
            GameObject.FindObjectOfType<EcranTitreManager>().afficherTuto();
        }
    }

    public void click_retour_menu_principal()
    {
        if(panel != null)
        {
            StartCoroutine(Fade(true));
            StartCoroutine(WaitForLevel(0, true));
        }
        else
            SceneManager.LoadScene(0);
    }

    public void click_credits()
    {
        SceneManager.LoadScene(2);
    }

    public void click_achats()
    {
         if(panel != null)
        {
             StartCoroutine(Fade(true));
            StartCoroutine(WaitForLevel(4, true));
        }
        else
            SceneManager.LoadScene(4);
    }

    public void click_options()
    {
        if(panel != null)
        {
            StartCoroutine(Fade(true));
            StartCoroutine(WaitForLevel(5, true));
        }
        else
            SceneManager.LoadScene(5);
    }

     public void click_astuces()
    {
        if(panel != null)
        {
            StartCoroutine(Fade(true));
            StartCoroutine(WaitForLevel(6, true));
        }
        else
            SceneManager.LoadScene(6);
    }

    public void click_meilleur_score()
    {
        if(ms_canvas != null)
            ms_canvas.SetActive(true);
        
        GameObject.FindObjectOfType<EcranTitreManager>().afficherMeilleurScore(); 
    }

    public void click_fermer_ms()
    {
        if(ms_canvas != null)
            ms_canvas.SetActive(false);
    }

    public void click_partie_tuto()
    {
        GameManager.setTutoriel(true);
        GoToLevel(7);
    }

    #endregion


    #region achats

    public static bool verifyAchat(string nomAchat)
    {
        if(PlayerPrefs.HasKey(nomAchat))
        {
            if(PlayerPrefs.GetInt(nomAchat) == 1)
                return true; 
            else
                return false; 
        }
        else
        {
            PlayerPrefs.SetInt(nomAchat, 0);
        }
        return false; 
    }

    public static bool verifyGoldForPurchase(int prix)
    {
        if(getArgent() >= prix)
        {
            return true; 
        }
        else
        {
            return false;    
        }
    }

    #endregion

    #region animation
    public static bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    #endregion  

    #region get

    public static OptionsManager.modejeu getModeJeu()
    {
        if(PlayerPrefs.HasKey("option_mode_jeu"))
        {
            string modejeu = PlayerPrefs.GetString("option_mode_jeu");
            if(modejeu == OptionsManager.modejeu.mouvements.ToString())
            {
                return OptionsManager.modejeu.mouvements;
            }
            else
            {
                return OptionsManager.modejeu.boutons;
            }
        }
        else
        {
            return OptionsManager.modejeu.mouvements;
        }
    }

    #endregion

    #region otherStuff

    public void checkTutorielPanel()
    {
        if(tutoriel.activeSelf == true)
        {
            tutoriel.SetActive(false);
        }
    }

    #endregion

    #region options

    //verify if avatar is setup, if not just create one ; return 0 or 1 for tof
    public static int getNumberAvatar()
    {
        if(PlayerPrefs.HasKey("avatar.skin"))
        {
            return PlayerPrefs.GetInt("avatar.skin");
        }
        else
        {
            PlayerPrefs.SetInt("avatar.skin", 1);
            return 1;
        }
    }

    public static int verifyAvatar(string nomAvatar)
    {
        if(PlayerPrefs.HasKey(nomAvatar))
        {
            return PlayerPrefs.GetInt(nomAvatar);
        }
        else
        {
            return -1;
        }
    }

    public static int verifyVolumeMusic()
    {
        if(PlayerPrefs.HasKey("options.musique.volume"))
        {
            return PlayerPrefs.GetInt("options.musique.volume");
        }
        else
        {
            PlayerPrefs.SetInt("options.musique.volume", 20);
            return -1;
        }
    }

    public static string verifyModeJeu()
    {
        if(PlayerPrefs.HasKey("option_mode_jeu"))
        {
            return PlayerPrefs.GetString("option_mode_jeu");
        }
        else
        {
            PlayerPrefs.SetString("option_mode_jeu", OptionsManager.modejeu.mouvements.ToString());
            return "null";
        }
    }
    #endregion

}