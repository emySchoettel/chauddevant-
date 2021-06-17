using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class TutoManager : MonoBehaviour
{
    public List<GameObject> cadres, nourritures, dechets;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI; 
    [SerializeField] private int stage = 1; 
    [SerializeField] private GameObject currentcadre, panel; 
    void Start()
    {

        if(panel != null)
        {
            panel.SetActive(true);
        }
        if(GameObject.Find("tmp_explications") != null)
            textMeshProUGUI = GameObject.Find("tmp_explications").GetComponent<TextMeshProUGUI>();
        
    
        if(textMeshProUGUI != null)
        {
            //stage++; 
            textMeshProUGUI.text = Translation.Get("tutoriel.jeu.pt1");
            StartCoroutine(LetsTutorielBegin());
        }
            
    }

    IEnumerator LetsTutorielBegin()
    {
        Debug.Log("start coroutine");
        yield return ReadText();
//        while(Input.GetTouch(0).phase != TouchPhase.Began || !Input.GetMouseButtonDown(0))
        for (int i = 0; i < 6; i++)
        {
            Debug.Log(i);
            while(!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }
            if(stage < 5)
            {
                Debug.Log("Iteration : " + stage);
                yield return ReadText();
            }
            else
            {
                Debug.Log("end of tutoriel");
                EndOfTutoriel(); 
            }
        }
    }

    IEnumerator ReadText()
    {
        currentcadre = cadres[stage]; 
        currentcadre.GetComponent<Image>().enabled = true; 
        string nom = "tutoriel.jeu.pt" + (stage +1).ToString();
        textMeshProUGUI.text = Translation.Get(nom);
        stage++;
        yield return null;
    }

    IEnumerator CountDownForLaunchGame()
    {
        for (int i = 4; i >= 0 ; i--)
        {
            textMeshProUGUI.text = i.ToString();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void EndOfTutoriel()
    {
        Image image; 
        foreach(GameObject go in cadres)
        {
            image = go.GetComponent<Image>(); 
            image.enabled = false; 
            go.SetActive(false); 
        }
        
        textMeshProUGUI.transform.parent.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
