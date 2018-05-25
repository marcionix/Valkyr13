using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    StatsScript ss;

    public Transform inicial;
    public GameObject bomb;
    float retorno = 7f;
    float esperar = 2f;


    // Use this for initialization
    void Start () {
        ss = this.gameObject.GetComponent<StatsScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!ss.vivo)
        {
            if ((Input.GetButton("Jump") && esperar <= 0) || retorno <= 0)
            {
                GameManagerScript.gm.RessussitarInimigos();
                ss.Cure(ss.maxVida);
                PlayerMovement pm = gameObject.GetComponent<PlayerMovement>();
                pm.corpoInf.SetActive(true);
                pm.corpoSup.SetActive(true);
                teleportarParaInicio();
                retorno = 3f;
            }
            retorno -= Time.deltaTime;
            esperar -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (ss.vivo)
        {
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonDown("Back"))//autodestruição
            {
                ss.Damage(999999);
            }
            if ((Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("Fire1")) && !GameManagerScript.gm.danoInimigosNaTela)//bomba
            {
                if (ss.souls>=100) {
                    GameObject go = Instantiate(bomb, transform.position, transform.rotation, null);
                    GameManagerScript.gm.danoInimigosNaTela = true;
                    ss.SubSouls(100);
                }
            }
        }
    }

    private void teleportarParaInicio()
    {
        this.gameObject.transform.position = new Vector3(inicial.position.x, inicial.position.y, 0);
    }
}
