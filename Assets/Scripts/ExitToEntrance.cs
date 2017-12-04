using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToEntrance : MonoBehaviour
{

    public Material inactiveMaterial;
    public Material gazedAtMaterial;
    public GameObject fadingSphere;
    private MeshRenderer meshRenderer;
    private bool once;
    private bool returned;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        once = false;
        returned = false;
    }

    void Update()
    {
        if (fadingSphere.GetComponent<Animator>().
                    GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && returned)
        {
            if (!once)
            {
                foreach (Transform child in GameManager.Instance.gameObject.transform)
                {
                    child.gameObject.SetActive(false);
                }
                SceneManager.LoadScene("1 ChoiceScene");
                once = true;
            }
        }
    }


    public void ChangeMaterial(bool isGazedAt)
    {
        meshRenderer.material = isGazedAt ? gazedAtMaterial : inactiveMaterial;
        if (isGazedAt)
        {
            Invoke("Load", 5);
        }
        else
        {
            CancelInvoke("Load");
        }
    }

    void Load()
    {
        fadingSphere.SetActive(true);
        returned = true;
    }

}
