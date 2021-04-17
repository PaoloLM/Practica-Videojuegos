using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private Rigidbody2D rb;

    public float movHor = 0f;
    public float velocidad = 3f;

    public bool tocarpiso = true;
    public bool tocarfrente = false;

    public LayerMask tocarpisoLayer;
    public float frentepisoRaydist = 0.25f;

    public float floorCheckY = 0.52f;
    public float frenteCheck = 0.51f;
    public float frenteDist = 0.001f;

    public int scoreGive = 50;

    private RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.obj.gamePausa)
        {
            //movHor = 0f;
            return;
        }

        //evitar caer precipicio
        //permite hacer una revision de los objetos en tiempo real
        tocarpiso = (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z),
            new Vector3(movHor, 0, 0), frentepisoRaydist, tocarpisoLayer));

        if (tocarpiso)
            movHor = movHor * -1;
        //choque con pared
        if (Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frenteCheck, tocarpisoLayer))
            movHor = movHor * -1;

        //choque con otro enemigo       
        hit = Physics2D.Raycast(new Vector3(transform.position.x + movHor * frenteCheck, transform.position.y, transform.position.z),
                new Vector3(movHor, 0, 0), frenteDist);

        if (hit != null)
            if (hit.transform != null)
                if (hit.transform.CompareTag("Enemy"))
                    movHor = movHor * -1;
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * velocidad, rb.velocity.y);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Dañar player

        if (collision.gameObject.CompareTag("Player"))
        {
            //Dañar player
            
            //Debug.Log("Daño del enemigo");
             Player.obj.getDamage();

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destruir enemigo
        if (collision.gameObject.CompareTag("Player"))
        {
            //Destruir enemigo
            AudioManager.obj.playEnemyHit();
            paraeliminar();

        }
    }
    private void paraeliminar()
    {
        FXManager.obj.showPop(transform.position);
        gameObject.SetActive(false);
    }
}
