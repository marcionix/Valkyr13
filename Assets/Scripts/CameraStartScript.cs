using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStartScript : MonoBehaviour {

    public string nome = "Fase";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nome);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
