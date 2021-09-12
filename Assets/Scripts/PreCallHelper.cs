using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreCallHelper : MonoBehaviour
{   
    [SerializeField]
    private AudioSource audioSource; 
    private void Awake() 
    {
        //Music awake
        int volumeMusic = Helper.verifyVolumeMusic();
        float realVolume = volumeMusic / 100.0f;
        if(volumeMusic >= 0)
        {
            if(volumeMusic > 0)
            {
                audioSource.volume = realVolume;        
            }
            else
                audioSource.volume = 0.5f;
        }       

        
    }
}
