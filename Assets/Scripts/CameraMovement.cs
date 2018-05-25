using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public PolygonCollider2D pc2d;
    float fx = 1f;
    float fy = 1f;
    Camera c;

    // Use this for initialization
    void Start () {
        if (!target)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        if (!pc2d)
        {
            pc2d = this.gameObject.GetComponent<PolygonCollider2D>();
        }
        c = this.gameObject.GetComponent<Camera>();
        fx = (c.aspect * c.orthographicSize);
        fy = c.orthographicSize;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 v3 = target.position;
        v3.z = transform.position.z;
        transform.position = v3;
        //
        float fx = (c.aspect * c.orthographicSize);
        float fy = c.orthographicSize;
        Vector2[] matriz = { new Vector2(fx, fy), new Vector2(fx, -fy), new Vector2(-fx, -fy), new Vector2(-fx, fy) };
        pc2d.SetPath(0, matriz);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().hostil = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().hostil = false;
        }
        if (collision.gameObject.tag == "PlayerProjectile" || collision.gameObject.tag == "EnemyProjectile")
            Destroy(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameManagerScript.gm.danoInimigosNaTela)
        {
            int dano = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsScript>().poder;
            if (collision.gameObject.tag == "Enemy")
            {
                if (!collision.gameObject.GetComponent<EnemyScript>().isBombardeado()) {
                    collision.gameObject.GetComponent<StatsScript>().Damage(99 + dano);
                    collision.gameObject.GetComponent<EnemyScript>().Bombardeado(true);
                    Debug.Log((int)(99 + dano) + " em " + collision.gameObject.name);
                }
            }
        }
    }
}
