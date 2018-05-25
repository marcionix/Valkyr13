using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericDisplayScript : MonoBehaviour {

    public SpriteRenderer centena;
    public SpriteRenderer dezena;
    public SpriteRenderer unidade;
    private int valor = 0;

    public Sprite[] n;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int cent = (valor % 1000) / 100;
        int dez = (valor % 100) / 10;
        int uni = (valor % 10);

        centena.sprite = n[cent];
        dezena.sprite = n[dez];
        unidade.sprite = n[uni];
    }

    public void setValor(int value)
    {
        valor = value;
    }


}
