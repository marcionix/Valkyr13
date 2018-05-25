using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public int level = 1;
    public float multiplicadorLVL = 0.3f;
    //public Transform corpoTransform;
    public GameObject sensorBacks;
    public Transform[] firepoints;
    public Vector3[] firepointsPosR;
    public Vector3[] firepointsPosL;
    public GameObject projectile;
    public float reloadTime = 3;
    public bool hostil = false;
    public bool fireAtWill = true;
    public bool inimigoAtivo = false;

    public bool rightLooking = true;
    public bool oldRightLooking = true;

    public bool inserido = false;

    private StatsScript ss;
    private float reloading = 0f;

    private bool atira;
    public Vector3 ultimaPosicao;
    public float anguloUltimaPosicao;
    public float distancia;
    private Rigidbody2D rb;
    private Vector3 posInicial;
    private bool bombardeado = false;
    private float recuperado = 0f;

    internal void UltimaPosicao(Vector3 position)
    {
        ultimaPosicao = position;
        
        Quaternion rot = Angulo(ultimaPosicao);
        anguloUltimaPosicao = rot.z;// * 180f;

        atira = false;
    }

    // Use this for initialization
    void Start ()
    {
        posInicial = transform.position;
        int r = UnityEngine.Random.Range(0, 2);
        r = r == 0 ? -1 : 1;
        transform.localScale = new Vector3(r,1,1);
        //
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        //recalcular os stats pelo level
        ss = this.gameObject.GetComponent<StatsScript>();
        ss.poder *= (int)(level * multiplicadorLVL);
        ss.armadura *= (int)(level * multiplicadorLVL);
        ss.vida *= (int)(level * multiplicadorLVL);
        ss.cadencia *= (int)(level * multiplicadorLVL);
        ss.velocidade = (int)(ss.velocidade * (level * (multiplicadorLVL*0.1)));
        ss.souls *= (int)(level * multiplicadorLVL);

        ss.poder = ss.poder <= 0 ? 1:ss.poder;
        ss.armadura = ss.armadura <= 0 ? 1 : ss.armadura;
        ss.vida = ss.vida <= 0 ? 1 : ss.vida;
        ss.maxVida = ss.vida;
        ss.cadencia = ss.cadencia <= 0 ? 1 : ss.cadencia;
        ss.velocidade = ss.velocidade <= 0 ? 1 : ss.velocidade;
        ss.souls = ss.souls <= 0 ? 1 : ss.souls;
    }

    public bool isBombardeado()
    {
        return bombardeado;
    }

    public void Bombardeado(bool v)
    {
        bombardeado = true;
    }

    void OnEnable()
    {
        ss.vivo = true;
        ss.vida = ss.maxVida;
        resetarPos();
    }

    // Update is called once per frame
    void Update () {
        distancia = Vector3.Distance(transform.position, ultimaPosicao);
        if(bombardeado == true && recuperado >= 0.5f)
        {
            //Debug.Log(this.gameObject.name + " recuperado");
            bombardeado = false;
            recuperado = 0f;
        }
        else if (bombardeado == true && recuperado < 0.5f)
        {
            //Debug.Log(this.gameObject.name + " recuperando em "+ recuperado);
            recuperado += Time.deltaTime;
        }

        oldRightLooking = rightLooking;
        if (gameObject.transform.localScale.x < 0)
            rightLooking = false;
        else if (gameObject.transform.localScale.x > 0)
            rightLooking = true;

        if (oldRightLooking != rightLooking)
        {
            //reposiciona os pontos de disparo
            for(int i = 0; i < firepoints.Length;i++)
            {
                if(rightLooking)
                    firepoints[i].localPosition = firepointsPosR[i];
                else
                    firepoints[i].localPosition = firepointsPosL[i];
            }
        }

        float cadenciado = (float)reloadTime / this.gameObject.GetComponent<StatsScript>().cadencia;

        if (reloading >= cadenciado && atira == true)
            reloading = 0;
        else
            reloading += Time.deltaTime;

        if (fireAtWill && hostil && atira && reloading == 0)
        {
            //posi = new Vector3(posi.x + 0.11f * this.transform.localScale.x, posi.y - 0.01f * this.transform.localScale.x, posi.z);
            foreach (Transform firePoint in firepoints)
            {
                if (projectile && GameManagerScript.gm.player)
                {
                    GameObject bullete = Instantiate(projectile, firePoint.position, firePoint.rotation) as GameObject;
                    bullete.transform.parent = null;
                    bullete.GetComponent<BulletScript>().SetPoder(this.gameObject.GetComponent<StatsScript>().poder);
                }
            }
            atira = false;
        }

    }

    private void FixedUpdate()
    {
        if (inimigoAtivo && ss.vivo && ((ultimaPosicao.x+ ultimaPosicao.y+ultimaPosicao.z)!=0) && distancia > 1)
        {
            Vector3 move = new Vector3(0f,0f, 0f);
            if (rightLooking)
            {
                move = new Vector3(Mathf.Cos(anguloUltimaPosicao) * ss.velocidade, 0 * Mathf.Sin(anguloUltimaPosicao) * ss.velocidade, 0f);
                transform.position += move * Time.fixedDeltaTime;
            }
            else
            {
                move = new Vector3(-Mathf.Cos(anguloUltimaPosicao) * ss.velocidade, 0 * -Mathf.Sin(anguloUltimaPosicao) * ss.velocidade, 0f);
                transform.position += move * Time.fixedDeltaTime;
            }
        }
    }

    public void AlvoLocalizado(Vector3 alvo)
    {
        mirar(alvo);
        atira = true;
        ultimaPosicao = new Vector3(0, 0, 0);
    }

    private void mirar(Vector3 alvo)
    {
        foreach (Transform firePoint in firepoints)
        {
            firePoint.rotation = Angulo(alvo);
        }
    }

    private Quaternion Angulo(Vector3 alvo)
    {
        Quaternion rot = new Quaternion(0,0,0,0);
        if (rightLooking)
            rot = Quaternion.LookRotation(gameObject.transform.position - alvo, Vector3.up);
        else
            rot = Quaternion.LookRotation(gameObject.transform.position - alvo, Vector3.down);
        return new Quaternion(0, 0, rot.z, rot.w);
    }

    public void resetarPos()
    {
        transform.position = posInicial;
        ultimaPosicao = new Vector3(0f, 0f, 0f);
        anguloUltimaPosicao = 0f;
        distancia = 0f;
    }
}
