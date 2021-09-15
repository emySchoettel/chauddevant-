using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using UnityEngine.UI;
using TMPro; 

public class Achats_v2 : MonoBehaviour
{
    [SerializeField]
    private Button skinBtn; 

    [SerializeField]
    private Button compBtn; 

    public bool isSkinClicked = false; 

    public bool isCompetencesClicked = false; 

     [SerializeField]
    private Button nextButton; 

    [SerializeField]
    private Button previousButton; 

    [SerializeField]
    private List<Competence> competences; 

    [System.Serializable]
    public class Competence
    {
        public enum typeBonus 
        {
            casserole,
            louche, 
            rappe,
            biere
        }

        public typeBonus typeBonusEnum;
        public string nom;
        public string description;
        public int prix; 
        public Sprite image; 
        public GameObject self;

        public bool isPurchased = false; 

        public void setIsPurchased()
        {
            switch(typeBonusEnum)
            {
                case typeBonus.casserole:
                    if(Helper.verifyAchat("achats.comp.casserole"))
                    {
                        isPurchased = true; 
                    }
                break;
                case typeBonus.louche: 
                    if(Helper.verifyAchat("achats.comp.louche"))
                    {
                        isPurchased = true; 
                    }
                break; 
                case typeBonus.rappe:
                    if(Helper.verifyAchat("achats.comp.rappe"))
                    {
                        isPurchased = true; 
                    }
                break; 
                case typeBonus.biere:
                    if(Helper.verifyAchat("achats.comp.biere"))
                    {
                        isPurchased = true;
                    }
                break;
            }
        }

        public GameObject getSelf()
        {
            return self; 
        }

    }

    [System.Serializable]
    public class Skin : MonoBehaviour
    {

        public enum typeSkins 
        {
            poele,
            planche,
            grille,
            bolo
        }

        public typeSkins typeSkinsEnum; 
        public string nom; 
        public string description; 

        public int prix; 

        public Sprite image;

        public bool isPurchased;

        public GameObject self; 

        public void setIsPurchased()
        {
            switch(typeSkinsEnum)
            {
                case typeSkins.planche:
                    if(Helper.verifyAchat("achats.skin.planche"))
                    {
                        isPurchased = true; 
                    }
                break;
                case typeSkins.grille: 
                    if(Helper.verifyAchat("achats.skin.grille"))
                    {
                        isPurchased = true; 
                    }
                break; 
                case typeSkins.bolo:
                    if(Helper.verifyAchat("achats.skin.bolo"))
                    {
                        isPurchased = true; 
                    }
                break; 
            }
        }

        public void changeTypeSkinEnum(typeSkins tS)
        {
            typeSkinsEnum = tS;
        }

        public void setParams(string nom, string description, int prix, Sprite image, bool isPurchased, GameObject self)
        {
            this.nom = nom;
            this.description = description; 
            this.prix = prix; 
            this.image = image; 
            this.isPurchased = isPurchased;
            this.self = self; 
        }
    }

    [SerializeField]
    private List<GameObject> SkinsGO; 

      [SerializeField]
    private List<Skin> Skins; 

    [SerializeField]
    private TextMeshProUGUI argent_txt; 

    private void Awake() 
    {
        skinBtn.GetComponentInChildren<TextMeshProUGUI>().text = Translation.Get("achats.bouton.skins");
        compBtn.GetComponentInChildren<TextMeshProUGUI>().text = Translation.Get("achats.bouton.competences");

        createSkins();


        if(PlayerPrefs.GetInt("argent") != 0)
        {
            argent_txt.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("argent").ToString(); 
        }

        Competence actualCompetence; 
        //global
        for (int i = 0; i < competences.Count; i++)
        {   actualCompetence = competences[i];
            actualCompetence.setIsPurchased();
        }

        //local
        for (int i = 0; i < 3; i++)
        {
            actualCompetence = competences[i];

            if(actualCompetence.isPurchased)
            {
                actualCompetence.getSelf().transform.GetComponentInChildren<Button>().interactable = false; 
            }
            else //if not purchased
            {
                //if gold enough
                if(Helper.verifyGoldForPurchase(actualCompetence.prix))
                {
                    actualCompetence.self.GetComponentInChildren<Button>().interactable = true; 
                }
                else//if not
                {
                     actualCompetence.self.GetComponentInChildren<Button>().interactable = false; 
                }
            }
            actualCompetence.self.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = actualCompetence.nom;
            actualCompetence.self.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = actualCompetence.description;
            actualCompetence.self.transform.GetChild(3).GetComponent<Image>().sprite = actualCompetence.image;
            actualCompetence.self.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = actualCompetence.prix.ToString();

        }
    }

