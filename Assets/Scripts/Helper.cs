using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Helper : MonoBehaviour
{
    [SerializeField] private GameObject panel, tutoriel = null;
    private static int rand_position_int;
    public static bool isFade; 

    public GameObject[] tab_nourriture;

    public enum directions{
        gauche,
        milieu, 
        droite
    }

    public enum nourriture{
        bacon,
        pain,
        oeuf
    }

    public enum item{
        nourriture, 
        dechet, 
        rien
    }

    public static int pointActuel = 0, meilleurScore = 0;
    private static int itemNourriture, itemDechet, commande;

    private static int argent; 

    public static int getArgent()
    {

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

    #region Debut du jeu 
    public void GoToLevel()
    {
        StartCoroutine(Fade(true));
        StartCoroutine(WaitForLevel(1, true));
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

    public void Fading(bool fade, GameObject unpanel = null)
    {
        StartCoroutine(Fade(fade, unpanel));
    }

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

    public static void createProjectile(GameObject joueur)
    {
        Projectile proj = Instantiate(joueur.GetComponent<PlayerMouvement>().prefabProjectile, joueur.transform.GetChild(0).position, Quaternion.identity);
    }

    public static void addPoints(int points, bool choix, item objet = item.rien)
    {
        pointActuel += points; 

        if(choix)
            addItem(objet);

        updateScore();
    }

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
    public static void updateScore()
    {
        GameObject txt = GameObject.Find("score_text");
        if(txt != null)
        {
            txt.GetComponent<TextMeshProUGUI>().text = pointActuel.ToString();
        }
    }
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
        int argentactuel = Mathf.RoundToInt(pointActuel / 10);
        if(PlayerPrefs.GetInt("argent") != 0)
        {
            argent = PlayerPrefs.GetInt("argent") + argentactuel; 
        }
        else
        {
            argent = argentactuel;
        }
        
        PlayerPrefs.SetInt("argent", argent); 
        
        PlayerPrefs.Save(); 
    }

    public void acheter()
    {
        
        List<Achat.Competence> competences = GameObject.FindObjectOfType<Achat>().getCompetences();
        GameObject competenceGO = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;;

        //Verifier dans les playerPrefs que le produit n est pas deja achete
        if(!PlayerPrefs.HasKey("casserole"))
        {
            if(argent >= competences[0].prix && competenceGO == competences[0].self)
            {
                addCompetenceToplayer(1); 
                PlayerPrefs.SetString("casserole", "true"); 
                PlayerPrefs.Save(); 
                competences[0].self.transform.GetChild(5).GetComponent<Button>().interactable = false;
                setArgent(competences[0].prix); 
                
            }
            else
            {
                throw new System.Exception("Pas assez d'argent");
            }
        }

        else if(!PlayerPrefs.HasKey("louche"))
        {
            if(argent >= competences[1].prix && competenceGO == competences[1].self)
            {
                addCompetenceToplayer(2); 
                PlayerPrefs.SetString("louche", "true"); 
                PlayerPrefs.Save(); 
                competences[1].self.transform.GetChild(5).GetComponent<Button>().interactable = false;
                setArgent(competences[1].prix);
            }
            else
            {
                throw new System.Exception("Pas assez d'argent");
            }
        }
        else if(!PlayerPrefs.HasKey("rappe"))
        {
            if(argent >= competences[2].prix && competenceGO == competences[2].self)
            {             
                addCompetenceToplayer(3); 
                PlayerPrefs.SetString("rappe", "true"); 
                PlayerPrefs.Save(); 
                competences[2].self.transform.GetChild(5).GetComponent<Button>().interactable = false;
                setArgent(competences[2].prix);
            }
            else
            {
                throw new System.Exception("Pas assez d'argent");
            }
        }
        else if(!PlayerPrefs.HasKey("biere"))
        {
            if(argent >= competences[3].prix && competenceGO == competences[3].self)
            {
                addCompetenceToplayer(4); 
                PlayerPrefs.SetString("biere", "true"); 
                PlayerPrefs.Save(); 
                competences[3].self.transform.GetChild(5).GetComponent<Button>().interactable = false;
                setArgent(competences[3].prix);
            }
            else
            {
                throw new System.Exception("Pas assez d'argent");
            }
        }
        GameObject.FindObjectOfType<Achat>().argent_txt.GetComponent<TextMeshProUGUI>().text = argent.ToString();
    }

    public static void addCompetenceToplayer(int numCompetence)
    {
        //TODO 
        switch(numCompetence)
        {
            //casserole 
            case 1: 
            break; 

            //louche 
            case 2:
            break; 

            //rappe 
            case 3: 
            break; 

            //biere 
            case 4:
            break; 
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



    private void Awake() 
    {
        argent = PlayerPrefs.GetInt("argent") != 0 ? PlayerPrefs.GetInt("argent") : 0;
       pointActuel = 4000; 
       updateScoreFinal(); 
    }
}