using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadInnerScene : MonoBehaviour
{
    public GameObject fadingSphere;
    public GameObject particles;
    private bool animStarted = false;
    private bool once = false;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fadingSphere = GameManager.Instance.transform.GetChild(1).GetChild(0).GetChild(1).gameObject;
        fadingSphere.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance.VR)
        {
            if ((transform.position.x == player.transform.position.x) ||
                    (transform.position.z == player.transform.position.z))
            {
                if (!animStarted)
                {
                    fadingSphere.SetActive(true);
                    animStarted = true;
                }

                if (fadingSphere.GetComponent<Animator>().
                    GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    if (!once)
                    {
                        SceneManager.LoadScene("3 InnerScene");
                        once = true;
                    }
                }
            }
        }
    }

    private IEnumerator DestroyObject(GameObject p)
    {
        yield return new WaitForSeconds(3f);
        Destroy(p);
    }

}
