using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Achat : MonoBehaviour
{
    public GameObject argent_txt; 

    [System.Serializable]
    public class Competence
    {
        public string nom; 
        [TextArea]
        public string description; 
        public Sprite image; 
        public int prix; 
        public GameObject self;
    }

    public static bool isNext; 

    public List<Competence> competences; 
    private void Awake() 
    {
        if(PlayerPrefs.GetInt("argent") != 0)
        {
            argent_txt.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("argent").ToString(); 
        }

        Debug.Log(Helper.getArgent());
        Debug.Log(PlayerPrefs.HasKey("casserole"));

        if(Helper.getArgent() > competences[0].prix && !PlayerPrefs.HasKey("casserole"))
        {
            competences[0].self.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            competences[0].self.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = false;
        }

        if(Helper.getArgent() > competences[1].prix && !PlayerPrefs.HasKey("louche"))
        {
            competences[1].self.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = true; 
        }
        else
            competences[1].self.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = false;
        if(Helper.getArgent() > competences[2].prix && !PlayerPrefs.HasKey("rappe"))
        {
            competences[2].self.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = true; 
        }
        else
            competences[2].self.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = false;

        if(Helper.getArgent() > competences[3].prix && !PlayerPrefs.HasKey("biere") )        
        {
            competences[3].self.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = true; 
        }
        else
            competences[3].self.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = false;

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
        }
    }

    public void nextPage()
    {
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

        competences[1].self.SetActive(false); 
        competences[2].self.SetActive(false); 

        GameObject.Find("Prece_btn").GetComponent<Button>().interactable = true; 
        GameObject.Find("Suiv_btn").GetComponent<Button>().interactable = false; 
    }

    public void previousPage()
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
        }

        GameObject.Find("Prece_btn").GetComponent<Button>().interactable = false; 
        GameObject.Find("Suiv_btn").GetComponent<Button>().interactable = true; 
    }

    public List<Competence> getCompetences()
    {
        return competences;
    }
}
