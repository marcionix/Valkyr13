using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript gm;
    public int souls = 0;
    public int maxSouls = 5000;
    public UnityEngine.UI.Image healthBar;
    private float healthBarSize;
    public Text mainScoreDisplay;
    public bool gameIsOver = false;
    public GameObject player;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemies;
    private StatsScript ss;

    private UnityEngine.SceneManagement.LoadSceneMode mode;
    public string[] tags;
    public GameObject[] reservas;

    public NumericDisplayScript numDisPoder;
    public NumericDisplayScript numDisArmadura;
    public NumericDisplayScript numDisVelocidade;
    public NumericDisplayScript numDisAlmas;
    public NumericDisplayScript numDisVida;
    public NumericDisplayScript numDisCadencia;
    
    public NumericDisplayScript numDisCusPoder;
    public NumericDisplayScript numDisCusArmadura;
    public NumericDisplayScript numDisCusVelocidade;
    public NumericDisplayScript numDisCusAlmas;
    public NumericDisplayScript numDisCusVida;
    public NumericDisplayScript numDisCusCadencia;
    public bool danoInimigosNaTela;

    // Use this for initialization
    void Start()
    {
        souls = 0;
        if (mainScoreDisplay)
            mainScoreDisplay.text = "0";

        if (gm == null)
            gm = this.gameObject.GetComponent<GameManagerScript>();

        if (healthBar != null)
            healthBarSize = healthBar.rectTransform.rect.width;
        else
            healthBarSize = 92f;

        ss = player.GetComponent<StatsScript>();
    }

    public int reservaTotal()
    {
        int valor = 0;
        foreach(GameObject go in reservas)
        {
            StatsScript ss = go.GetComponent<StatsScript>();
            valor += ss.souls;
        }
        return valor;
    }

    internal void reservaSubSouls(int value)
    {
        foreach (GameObject go in reservas)
        {
            StatsScript ss = go.GetComponent<StatsScript>();
            if (ss.souls >= value)
            {
                ss.souls -= value;
                value -= value;
            }
            else if(ss.souls > 0)
            {
                value -= ss.souls;
                ss.souls -= ss.souls;
            }
            go.GetComponent<ReservaScript>().reposicionar(ss.souls, ss.maxSouls);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainScoreDisplay)
            mainScoreDisplay.text = souls.ToString();

        GameObject barra = healthBar.gameObject;
        RectTransform rt = barra.transform as RectTransform;
        float width = ((float)ss.vida / (float)ss.maxVida) * (float)healthBarSize;
        rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);

        numDisPoder.setValor(player.GetComponent<StatsScript>().poder);
        numDisArmadura.setValor(player.GetComponent<StatsScript>().armadura);
        numDisVelocidade.setValor(player.GetComponent<StatsScript>().velocidade);
        numDisAlmas.setValor(player.GetComponent<StatsScript>().maxSouls);
        numDisVida.setValor(player.GetComponent<StatsScript>().maxVida);
        numDisCadencia.setValor(player.GetComponent<StatsScript>().cadencia);

        numDisCusPoder.setValor(player.GetComponent<StatsScript>().custoPoderC());
        numDisCusArmadura.setValor(player.GetComponent<StatsScript>().custoArmaduraC());
        numDisCusVelocidade.setValor(player.GetComponent<StatsScript>().custoVelocidadeC());
        numDisCusAlmas.setValor(player.GetComponent<StatsScript>().custoMaxSoulsC());
        numDisCusVida.setValor(player.GetComponent<StatsScript>().custoMaxVidaC());
        numDisCusCadencia.setValor(player.GetComponent<StatsScript>().custoCadenciaC());
    }
    
    public void addSouls(int points)
    {
        souls += points;
    }
    
    public void subSouls(int points)
    {
        souls -= points;
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

    public void RessussitarInimigos()
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (go.tag == "Enemy")
            {
                if (!go.active)
                    go.SetActive(true);
                go.GetComponent<EnemyScript>().resetarPos();
            }
        }
    }
}
