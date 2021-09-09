using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Astuce : MonoBehaviour
{
    [SerializeField]
    private string nom; 
    [SerializeField]
    private Image icon; 
    [SerializeField]
    private string description;

    private bool isClicked = false; 

    public void setParams(string nom, Image image, string desc)
    {
        this.nom = nom; 
        icon = image; 
        description = desc;
    }

    public string getName()
    {
        return nom; 
    }

    public string getDescription()
    {
        return description; 
    }

    public bool clicked()
    {
        if(!isClicked)
        {
            isClicked = true; 
        }
        else
        {
            isClicked = false; 
        }
        return isClicked;
    }
}
