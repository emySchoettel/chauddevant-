using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdminManager : MonoBehaviour
{

    [SerializeField]
    private GameObject gameObjectAdmin; 
    [SerializeField]
    private GameObject doneGameObject; 
    //Argent 
    [Header("Argent")]

    [SerializeField]
    private TMP_InputField inputField; 

    [SerializeField]
    private Button submitArgent; 

    //Apparences
    [Header("Apparences")]

    [SerializeField]
    private GameObject GOApparence; 
    [SerializeField]
    private Slider sliderApparence; 
    [SerializeField]
    private Button submitApparence;

    [SerializeField]
    private TextMeshProUGUI valueApparence; 

    private string[] apparencesNames = { "achats.skin.planche", "achats.skin.grille", "achats.skin.bolo"};

    //Competences
    [Header("Competences")]
    
    [SerializeField]
    private GameObject GOCompetences; 
    [SerializeField]
    private Slider sliderCompetences; 
    [SerializeField]
    private Button submitCompetences;
    [SerializeField]
    private TextMeshProUGUI valueCompetences; 

    private string[] competencesNames = { "achats.comp.casserole", "achats.comp.louche", "achats.comp.rappe","achats.comp.biere"};

    //Astuces
    [Header("Astuces")]
    [SerializeField]
     private GameObject GOAstuces; 
    [SerializeField]
    private Slider sliderAstuces; 
    [SerializeField]
    private Button submitAstuces;
    [SerializeField]
    private TextMeshProUGUI valueAstuces; 

    private string[] astucesNames = { "astuce.beurre", "astuce.frites", "astuce.carbonara","astuce.pizza", "astuce.sushis", "astuce.the_anglais"};


    #region button

    public void close()
    {
        gameObjectAdmin.SetActive(false); 
    }

    public void submitArgentClick()
    {
        if(inputField.text == "")
        {
            inputField.text = "0";
        }
        int argent = int.Parse(inputField.text); 
        PlayerPrefs.SetInt("argent", argent);
        launchWait();
    }

    public void submitApparenceClick()
    {
        int value = (int)sliderApparence.value;

        submitChanges(value, apparencesNames);
        launchWait();
    }

    public void submitCompencesClick()
    {
        int value = (int)sliderCompetences.value; 

        submitChanges(value, competencesNames);
        launchWait();
    }

    public void submitAstucesClick()
    {
        int value = (int)sliderAstuces.value;

        submitChanges(value, astucesNames);
        launchWait();
    }

    public void admin_open()
    {
        gameObjectAdmin.SetActive(true);
    }

    #endregion

    #region slider

    public void changeValueApparence()
    {
        valueApparence.text = sliderApparence.value.ToString();
    }

    public void changeValueCompetences()
    {
        valueCompetences.text = sliderCompetences.value.ToString();
    }

    public void changeValueAstuces()
    {
        valueAstuces.text = sliderAstuces.value.ToString();
    }

    #endregion

    
    public void submitChanges(int valueChanges, string[] tableOfNames)
    {

        Debug.Log(tableOfNames.Length);
        
        if(valueChanges == 0) //si aucune competence
        {
            foreach(string st in tableOfNames)
            {
                PlayerPrefs.SetInt(st, 0);
            }
        }
        else if(valueChanges == 1) //si une competence 
        {
            PlayerPrefs.SetInt(tableOfNames[0], 1);
            PlayerPrefs.SetInt(tableOfNames[1], 0);
            PlayerPrefs.SetInt(tableOfNames[2], 0);

            //table of competences
            if(tableOfNames.Length == 4)
                PlayerPrefs.SetInt(tableOfNames[3], 0);
            
            //table of astuces
            if(tableOfNames.Length == 6)
            {
                PlayerPrefs.SetInt(tableOfNames[3], 0);
                PlayerPrefs.SetInt(tableOfNames[4], 0);
                PlayerPrefs.SetInt(tableOfNames[5], 0);
            }
        }
        else if(valueChanges == 2)
        {
            PlayerPrefs.SetInt(tableOfNames[0], 1);
            PlayerPrefs.SetInt(tableOfNames[1], 1);
            PlayerPrefs.SetInt(tableOfNames[2], 0);

            //table of competences
            if(tableOfNames.Length == 4)
                PlayerPrefs.SetInt(tableOfNames[3], 0);

            //table of astuces
            if(tableOfNames.Length == 6)
            {
                PlayerPrefs.SetInt(tableOfNames[3], 0);
                PlayerPrefs.SetInt(tableOfNames[4], 0);
                PlayerPrefs.SetInt(tableOfNames[5], 0);
            }
                
        }
        else if(valueChanges == 3)
        {
            if(tableOfNames.Length == 3) //Full table of apparences
            {
                foreach(string st in tableOfNames)
                    PlayerPrefs.SetInt(st, 1);
            }
            else
            {    
                PlayerPrefs.SetInt(tableOfNames[0], 1);
                PlayerPrefs.SetInt(tableOfNames[1], 1);
                PlayerPrefs.SetInt(tableOfNames[2], 1);
            }

            //table of competences
            if(tableOfNames.Length == 4)
                PlayerPrefs.SetInt(tableOfNames[3], 0);
        }
        else if(valueChanges == 4)
        {
            if(tableOfNames.Length == 4) //Full table of competences
            {
                foreach(string st in tableOfNames)
                    PlayerPrefs.SetInt(st, 1);
            }
            else
            {    
                PlayerPrefs.SetInt(tableOfNames[0], 1);
                PlayerPrefs.SetInt(tableOfNames[1], 1);
                PlayerPrefs.SetInt(tableOfNames[2], 1);
                PlayerPrefs.SetInt(tableOfNames[3], 1);
                PlayerPrefs.SetInt(tableOfNames[4], 0);
                PlayerPrefs.SetInt(tableOfNames[5], 0);
            }
        }
        else if(valueChanges == 5)
        {
            PlayerPrefs.SetInt(tableOfNames[0], 1);
            PlayerPrefs.SetInt(tableOfNames[1], 1);
            PlayerPrefs.SetInt(tableOfNames[2], 1);
            PlayerPrefs.SetInt(tableOfNames[3], 1); 
            PlayerPrefs.SetInt(tableOfNames[4], 1);
            PlayerPrefs.SetInt(tableOfNames[5], 0);
        }
        else if(valueChanges == 6) //full table of astuces
        {
            foreach(string st in tableOfNames)
            {
                PlayerPrefs.SetInt(st, 1);
            }
        }

    }
    public void launchWait()
    {
        doneGameObject.SetActive(true);
        StartCoroutine(wait());
        
    }
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2.0f);
        doneGameObject.SetActive(false);
    }
}