    public void purchaseItem()
    {
        GameObject objectClicked = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        GameObject parentClicked = objectClicked.transform.parent.gameObject;

        //Toast.Create();

        //if is a skin 
        if(parentClicked.GetComponent<Skin>() != null)
        {
            Skin theSkinOfParent = parentClicked.GetComponent<Skin>();
            switch(theSkinOfParent.typeSkinsEnum)
            {
                case Skin.typeSkins.planche:
                    PlayerPrefs.SetInt("achats.skin.planche", 1);
                    
                break;

                case Skin.typeSkins.grille:
                    PlayerPrefs.SetInt("achats.skin.planche", 1);
                break; 

                case Skin.typeSkins.bolo:
                    PlayerPrefs.SetInt("achats.skin.planche", 1);
                break;
            }
            objectClicked.GetComponent<Button>().interactable = false;
            theSkinOfParent.setIsPurchased();
            Helper.setArgent(theSkinOfParent.prix);
        }
        //if is a comp
        else
        {
            if(parentClicked == competences[0].self && parentClicked.transform.GetComponentInChildren<TextMeshProUGUI>().text == "Casserole") //comp casserole
            {
                PlayerPrefs.SetInt("achats.comp.casserole", 1);
                competences[0].setIsPurchased();
                objectClicked.GetComponent<Button>().interactable = false;
                Helper.setArgent(competences[0].prix);
            }
            else if(parentClicked == competences[1].self) //comp louche 
            {   
                PlayerPrefs.SetInt("achats.comp.louche", 1);
                competences[1].setIsPurchased();
                Helper.setArgent(competences[1].prix);
                objectClicked.GetComponent<Button>().interactable = false;
            }
            else if(parentClicked == competences[2].self) //comp rappe 
            {
                PlayerPrefs.SetInt("achats.comp.rappe", 1);
                competences[2].setIsPurchased();
                Helper.setArgent(competences[2].prix);
                objectClicked.GetComponent<Button>().interactable = false;
            }
            else if(parentClicked == competences[3].self && parentClicked.transform.GetComponentInChildren<TextMeshProUGUI>().text == "Biere")  //comp biere
            {
                PlayerPrefs.SetInt("achats.comp.biere", 1);
                competences[3].setIsPurchased();
                Helper.setArgent(competences[3].prix);
                objectClicked.GetComponent<Button>().interactable = false;
            }
            
        }
        PlayerPrefs.Save();
        updateMoney();
    }

    private void updateMoney()
    {
        argent_txt.text = Helper.getArgent().ToString();
    }
    private void getCompetencesStuff()
    {
        Competence actualCompetence; 
        for (int i = 0; i < competences.Count; i++)
        {   actualCompetence = competences[i];
            actualCompetence.setIsPurchased();
        }

        for (int i = 0; i < 3; i++)
        {
            actualCompetence = competences[i];
           if(actualCompetence.isPurchased)
            {
                actualCompetence.getSelf().transform.GetComponentInChildren<Button>().interactable = false; 
            }
            else //if not purchased
            {
                //if gold enough
                if(Helper.verifyGoldForPurchase(actualCompetence.prix))
                {
                    actualCompetence.self.GetComponentInChildren<Button>().interactable = true; 
                }
                else//if not
                {
                     actualCompetence.self.GetComponentInChildren<Button>().interactable = false; 
                }
            }
            actualCompetence.self.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = actualCompetence.nom;
            actualCompetence.self.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = actualCompetence.description;
            actualCompetence.self.transform.GetChild(3).GetComponent<Image>().sprite = actualCompetence.image;
            actualCompetence.self.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = actualCompetence.prix.ToString();

        }

            competences[0].self.SetActive(true);
            competences[1].self.SetActive(true);
            competences[2].self.SetActive(true);
            Skins[0].self.SetActive(false);
            Skins[1].self.SetActive(false);
            Skins[2].self.SetActive(false);
    }

