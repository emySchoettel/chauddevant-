using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using Lean.Touch; 
public class GameManager : MonoBehaviour
{
    //Prefab
    [SerializeField] private PlayerMouvement joueurprefab;
    [SerializeField] private PlayerMouvement joueurActuel; 
    private PlayerVies viesJoueur; 
    [SerializeField] private ItemController itemController;
    [SerializeField] private Helper GO_Helper; 
    [SerializeField] private GameObject GameOver, GameOverPanel, GameOverScoring, Scoring;

   

    private bool endGame; 

   private void Awake() 
    { 
        if(Helper.isFade)
        {
            GO_Helper.Fading(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnJoueur());
        StartCoroutine(SpawnNourriture());
        StartCoroutine(SpawnCommande());
        Commande.preparerCommande();
        Inventaire.createInventaire();
    }

    private IEnumerator SpawnJoueur() 
    {
        if(Helper.pointActuel != 0)
        {
            Helper.pointActuel = 0; 
        }
        GameObject positionGauche = GameObject.Find("GaucheJoueur");
        joueurActuel = Instantiate(joueurprefab, positionGauche.transform.position, Quaternion.identity);
        viesJoueur = joueurActuel.GetComponent<PlayerVies>();
        yield return null; 
    }

    private IEnumerator SpawnNourriture()
    {
        GameObject[] tab_items_positions = new GameObject[3];
        int length = GameObject.Find("EmplacementsNourriture").transform.childCount;
        for (int i = 0; i < length; i++)
        {
            tab_items_positions[i] = GameObject.Find("EmplacementsNourriture").transform.GetChild(i).gameObject;
        }
        ItemController itemControl = Instantiate(itemController, new Vector3(), Quaternion.identity);
        itemControl.setTabPositions(tab_items_positions);
        GameObject[] tab_canvas = AddEmplacementsCommandeEtInventaire(true); 
        itemControl.setTabInventairePositions(tab_canvas);
        tab_canvas = AddEmplacementsCommandeEtInventaire(false); 
        itemControl.setTabCommandePositions(tab_canvas);

        yield return null; 
    }

    private IEnumerator SpawnCommande()
    {
        GameObject Commande = Instantiate(new GameObject("Commande"));
        Commande.AddComponent<Commande>();
        yield return null; 
    }

    private GameObject[] AddEmplacementsCommandeEtInventaire(bool decision)
    {
        GameObject[] tab_commande_positions = new GameObject[3];
        int length;
        if(decision)
        {
            length = GameObject.Find("EmplacementsCommande").transform.childCount;
            for (int i = 0; i < length; i++)
            {
                tab_commande_positions[i] = GameObject.Find("EmplacementsCommande").transform.GetChild(i).gameObject;
            }
        }
        else
        {
            length = GameObject.Find("EmplacementsInventaire").transform.childCount;
            for (int i = 0; i < length; i++)
            {
                tab_commande_positions[i] = GameObject.Find("EmplacementsInventaire").transform.GetChild(i).gameObject;
            }
        }
        return tab_commande_positions;
    }

    public void dechetToPlayer()
    {
        if(viesJoueur.getVies() > 0)
        {
            StartCoroutine(joueurActuel.Vulnerability());
            viesJoueur.setVies(viesJoueur.getVies() -1);
        }
        if(viesJoueur.getVies() == 0)
        {
            Sc_Fin.setTitre("Perdu !");
            endOfGame(); 
        }
    }

    public void endOfGame()
    {
        //stop everything 
        Commande.commandeActuelle = 5;
        //joueurActuel.setMove(false); 
        itemController.setEndGame(true);

        Sc_Fin.setTitre("Vous avez perdu");
        
        if(GameOverPanel != null)
            GO_Helper.Fading(true, GameOverPanel);

        Helper.Wait(3f);

        if(GameOverScoring != null)
        {
            for (int i = 0; i < GameOverScoring.transform.childCount; i++)
            {
                GameOverScoring.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        GO_Helper.GoToLevelFin();
    }

    public void verifyInventaire(NourritureItem nourritureItem)
    {
        
        if(Commande.commandeActuelle < 4)
        {   
            Debug.Log(Commande.commande[Commande.commandeActuelle].name);
            if(Commande.commande[Commande.commandeActuelle].GetComponent<NourritureItem>().type == nourritureItem.type)
            {
                Inventaire.addItemInventaire(gameObject.GetComponent<NourritureItem>());
                Helper.addPoints(20, false);
                Commande.commandeActuelle++; 
            }
            else
            {
                dechetToPlayer();
            }
        }
    }
    
    #region get et set
    public GameObject getPlayer()
    {
        return joueurActuel.gameObject;
    }

    public ItemController GetItemController()
    {
        return itemController;
    }

    #endregion
}
