using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : MonoBehaviour
{
    public enum position{
        hide,
        show
    }

    public static GameObject prefabStatic;

    [SerializeField]
    private GameObject prefab;

    public position thisposition = position.hide;

    private void Awake() 
    {
        prefabStatic = prefab;     
    }
    public static void Create()
    {
        Instantiate(prefabStatic, prefabStatic.transform.position, Quaternion.identity);
    }
}
