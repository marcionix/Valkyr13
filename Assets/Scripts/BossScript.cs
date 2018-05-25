using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {
    
    public Transform[] specialfirepoints;
    public Vector3[] specialfirepointsPosR;
    public Vector3[] specialfirepointsPosL;
    StatsScript ss;
    public string nomeTelaFinal;
    float esperar = 6f;
    float tempo = 0f;

    // Use this for initialization
    void Start () {
        ss = this.gameObject.GetComponent<StatsScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!ss.vivo)
        {
            if (esperar >= tempo)
            {
                Debug.Log("Morreu!");
                UnityEngine.SceneManagement.SceneManager.LoadScene(nomeTelaFinal);
            }
            else
            {
                tempo += Time.deltaTime;
            }
        }
	}
}