    private void getSkinStuff()
    {
        Skin actualSkin; 

        for (int i = 0; i < Skins.Count; i++)
        {   actualSkin = Skins[i];
            actualSkin.setIsPurchased();
        }

        for (int i = 0; i < 3; i++)
        {
            actualSkin = Skins[i];
            if(actualSkin.isPurchased)
            {
                actualSkin.self.GetComponentInChildren<Button>().interactable = false; 
            }
            else //if not purchased
            {
                //if gold enough
                if(Helper.verifyGoldForPurchase(actualSkin.prix))
                {
                    actualSkin.self.GetComponentInChildren<Button>().interactable = true; 
                }
                else//if not
                {
                     actualSkin.self.GetComponentInChildren<Button>().interactable = false; 
                }
            }
            actualSkin.self.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = actualSkin.nom;
            actualSkin.self.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = actualSkin.description;
            actualSkin.self.transform.GetChild(3).GetComponent<Image>().sprite = actualSkin.image;
            actualSkin.self.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = actualSkin.prix.ToString();

        }

            //set active all
            competences[0].self.SetActive(false);
            competences[1].self.SetActive(false);
            competences[2].self.SetActive(false);
            Skins[0].self.SetActive(true);
            Skins[1].self.SetActive(true);
            Skins[2].self.SetActive(true);
    }

    public bool createSkins()
    {
        Skin theskin = SkinsGO[0].AddComponent<Skin>();
        string nom = Translation.Get("achats.skins.planche.nom");
        string description = Translation.Get("achats.skins.planche.description");
        int prix = int.Parse(Translation.Get("achats.skins.planche.prix"));
        Sprite image = SkinsGO[0].transform.GetChild(3).GetComponent<Image>().sprite;
        theskin.setParams(nom, description, prix, image, false, SkinsGO[0]);

        theskin = SkinsGO[1].AddComponent<Skin>();
        nom = Translation.Get("achats.skins.grille.nom");
        description = Translation.Get("achats.skins.grille.description");
        prix = int.Parse(Translation.Get("achats.skins.grille.prix"));
        image = SkinsGO[1].transform.GetChild(3).GetComponent<Image>().sprite;
        theskin.setParams(nom, description, prix, image, false, SkinsGO[1]);

        theskin = SkinsGO[2].AddComponent<Skin>();
        nom = Translation.Get("achats.skins.bolo.nom");
        description = Translation.Get("achats.skins.bolo.description");
        prix = int.Parse(Translation.Get("achats.skins.bolo.prix"));
        image = SkinsGO[2].transform.GetChild(3).GetComponent<Image>().sprite;
        theskin.setParams(nom, description, prix, image, false, SkinsGO[2]);

        for (int i = 0; i < SkinsGO.Count; i++)
        {
            Skins[i] = SkinsGO[i].GetComponent<Skin>();
        }

        return true; 
    }

    public void skinButtonClick()
    {
        nextButton.interactable = false; 
        previousButton.interactable = false; 
        compBtn.interactable = true; 
        skinBtn.interactable = false; 

        getSkinStuff();

    }

    public void comptButtonClick()
    {
        nextButton.interactable = true; 
        previousButton.interactable = false; 
        compBtn.interactable = false; 
        skinBtn.interactable = true; 

        getCompetencesStuff();
    }

    public void nextButtonClick()
    {
        previousButton.interactable = true; 
        nextButton.interactable = false; 

        //iteration for competences; 
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

       if(competences[i].isPurchased)
        {
            competences[i].getSelf().transform.GetComponentInChildren<Button>().interactable = false; 
        }
        else //if not purchased
        {
            //if gold enough
            if(Helper.verifyGoldForPurchase(competences[i].prix))
            {
                competences[i].self.GetComponentInChildren<Button>().interactable = true; 
            }
            else//if not
            {
                    competences[i].self.GetComponentInChildren<Button>().interactable = false; 
            }
        }

        competences[1].self.SetActive(false); 
        competences[2].self.SetActive(false); 

        //iteration for skins; 
    }

    public void previousButtonClick()
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

            Competence actualCompetence = competences[i];
            if(actualCompetence.isPurchased)
        {
            actualCompetence.getSelf().transform.GetComponentInChildren<Button>().interactable = false; 
        }
        else //if not purchased
        {
            //if gold enough
            if(Helper.verifyGoldForPurchase(actualCompetence.prix))
            {
                actualCompetence.self.GetComponentInChildren<Button>().interactable = true; 
            }
            else//if not
            {
                actualCompetence.self.GetComponentInChildren<Button>().interactable = false; 
            }
        }
            
        }

        if(!Helper.verifyAchat("achats.comp.casserole") && Helper.getArgent() > competences[0].prix)
        {
            competences[0].self.transform.GetChild(5).GetComponent<Button>().interactable = true; 
        }

        previousButton.interactable = false; 
        nextButton.interactable = true; 
    }
}
