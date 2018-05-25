using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMotorScript : MonoBehaviour {

    public UnityEngine.UI.Image liquido;
    public UnityEngine.UI.Image motor;
    public Sprite motorNaoCheio;
    public Sprite motorCheio;
    public GameObject player;

    private float pyLiquido = -30;
    private Vector2 posLiquido;

	// Use this for initialization
	void Start () {
        posLiquido = liquido.rectTransform.anchoredPosition;

    }
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<StatsScript>().souls >= player.GetComponent<StatsScript>().maxSouls)
        {
            motor.sprite = motorCheio;
        }
        else
        {
            motor.sprite = motorNaoCheio;
        }

        posLiquido = new Vector3(posLiquido.x, pyLiquido + ((float)player.GetComponent<StatsScript>().souls * (30f / (float)player.GetComponent<StatsScript>().maxSouls)));
        liquido.rectTransform.anchoredPosition = posLiquido;
    }
}
