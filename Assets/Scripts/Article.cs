using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Article : MonoBehaviour
{
    [SerializeField]
    private string nom; 
    [SerializeField]
    private string description; 
    [SerializeField]
    private int prix; 
    [SerializeField]
    private bool isPurchased = false;
    [SerializeField]

    private GameObject GO; 

    public void setParams(string nom, string description, int prix, GameObject gameObject = null)
    {
        this.nom = nom;
        this.description = description; 
        this.prix = prix; 
        GO = gameObject;
    }

}
