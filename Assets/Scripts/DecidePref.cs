using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

public class DecidePref : MonoBehaviour {

    public GameObject LoadingObj;

    void Start()
    {
        VRSettings.LoadDeviceByName("");
        VRSettings.enabled = false;
    }
    public void TurnOnOffVR(string answer)
    {
        if (answer == "yes")
        {
            PlayerPrefs.SetString("DecidePref", "yes");
        }
        else
        {
            PlayerPrefs.SetString("DecidePref", "no");
        }
        StartCoroutine(StartCountdown(2));
    }

    float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue = 10)
    {
        currCountdownValue = countdownValue;
        LoadingObj.SetActive(true);
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        SceneManager.LoadScene("2 OuterScene");
    }
}

