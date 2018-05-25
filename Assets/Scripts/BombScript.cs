using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

    public float esperar = 0.5f;
    private float tempo = 0f;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
        if (esperar < tempo)
        {
            GameManagerScript.gm.danoInimigosNaTela = false;
            Destroy(this.gameObject);
        }
    }
}
