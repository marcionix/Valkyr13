using UnityEngine;
using System.Collections;
using System;

public class BulletScript : MonoBehaviour
{

    public float maxSpeed = 200f;
    float esperar = 10f;
    float tempo = 0f;
    float Angulo = 0f;
    private int poder = 1;
    public GameObject hitEffect;
    public bool enemyProjectile = false;
    public string[] tags;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Angulo);
        transform.Translate(Vector3.right * Time.deltaTime * maxSpeed);

        tempo += Time.deltaTime;
        if (esperar < tempo)
            Destroy(this.gameObject);

        if(Physics2D.OverlapPoint(this.transform.position, GameManagerScript.gm.whatIsGround))
        {
            splash();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)  
    {
        if (doNothing(collision.gameObject.tag))
        {
            return;
        }
        try
        {
            StatsScript ss = collision.gameObject.GetComponent<StatsScript>();
            if (ss == null || !ss.vivo)
                return;

            if (ss != null)
                ss.Damage(poder);
        }catch(UnityException ue)
        {
            //
        }
        splash();
    }

    public void splash()
    {
        if (hitEffect)
        {
            GameObject effect = Instantiate(hitEffect, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
            effect.transform.parent = null;
        }
        Destroy(this.gameObject);
    }

    public void SetPoder(int value)
    {
        poder = value;
    }

    public void setAngulo(float f)
    {
        this.Angulo = f;
    }

    public bool doNothing(string tag)
    {
        foreach (string t in tags)
        {
            if (tag == t)
            {
                return true;
            }
        }
        return false;
    }
}
