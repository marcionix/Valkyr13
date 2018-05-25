using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AditionButtonScript : MonoBehaviour {

    public bool upPoder;
    public bool upArmadura;
    public bool upVelocidade;
    public bool upSouls;
    public bool upCadencia;
    public bool upHealth;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "PlayerProjectile")
        {
            if (upPoder) {
                GameManagerScript.gm.player.GetComponent<StatsScript>().PowerUp();
            }
            if (upArmadura) {
                GameManagerScript.gm.player.GetComponent<StatsScript>().ArmorUp();
            }
            if (upVelocidade)
            {
                GameManagerScript.gm.player.GetComponent<StatsScript>().SpeedUp();
            }
            if (upSouls)
            {
                GameManagerScript.gm.player.GetComponent<StatsScript>().MaxSoulsUp();
            }
            if (upCadencia)
            {
                GameManagerScript.gm.player.GetComponent<StatsScript>().CadenciaUp();
            }
            if (upHealth)
            {
                GameManagerScript.gm.player.GetComponent<StatsScript>().MaxHealthUp();
            }
            collision.gameObject.GetComponent<BulletScript>().splash();
        }
    }
}
