using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerSceneManager : MonoBehaviour
{

    public Transform playerStartingPoint;
    public GameObject loadingObj;
    private Camera mainCamera;
    public static InnerSceneManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera.transform.parent.tag.Equals("Player"))
        {
            mainCamera.transform.parent.position = new Vector3(playerStartingPoint.position.x,
                1.35f, playerStartingPoint.position.z);
            mainCamera.transform.parent.rotation = playerStartingPoint.rotation;
        }
        else
        {
            mainCamera.transform.parent.position = new Vector3(playerStartingPoint.position.x,
                0, playerStartingPoint.position.z);
        }
    }
}
