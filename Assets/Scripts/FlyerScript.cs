using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerScript : MonoBehaviour {

    public int range = 0;
    Vector3 v3;
    StatsScript ss;
    EnemyScript me;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        v3 = this.gameObject.transform.position;
        ss = this.gameObject.GetComponent<StatsScript>();
        me = this.gameObject.GetComponent<EnemyScript>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (ss.vivo)
        {
            Vector3 nv3 = this.gameObject.transform.position;
            float y = 0;// Mathf.Sin(nv3.x)*10;
            Vector3 move = new Vector3(ss.velocidade, 0f, 0f);
            if (me.rightLooking)
            {
                move = new Vector3(ss.velocidade, y, 0f);
                if (nv3.x > (v3.x + range))
                    this.gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                move = new Vector3((-1)*ss.velocidade, y, 0f);
                if (nv3.x < (v3.x - range))
                    this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            transform.position += move * Time.deltaTime;
            //rb.velocity = move;
        }
	}
}
