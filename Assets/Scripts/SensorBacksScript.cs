using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBacksScript : MonoBehaviour {

    public Transform parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player" || collision.gameObject.tag == "PlayerProjectile")
        {
            Vector3 v3 = parent.localScale;
            v3.x *= -1;
            parent.localScale = v3;
        }
    }
}
