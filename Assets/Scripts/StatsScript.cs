using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsScript : MonoBehaviour {

    public int vida;
    public int maxVida;
    public int poder;
    public int armadura;
    public int velocidade;
    public bool vivo = true;
    private bool morto = false;
    public int souls = 0;
    public int maxSouls = 100;
    public int cadencia;
    public GameObject explosao;
    public float explosionScale = 1f;
    public GameObject soul;
    public GameObject displayDamage;
    public int custoMaxVida;
    public int custoPoder;
    public int custoArmadura;
    public int custoVelocidade;
    public int custoMaxSouls;
    public int custoCadencia;
    

    // Update is called once per frame
    void Update () {
        if (!vivo && !morto)
        {
            GameObject go = Instantiate(explosao, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0), null);
            go.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
            if (soul)
            {
                if (this.gameObject.tag == "Player")
                    Destroy(GameObject.FindGameObjectWithTag("Lapide"));
                GameObject gosoul = Instantiate(soul, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0), null);
                try
                {
                    gosoul.GetComponent<SoulScript>().setPontos(souls);
                }catch(Exception e)
                {
                    //fazer nada
                }
            }
            if (this.gameObject.tag != "Player")
            {//para os inimigos cai aqui
                try {
                    if (this.gameObject.GetComponent<EnemyScript>().inserido == true)
                        Destroy(this.gameObject);
                    this.gameObject.SetActive(false);
                } catch (Exception e) {
                }
            }
            else
            {//para o player cai aqui
                morto = true;
            }
        }
	}

    public void Damage(int value)
    {
        if (vivo)
        {
            int valor = (value - armadura);
            if (valor <= 0)
                valor = 0;
            vida -= valor;
            if (vida <= 0)
                vida = 0;
            if (vida == 0)
                vivo = false;

            GameObject go = Instantiate(displayDamage, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0), null);
            go.GetComponent<NumericDisplayDamageScript>().setValor(valor);
        }
    }

    public void Cure(int value)
    {
        vida += value;
        if (!vivo)
        {
            vivo = true;
            morto = false;
        }
    }

    public void PowerUp()
    {
        int valor = custoPoderC();
        if (calcularSouls(valor))
            poder++;
    }

    public void ArmorUp()
    {
        int valor = armadura * custoArmadura;
        if (calcularSouls(valor))
            armadura++;
    }

    public void SpeedUp()
    {
        int valor = custoVelocidadeC();
        if (calcularSouls(valor))
            velocidade++;
    }

    public void MaxSoulsUp()
    {
        int valor = custoMaxSoulsC();
        if (calcularSouls(valor))
            maxSouls++;
    }

    public void MaxHealthUp()
    {
        int valor = custoMaxVidaC();
        if (calcularSouls(valor))
        {
            int vidaTemp = vida;
            int maxVidaTemp = maxVida;
            maxVida++;
            vidaTemp *= maxVida;
            vidaTemp /= maxVidaTemp;
            vida = vidaTemp;
        }

    }

    public void CadenciaUp()
    {
        int valor = custoCadenciaC();
        if(calcularSouls(valor))
            cadencia++;
    }

    public void AddSouls(int souls)
    {
        this.souls += souls;
    }

    public void SubSouls(int souls)
    {
        this.souls -= souls;
    }

    private bool calcularSouls(int valor)
    {
        if (souls >= (valor))
        {
            SubSouls(valor);
            valor -= valor;
        }
        else if (souls < valor && souls > 0)
        {
            if (souls + GameManagerScript.gm.reservaTotal() >= valor)
            {
                valor -= souls;
                SubSouls(souls);
            }
        }
        if (valor > 0 && GameManagerScript.gm.reservaTotal() >= valor)
        {
            GameManagerScript.gm.reservaSubSouls(valor);
            valor -= valor;
        }
        if (valor == 0)
            return true;
        else
            return false;
    }

    public int custoMaxVidaC() {
        return custoMaxVida;
    }
    public int custoPoderC() {
        return poder * custoPoder;
    }
    public int custoArmaduraC() {
        return armadura * custoArmadura;
    }
    public int custoVelocidadeC() {
        return velocidade * custoVelocidade;
    }
    public int custoMaxSoulsC() {
        return custoMaxSouls;
    }
    public int custoCadenciaC() {
        return cadencia * custoCadencia;
    }

}
