using UnityEngine;
using System.Collections;
using System;

public class ShootScript : MonoBehaviour
{

    public float reloadTime = 1.5f;
    float reloading = 0f;
    private float cadenciado;
    bool atira = false;
    public GameObject projectile;
    public Transform firePoint;
    private float fracao = (float)(1f / 180f);

    //direita
    public Sprite corpSupDNorte;
    public Sprite corpSupNordeste;
    public Sprite corpSupLeste;
    public Sprite corpSupSudeste;
    public Sprite corpSupDSul;
    //esquerda
    public Sprite corpSupENorte;
    public Sprite corpSupNoroeste;
    public Sprite corpSupOeste;
    public Sprite corpSupSudoeste;
    public Sprite corpSupESul;
    private PlayerMovement pm;
    private StatsScript ss;

    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        pm = this.gameObject.GetComponent<PlayerMovement>();
        ss = this.gameObject.GetComponent<StatsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ss.vivo)
        {
            cadenciado = (float)reloadTime / this.gameObject.GetComponent<StatsScript>().cadencia;

            bool faceToRight = GameManagerScript.gm.player.GetComponent<PlayerMovement>().rightLooking;
            bool faceToUp = GameManagerScript.gm.player.GetComponent<PlayerMovement>().upLooking;
            bool faceToDown = GameManagerScript.gm.player.GetComponent<PlayerMovement>().downLooking;
            bool movimentando = Input.GetAxis("Horizontal") != 0;
            {
                //diretia esquerda 
                if (faceToRight == true && faceToUp == false && faceToDown == false) // && !diagonalDown && !diagonalUp)
                {
                    firePoint.transform.localPosition = new Vector3(0.922f, 0.42f, 0f);
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
                    pm.trocarCorpoSup(corpSupLeste);
                }
                else if (faceToRight == false && faceToUp == false && faceToDown == false) // && !diagonalDown && !diagonalUp)
                {
                    firePoint.transform.localPosition = new Vector3(-1.061f, 0.42f, 0f);
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, 180);
                    pm.trocarCorpoSup(corpSupOeste);
                }
                //diagonais superiores
                else if (faceToRight == true && ((faceToUp == true && faceToDown == false && movimentando)))// || diagonalUp))
                {
                    firePoint.transform.localPosition = new Vector3(0.625f, 1.106f, 0f);
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, 45);
                    pm.trocarCorpoSup(corpSupNordeste);
                }
                else if (faceToRight == false && ((faceToUp == true && faceToDown == false && movimentando)))// || diagonalUp))
                {
                    firePoint.transform.localPosition = new Vector3(-0.774f, 1.106f, 0f);
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, 135);
                    pm.trocarCorpoSup(corpSupNoroeste);
                }
                //diagonais inferiores
                else if (faceToRight == true && ((faceToUp == false && faceToDown == true && movimentando)))// || (diagonalDown && !diagonalUp)))
                {
                    firePoint.transform.localPosition = new Vector3(0.575f, -0.285f, 0f);
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, -45);
                    pm.trocarCorpoSup(corpSupSudeste);
                }
                else if (faceToRight == false && ((faceToUp == false && faceToDown == true && movimentando)))// || (diagonalDown && !diagonalUp)))
                {
                    firePoint.transform.localPosition = new Vector3(-0.704f, -0.285f, 0f);
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, -135);
                    pm.trocarCorpoSup(corpSupSudoeste);
                }
                //para cima parado
                else if (faceToRight == true && faceToUp == true && faceToDown == false && !movimentando)
                {
                    firePoint.transform.localPosition = new Vector3(-0.09f, 1.486f, 0f);
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, 90);
                    pm.trocarCorpoSup(corpSupDNorte);
                }
                else if (faceToRight == false && faceToUp == true && faceToDown == false && !movimentando)
                {
                    firePoint.transform.localPosition = new Vector3(-0.09f, 1.486f, 0f);
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, 90);
                    pm.trocarCorpoSup(corpSupENorte);
                }
                //para baixo parado
                else if (faceToRight == true && faceToUp == false && faceToDown == true && !movimentando)
                {
                    firePoint.transform.localPosition = new Vector3(0f, -0.535f, 0f);
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, -90);
                    pm.trocarCorpoSup(corpSupDSul);
                }
                else if (faceToRight == false && faceToUp == false && faceToDown == true && !movimentando)
                {
                    firePoint.transform.localPosition = new Vector3(-0.111f, -0.535f, 0f);
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, -90);
                    pm.trocarCorpoSup(corpSupESul);
                }
            }
            if (reloading >= cadenciado && (Input.GetKey(KeyCode.G) || Input.GetButton("Fire2")))
            {
                atira = true;
                reloading = 0;
            }
            else
            {
                reloading += Time.deltaTime;
            }

            if (atira)
            {
                //posi = new Vector3(posi.x + 0.11f * this.transform.localScale.x, posi.y - 0.01f * this.transform.localScale.x, posi.z);
                if (projectile && GameManagerScript.gm.player)
                {
                    GameObject bullete = Instantiate(projectile, firePoint.position, firePoint.rotation) as GameObject;
                    bullete.transform.parent = null;
                    bullete.GetComponent<BulletScript>().SetPoder(this.gameObject.GetComponent<StatsScript>().poder);
                }
                atira = false;
            }
        }
    }
}
