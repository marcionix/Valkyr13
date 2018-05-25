using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericDisplayDamageScript : MonoBehaviour {

    public SpriteRenderer mcentena;
    public SpriteRenderer mdezena;
    public SpriteRenderer munidade;
    public SpriteRenderer centena;
    public SpriteRenderer dezena;
    public SpriteRenderer unidade;
    private int valor = 0;
    float esperar = 1f;
    float tempo = 0f;
    float subir = 1f;
    float deslocar = 0f;

    public Sprite[] n;

    private void Start()
    {
        Vector3 v3 = this.gameObject.transform.position;
        deslocar = v3.x + Random.Range(-0.2f, 0.2f);
        subir = (v3.y + subir);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v3 = this.gameObject.transform.position;
        v3.y += (subir - v3.y) / 10;
        v3.x = deslocar;
        this.gameObject.transform.position = v3;

        tempo += Time.deltaTime;
        if (esperar < tempo)
            Destroy(this.gameObject);

    }

    public void setValor(int value)
    {
        valor = value;

        if (valor > 99999)
        {
            int mcent = (valor % 1000000) / 100000;
            mcentena.sprite = n[mcent];
        }
        else
            mcentena.sprite = null;
        if (valor > 9999)
        {
            int mdez = (valor % 100000) / 10000;
            mdezena.sprite = n[mdez];
        }
        else
            mdezena.sprite = null;
        if (valor > 999)
        {
            int muni = (valor % 10000) / 1000;
            munidade.sprite = n[muni];
        }
        else
            munidade.sprite = null;
        if (valor > 99)
        {
            int cent = (valor % 1000) / 100;
            centena.sprite = n[cent];
        }
        else
            centena.sprite = null;
        if (valor > 9)
        {
            int dez = (valor % 100) / 10;
            dezena.sprite = n[dez];
        }
        else
            dezena.sprite = null;
        int uni = (valor % 10);
        unidade.sprite = n[uni];
    }
}
