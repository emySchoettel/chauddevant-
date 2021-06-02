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
    private Vector3 previous_position_nourriture, previous_position_dechet; 

    [SerializeField] public static PlayerMouvement player; 
    private bool endgame = false; 

    private void OnEnable() 
    {
        player = GameObject.FindObjectOfType<PlayerMouvement>(); 
    }

    void Start()
    {
        if(!GameManager.GetBoolTutoriel())
            Invoke("StartNourriture", 0.5f);
    }

    void StartNourriture()
    {
        if(!endgame)
        {
            int randomTime = UnityEngine.Random.Range(0,2);

            rand_number_item = UnityEngine.Random.Range(0, 2);

            Vector3 nextPosition_nourriture = Helper.randomPosition();
            Vector3 nextPosition_dechet = Helper.randomPosition(); 

            if(previous_position_nourriture == null)
            {
                previous_position_nourriture = nextPosition_nourriture;
            }

            if(previous_position_dechet == null)
            {
                previous_position_dechet = nextPosition_dechet; 
            }


            while(nextPosition_dechet == nextPosition_nourriture)
            {
                nextPosition_dechet = Helper.randomPosition();
                nextPosition_nourriture = Helper.randomPosition();
            }
            
            if(rand_number_item == 1)
            {
                
                while(nextPosition_nourriture == previous_position_nourriture)
                {
                    nextPosition_nourriture = Helper.randomPosition(); 
                }
                rand_item = UnityEngine.Random.Range(0, (tab_nourriture.Length));

                if(rand_item < tab_nourriture.Length)
                {
                    NourritureItem nourriture = Instantiate(tab_nourriture[rand_item].GetComponent<NourritureItem>(), nextPosition_nourriture, Quaternion.identity);
                    nourriture.directionActuelle = Helper.setItemPosition();
                }
            }
            else
            {
                while(nextPosition_dechet == previous_position_dechet)
                {
                    nextPosition_dechet = Helper.randomPosition();
                }
                rand_item = UnityEngine.Random.Range(0, (tab_nourriture.Length));

                if(rand_item < tab_dechet.Length)
                {
                    Instantiate(tab_dechet[rand_item], nextPosition_dechet, Quaternion.identity);
                }
            }
            previous_position_dechet = nextPosition_dechet; 
            previous_position_nourriture = nextPosition_nourriture; 

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
        CancelInvoke();
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
        
    }

    public bool getEndGame()
    {
        return endgame;
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
