using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterSceneManager : MonoBehaviour {

    public static OuterSceneManager Instance;
    public GameObject loadingObj;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
