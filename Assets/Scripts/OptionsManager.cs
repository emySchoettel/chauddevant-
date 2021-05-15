using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    private Helper helper; 

    private void Start() 
    {
        helper = GameObject.FindObjectOfType<Helper>() ?  GameObject.FindObjectOfType<Helper>() : null; 
    }
    public void saveOptions()
    {
        helper.click_retour_menu_principal();
    }
}
