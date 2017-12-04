using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItemMobile : MonoBehaviour {

    public GameObject InteractObj;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.parent.name == "MobileMode")
        {
            InteractObj.GetComponent<Interact>().ShowInfo();
            Debug.Log("ShowInfo");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.name == "MobileMode")
        {
            InteractObj.GetComponent<Interact>().HideInfo();
            Debug.Log("HideInfo");
        }
    }
}
