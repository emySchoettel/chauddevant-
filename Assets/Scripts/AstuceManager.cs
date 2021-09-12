using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AstuceManager : MonoBehaviour
{

    [SerializeField]
    private List<Astuce> lesastucesDuChef; 

    [SerializeField]
    private List<GameObject> astuceGO; 

    [SerializeField]
    private List<GameObject> astuceGOHide;

    private Astuce astuceActuelle; 

    public GameObject descriptionGO; 

    public void onClick()
    {
        astuceActuelle = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Astuce>();

        if(astuceActuelle.clicked())
        {
            descriptionGO.SetActive(true);
            descriptionGO.GetComponentInChildren<TextMeshProUGUI>().text = astuceActuelle.getDescription();
        }
        else
        {
            descriptionGO.SetActive(false);
        }
    }

    private void Start() 
    {
        if(astuceGO.Count != 0 && astuceGOHide.Count != 0)
        {
            GameObject actualGO;

            //if astuce is true
            if(Helper.verifyAstuce("astuce.beurre"))
            {   
                //create astuce
                actualGO = astuceGO[0];
                Astuce astuce = actualGO.AddComponent(typeof(Astuce)) as Astuce;
                astuce.setParams(actualGO.GetComponentInChildren<TextMeshProUGUI>().text, actualGO.GetComponentInChildren<Image>(), Translation.Get("astuce.beurre.description"));

                //display astuce
                astuceGO[0].SetActive(true);
                astuceGOHide[0].SetActive(false);

            }
            else //if not then hide
            {
                astuceGO[0].SetActive(false);
                astuceGOHide[0].SetActive(true);
            }
            
            if(Helper.verifyAstuce("astuce.frites"))
            {
                //create astuce
                actualGO = astuceGO[1];
                Astuce astuce = actualGO.AddComponent(typeof(Astuce)) as Astuce;
                astuce.setParams(actualGO.GetComponentInChildren<TextMeshProUGUI>().text, actualGO.GetComponentInChildren<Image>(), Translation.Get("astuce.frites.description"));

                //display astuce
                astuceGO[1].SetActive(true);
                astuceGOHide[1].SetActive(false);

            }
            else //if not then hide
            {
                astuceGO[1].SetActive(false);
                astuceGOHide[1].SetActive(true);
            }


            if(Helper.verifyAstuce("astuce.carbonara"))
            {
                //create astuce
                actualGO = astuceGO[2];
                Astuce astuce = actualGO.AddComponent(typeof(Astuce)) as Astuce;
                astuce.setParams(actualGO.GetComponentInChildren<TextMeshProUGUI>().text, actualGO.GetComponentInChildren<Image>(), Translation.Get("astuce.carbonara.description"));

                //display astuce
                astuceGO[2].SetActive(true);
                astuceGOHide[2].SetActive(false);
            }
            else //if not then hide
            {
                astuceGO[2].SetActive(false);
                astuceGOHide[2].SetActive(true);
            }

            if(Helper.verifyAstuce("astuce.pizza"))
            {
                 //create astuce
                actualGO = astuceGO[3];
                Astuce astuce = actualGO.AddComponent(typeof(Astuce)) as Astuce;
                astuce.setParams(actualGO.GetComponentInChildren<TextMeshProUGUI>().text, actualGO.GetComponentInChildren<Image>(), Translation.Get("astuce.pizza.description"));

                //display astuce
                astuceGO[3].SetActive(true);
                astuceGOHide[3].SetActive(false);

               
            }
            else //if not then hide
            {
                astuceGO[3].SetActive(false);
                astuceGOHide[3].SetActive(true);
            }


            if(Helper.verifyAstuce("astuce.sushis"))
            {
                 //create astuce
                actualGO = astuceGO[4];
                Astuce astuce = actualGO.AddComponent(typeof(Astuce)) as Astuce;
                astuce.setParams(actualGO.GetComponentInChildren<TextMeshProUGUI>().text, actualGO.GetComponentInChildren<Image>(), Translation.Get("astuce.sushis.description"));

                //display astuce
               astuceGO[4].SetActive(true);
               astuceGOHide[4].SetActive(false);
            }
            else //if not then hide
            {   
                astuceGO[4].SetActive(false);
                astuceGOHide[4].SetActive(true);
            }
            
            if(Helper.verifyAstuce("astuce.the_anglais"))
            {
                //create astuce
                actualGO = astuceGO[5];
                Astuce astuce = actualGO.AddComponent(typeof(Astuce)) as Astuce;
                astuce.setParams(actualGO.GetComponentInChildren<TextMeshProUGUI>().text, actualGO.GetComponentInChildren<Image>(), Translation.Get("astuce.the_anglais.description"));

                //display astuce
                astuceGO[5].SetActive(true);
                astuceGOHide[5].SetActive(false);               
            }
            else //if not then hide
            {
                astuceGO[5].SetActive(false);
                astuceGOHide[5].SetActive(true);
            }
        }
       
    }
}

