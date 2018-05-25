using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

    float esperar = 0.7f;
    float tempo = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
        if (esperar < tempo)
            Destroy(this.gameObject);
    }
}
