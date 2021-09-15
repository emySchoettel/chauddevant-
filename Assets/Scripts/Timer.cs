using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft = 180.0f;
    void Update()
    {
        timeLeft -= Time.deltaTime;
        
        if (timeLeft < 0)
        {
           gameObject.GetComponent<GameManager>().endOfGame();
           this.enabled = false;
           
        }
    }
}
