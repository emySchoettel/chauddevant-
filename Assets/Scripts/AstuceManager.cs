using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstuceManager : MonoBehaviour
{
    [System.Serializable]
    public class Astuce
    {
        public string nom; 
        public Image icon; 
        public string description;
    }

    [SerializeField]
    private List<Astuce> lesastucesDuChef; 

    [SerializeField]
    private List<Astuce> astucesActuelles;
}
