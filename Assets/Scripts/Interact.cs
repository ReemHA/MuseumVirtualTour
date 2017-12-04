using System;
using System.Collections;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Material inactiveMaterial;
    public Material gazedAtMaterial;
    public GameObject man;
    public GameObject particles;
    public bool showMan;
    private GameObject manObj;
    private MeshRenderer meshRenderer;
    private GameObject player;
    private Camera mainCamera;
    private Transform playerGoToPosition;
    private GameObject infoBubble;
    private bool playerIsGazing;
    private bool infoShowOn;
    private float manXRotation;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        playerGoToPosition = this.transform.parent.GetChild(0);
        if (this.transform.parent.childCount > 2)
        {
            infoBubble = this.transform.parent.GetChild(3).gameObject;
        }
    }

    void Update()
    {
        if (manObj != null)
        {
            if (/*!playerIsGazing ||*/ !manObj.GetComponent<AudioSource>().isPlaying)
            {
                HideMan();
                HideInfo();
            }
        }
    }

    public void ChangeMaterial(bool isGazedAt)
    {
        meshRenderer.material = isGazedAt ? gazedAtMaterial : inactiveMaterial;
        playerIsGazing = isGazedAt;
        if (isGazedAt)
        {
            Invoke("CountTimer", 3);
            // StartCoroutine(CountTimer(3));
        }
        else
        {
            CancelInvoke("CountTimer");
            //StopCoroutine("CountTimer");
        }
    }

    public void HideInfo()
    {
        if (infoBubble != null)
        {
            infoBubble.gameObject.SetActive(false);
        }
    }

    void CountTimer()
    {
        MoveCamera();
        if (infoBubble != null)
        {
            ShowInfo();
        }
    }

    private void MoveCamera()
    {
        player.transform.position =
            new Vector3(playerGoToPosition.position.x,
            mainCamera.transform.position.y, playerGoToPosition.position.z);
        mainCamera.transform.forward = playerGoToPosition.forward;
    }

    public void ShowInfo()
    {
        if (GameObject.Find("Guid"))
            Destroy(GameObject.Find("Guid"));
        infoBubble.gameObject.SetActive(true);
        infoShowOn = true;
        if (showMan)
        {
            ShowMan();
        }
    }

    private void ShowMan()
    {
        var p = Instantiate(particles, transform.parent);
        StartCoroutine(DestroyObject(p));
        p.transform.position = infoBubble.transform.position;
        p.transform.position = new Vector3(p.transform.position.x + 1, 0f,
           p.transform.position.z);

        manObj = Instantiate(man);
        manObj.name = "Guid";
        manObj.transform.position = infoBubble.transform.position;
        manObj.transform.position = new Vector3(manObj.transform.position.x + 1, 0f,
            manObj.transform.position.z);
    }

    private IEnumerator DestroyObject(GameObject objectToBeDestroyed)
    {
        yield return new WaitForSeconds(3);
        Destroy(objectToBeDestroyed);
    }

    private void HideMan()
    {
        manObj.GetComponent<Animator>().SetBool("hide", true);
        StartCoroutine(DestroyObject(manObj));
    }

    void OnMouseDown()
    {
        if (infoBubble != null)
        {
            ShowInfo();
        }
    }
}
