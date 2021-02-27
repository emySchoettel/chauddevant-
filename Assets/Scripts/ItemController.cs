using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class ItemController : MonoBehaviour
{
    [SerializeField] private DechetItem[] tab_dechet; 
    [SerializeField] private GameObject[] tab_positions = new GameObject[3];
    [SerializeField] private GameObject[] tab_commande;

    [SerializeField] private GameObject[] tab_nourriture;
    [SerializeField] private GameObject[] tab_inventaire;
    private int rand_number_item, rand_position_int, rand_item ;
    public static int rand_previous_item = 0;
    private Vector3 rand_position;

    [SerializeField] public static PlayerMouvement player; 
    private bool endgame = false; 

    private void OnEnable() 
    {
        player = GameObject.FindObjectOfType<PlayerMouvement>(); 
        //helper = GameObject.Find("Helper").GetComponent<Helper>();
    }

    void Start()
    {
        Invoke("StartNourriture", 0.5f);
    }

    void StartNourriture()
    {
        if(!endgame)
        {
            int randomTime = UnityEngine.Random.Range(0,2);

            rand_number_item = UnityEngine.Random.Range(0, 2);
            Vector3 nextPosition = new Vector3(); 
            if((rand_number_item % 2) == 0)
            {
                nextPosition = Helper.randomPosition();
                rand_item = UnityEngine.Random.Range(0, (tab_nourriture.Length));

                if(rand_item < tab_nourriture.Length)
                {
                    //TODO randomize les instances
                    NourritureItem nourriture = Instantiate(tab_nourriture[rand_item].GetComponent<NourritureItem>(), nextPosition, Quaternion.identity);
                    //NourritureItem nourriture = Instantiate(tab_nourriture[rand_item].GetComponent<NourritureItem>(), nextPosition, Quaternion.identity);
                    nourriture.directionActuelle = Helper.setItemPosition();
                }
            }
            else
            {
                nextPosition = Helper.randomPosition();
                rand_item = UnityEngine.Random.Range(0, (tab_nourriture.Length));

                if(rand_item < tab_dechet.Length)
                {
                    Instantiate(tab_dechet[0], nextPosition, Quaternion.identity);
                    //Instantiate(tab_dechet[rand_item], nextPosition, Quaternion.identity);
                }
            }
            Invoke("StartNourriture", randomTime);
        }
        else
        {
            CancelInvoke();
        }
    }

    private void Update() 
    {
        if(endgame)    
        {
            CancelInvoke();
        }
    }

    #region getter et setter
    public void setEndGame(bool choix)
    {
        endgame = choix;
        DechetItem[] itemsDechets = GameObject.FindObjectsOfType<DechetItem>(); 
        foreach(DechetItem itemDechet in itemsDechets)
        {
            Destroy(itemDechet.gameObject);
        }
        NourritureItem[] itemsNourriture = GameObject.FindObjectsOfType<NourritureItem>();
        foreach(NourritureItem item in itemsNourriture)
        {
            Destroy(item.gameObject);
        }
        CancelInvoke();
    }

    public void setTabPositions(GameObject[] tab)
    {
        tab_positions = tab;
    }

    public void setTabInventairePositions(GameObject[] tab)
    {
        tab_commande = tab;
    }

    public void setTabCommandePositions(GameObject[] tab)
    {
        tab_inventaire = tab;
    }

    public GameObject[] getTabPositionsCommande()
    {
        return tab_commande;
    }

    public GameObject[] getTabNourritures()
    {
        return tab_nourriture;
    }
    public GameObject[] getTabPositionsInventaire()
    {
        return tab_inventaire;
    }

    public GameObject[] getTabPositions()
    {
        return tab_positions;
    }

    public PlayerMouvement GetPlayer()
    {
        return player; 
    }

    #endregion
}
