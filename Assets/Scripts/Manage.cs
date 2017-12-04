using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage : MonoBehaviour {
    public GameObject gameManager;
    public static Manage Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start () {
        if (GameManager.Instance == null)
        {
            GameObject gManager = Instantiate(gameManager, transform);
            gManager.transform.parent = null;
        }
    }

    void Update () {
		
	}
}
