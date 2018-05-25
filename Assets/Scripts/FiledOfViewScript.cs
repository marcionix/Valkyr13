using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiledOfViewScript : MonoBehaviour {



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyScript es = this.gameObject.GetComponentInParent<EnemyScript>();
            if (es)
            {
                es.AlvoLocalizado(collision.gameObject.transform.position);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyScript es = this.gameObject.GetComponentInParent<EnemyScript>();
            if (es)
            {
                es.UltimaPosicao(collision.gameObject.transform.position);
            }
        }
    }
}
