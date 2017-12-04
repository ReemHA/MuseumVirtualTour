using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public bool VR;
    public GameObject fadingSphere;
    public GameObject VR_PlayerObj;
    public GameObject MobileMode;
    private GameObject fadingObject;
    private bool once;
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        SceneManager.sceneLoaded += DisableGameObject;
    }

    private void DisableGameObject(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name.Equals("2 OuterScene"))
        {
            TurnOnOffVR(PlayerPrefs.GetString("DecidePref"));
        }
        else if (arg0.name.Equals("1 ChoiceScene"))
        {
            transform.GetChild(0).GetChild(2).transform.localPosition = 
                new Vector3(-0.7f, 0f, 12.95f);
            transform.GetChild(0).GetChild(2).transform.localRotation.eulerAngles.Set(0, -180f, 0f);

            transform.GetChild(1).transform.localPosition = new Vector3(-126.3f, 10.3f, -77.8f);
            transform.GetChild(1).transform.localRotation.eulerAngles.Set(0, -221.8f, 0f);
        }
    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name.Equals("3 InnerScene"))
        {
            if (!once)
            {
                fadingSphere.GetComponent<Animator>().SetBool("transit", true);
                once = true;
            }
        }
    }

    IEnumerator LoadDevice(string newDevice)
    {
        VRSettings.LoadDeviceByName(newDevice);
        yield return null;
        VRSettings.enabled = true;
    }
    public void TurnOnOffVR(string answer)
    {
        if (answer == "yes")
        {
            StartCoroutine(LoadDevice("cardboard"));
            VR_PlayerObj.SetActive(true);
            MobileMode.SetActive(false);
            VR = true;
        }
        else
        {
            VR_PlayerObj.SetActive(false);
            MobileMode.SetActive(true);
            VR = false;
        }
    }
}
