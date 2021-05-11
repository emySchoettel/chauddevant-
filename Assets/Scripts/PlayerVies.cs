using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; 

public class PlayerVies : MonoBehaviour
{
    [SerializeField] private int vies = 3; 
    public GameObject[] emplacementsVies; 
    private void OnEnable() 
    {
        GameObject Emplacement = GameObject.Find("EmplacementsVies"); 
        for (int i = 0; i < Emplacement.transform.childCount; i++)
        {
            emplacementsVies[i] = Emplacement.transform.GetChild(i).gameObject;
        }
    }

    public void setVies(int i)
    {
        vies = i;
        emplacementsVies[vies].gameObject.SetActive(false);
    }


    public int getVies()
    {
        return vies; 
    }

    public bool haslifes()
    {
        if(getVies() != 0)
            return true; 
        else 
            return false; 
    }
}
