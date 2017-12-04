using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEntrance : MonoBehaviour
{

    public GameObject TiltObj;
    private GameObject LoadingObj;
    float currCountdownValue;

    void Start()
    {
        if (OuterSceneManager.Instance != null)
        {
            LoadingObj = OuterSceneManager.Instance.loadingObj;
        }
        else if (InnerSceneManager.Instance != null)
        {
            LoadingObj = InnerSceneManager.Instance.loadingObj;
        }
    }

    public IEnumerator StartCountdown(string sceneName, float countdownValue = 10)
    {
        currCountdownValue = countdownValue;
        LoadingObj.SetActive(true);
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        if (sceneName.Equals("1 ChoiceScene"))
        {
            foreach (Transform child in GameManager.Instance.gameObject.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        SceneManager.LoadScene(sceneName);
    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.name == "PlayerMob")
        {
            Exit("3 InnerScene");
        }
    }

    public void Exit(string name)
    {
        if (OuterSceneManager.Instance != null)
        {
            LoadingObj = OuterSceneManager.Instance.loadingObj;
        }
        else if (InnerSceneManager.Instance != null)
        {
            LoadingObj = InnerSceneManager.Instance.loadingObj;
        }

        StartCoroutine(StartCountdown(name, 2));
    }
}
