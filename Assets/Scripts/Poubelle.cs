using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poubelle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Nourriture"))
        {
            if(other.gameObject.GetComponent<NourritureItem>().clear)
            {
                Destroy(other.gameObject);
            }
        }
        else if(other.transform.CompareTag("Dechet"))
        {
            Destroy(other.gameObject);
        }
    }
}
