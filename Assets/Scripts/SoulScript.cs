using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulScript : MonoBehaviour {

    private int pontos = 10;
    public bool esvanecer = true;
    private float tempo = 0;
    public float esperar;

    private void Update()
    {
        if (esvanecer)
        {
            tempo += Time.deltaTime;
            if (esperar < tempo)
                Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        try
        {
            StatsScript ss = collider.gameObject.GetComponent<StatsScript>();
            if (ss.souls < ss.maxSouls && ss.vivo)
            {
                if (collider.tag == "Player")
                {
                    ss.AddSouls(pontos);
                    if (ss.souls > ss.maxSouls)
                        ss.souls = ss.maxSouls;
                    Destroy(this.gameObject);
                }
            }
        }catch(UnityException ue)
        {
            //não faz nada
        }
    }

    public void setPontos(int pontos)
    {
        this.pontos = pontos;
    }
}
