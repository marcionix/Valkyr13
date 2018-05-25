using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservaScript : MonoBehaviour
{

    public GameObject soulsFluid;
    private float iniPosSoulsBar;
    private StatsScript ss;

    // Use this for initialization
    void Start()
    {
        iniPosSoulsBar = soulsFluid.transform.localPosition.y;
        ss = this.gameObject.GetComponent<StatsScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void reposicionar(int contarAlmas, int maxAlmas)
    {
        float floatY = iniPosSoulsBar - ((iniPosSoulsBar / maxAlmas) * contarAlmas);
        soulsFluid.transform.localPosition = new Vector3(soulsFluid.transform.localPosition.x, floatY, soulsFluid.transform.localPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            collision.gameObject.GetComponent<BulletScript>().splash();
            int contarAlmas = GameManagerScript.gm.player.GetComponent<StatsScript>().souls;
            if (ss.souls < ss.maxSouls)
            {
                if (contarAlmas > (ss.maxSouls - ss.souls))
                {
                    contarAlmas = contarAlmas - (ss.maxSouls - ss.souls);
                }
                GameManagerScript.gm.player.GetComponent<StatsScript>().SubSouls(contarAlmas);
                ss.souls += contarAlmas;
                reposicionar(ss.souls, ss.maxSouls);
            }
        }
    }
}
