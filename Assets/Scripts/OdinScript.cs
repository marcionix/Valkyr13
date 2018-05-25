using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinScript : MonoBehaviour {

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float alpha = Random.Range(0.5f,0.8f);
        sr.color = new Color(1f, 1f, 1f, alpha);
	}
}
