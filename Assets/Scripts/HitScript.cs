using UnityEngine;
using System.Collections;

public class HitScript : MonoBehaviour {
    
    float esperar = 0.2f;
    float tempo = 0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        tempo += Time.deltaTime;
        if (esperar < tempo)
            Destroy(this.gameObject);
    }
}
