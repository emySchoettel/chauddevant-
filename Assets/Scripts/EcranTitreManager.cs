using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EcranTitreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txt_argent;  

    [SerializeField]
    private GameObject ms_canvas;
    private void Start() 
    {
        if(txt_argent != null)
            txt_argent.text = Helper.getArgent().ToString(); 
    }

    private void Awake() 
    {
        if(ms_canvas != null && ms_canvas.activeSelf == true)
            ms_canvas.SetActive(false);
    }

    public void afficherMeilleurScore()
    {
        TextMeshProUGUI textMS = ms_canvas.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        textMS.text = Helper.getMeilleurScore().ToString(); 
    }
}
