using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        StartCoroutine(WaitForLevel(2, true));
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
        // if(ItemController.rand_previous_item == 0)
        // {
        //     ItemController.rand_previous_item = rand_position_int; 
        // }
        // else
        // {
        //     while(ItemController.rand_previous_item == rand_position_int)
        //     {
        //         rand_position_int = UnityEngine.Random.Range(0,3);
        //         Debug.Log(rand_position_int);
        //     }
        // }
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
            txt.GetComponent<Text>().text = pointActuel.ToString();
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
        if(pointActuel > meilleurScore)
        {
            meilleurScore = pointActuel; 
        }
    }
}
