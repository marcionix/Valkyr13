using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform groundCheck;
    public bool isGrounded;
    public float jumpForce;
    Rigidbody2D rb;

    public bool rightLooking = true;
    public bool upLooking = false;
    public bool downLooking = false;

    public Sprite standSupRight;
    public Sprite standSupLeft;
    public Sprite standInfRight;
    public Sprite standInfLeft;
    public GameObject corpoSup;
    public GameObject corpoInf;
    public GameObject standSup;
    public GameObject standInf;
    public GameObject movInf;
    public Sprite jumpInfDir;
    public Sprite jumpInfEsq;

    private string state;
    private string prevState;
    private StatsScript ss;

    // Use this for initialization
    void Start()
    {
        state = "parado";
        ss = this.gameObject.GetComponent<StatsScript>();
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ss.vivo)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (ss.vivo)
        {
            bool ficarParado = false;
            if (Input.GetButton("RB") || Input.GetButton("LB") || Input.GetKey(KeyCode.H))
            {
                ficarParado = true;
            }
            isGrounded = Physics2D.OverlapPoint(groundCheck.position, GameManagerScript.gm.whatIsGround) || Physics2D.OverlapPoint(groundCheck.position, GameManagerScript.gm.whatIsEnemies);
            prevState = state;
            float x = Input.GetAxis("Horizontal");
            if (x > 0 && !rightLooking)
            {
                rightLooking = true;
                Flip(x);
                prevState = "virei";
            }
            else if (x < 0 && rightLooking)
            {
                rightLooking = false;
                Flip(x);
                prevState = "virei";
            }
            if (ficarParado || !ss.vivo)
            {
                x = 0;
            }
            float y = Input.GetAxis("Vertical");
            if (y > 0)
            {
                upLooking = true;
                downLooking = false;
            }
            else if (y < 0)
            {
                upLooking = false;
                downLooking = true;
            }
            else
            {
                upLooking = false;
                downLooking = false;
            }

            if (x != 0)
            {
                state = "movendo";
            }
            else if (x == 0)
            {
                state = "parado";
            }
            if (!isGrounded)
            {
                state = "aereo";//pulando/caindo
            }

            if (state == "parado" && prevState != "parado")
            {
                if (rightLooking)
                    trocarCorpoInf(standInf, standInfRight);
                else
                    trocarCorpoInf(standInf, standInfLeft);
            }
            if (state == "movendo" && prevState != "movendo")
            {
                if (rightLooking)
                    trocarCorpoInf(movInf, null, new Vector3(1f, 1f, 1f));
                else
                    trocarCorpoInf(movInf, null, new Vector3(-1f, 1f, 1f));
            }
            if (state == "aereo" && prevState != "aereo")
            {
                if (rightLooking)
                    trocarCorpoInf(standInf, jumpInfDir);
                else
                    trocarCorpoInf(standInf, jumpInfEsq);
            }
            //Debug.Log(state + "/" + prevState);
            Vector3 move = new Vector3(x * ss.velocidade, rb.velocity.y, 0f);
            if (ficarParado || !ss.vivo)
            {
                move = new Vector3(0f, rb.velocity.y, 0f);
            }
            if(ss.vivo)
                rb.velocity = move;
        }
        else
        {
            corpoSup.SetActive(false);
            corpoInf.SetActive(false);
        }
    }

    public bool GetFaceToRight()
    {
        return rightLooking;
    }

    public void trocarCorpoSup(Sprite s)
    {
        if (s != null) {
            SpriteRenderer spriteRS = corpoSup.GetComponent<SpriteRenderer>();
            spriteRS.sprite = s;
        }
    }

    public void trocarCorpoInf(GameObject go, Sprite s, Vector3 scale)
    {
        if (go != null)
        {
            GameObject gamOb = Instantiate(go, corpoInf.transform.position, corpoInf.transform.rotation, this.gameObject.transform);
            Destroy(corpoInf);
            corpoInf = gamOb;
            corpoInf.transform.localScale = scale;
        }
        if (s != null)
        {
            SpriteRenderer spriteRS = corpoInf.GetComponent<SpriteRenderer>();
            spriteRS.sprite = s;
        }
    }

    public void trocarCorpoInf(GameObject go, Sprite s)
    {
        trocarCorpoInf(go, s, new Vector3(1f,1f,1f));
    }

    public void trocarCorpoInf(GameObject go)
    {
        trocarCorpoInf(go, null, new Vector3(1f, 1f, 1f));
    }

    void Flip(float x)
    {
        Vector3 scale = corpoInf.transform.localScale;
        //scale.x *= -1;
        //corpoInf.transform.localScale = scale;
        if (rightLooking)
            trocarCorpoSup(standSupRight);
        else
            trocarCorpoSup(standSupLeft);
    }
}
