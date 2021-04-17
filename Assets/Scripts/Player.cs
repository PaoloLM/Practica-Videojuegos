using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player obj;

    public int vida = 3;
    public bool tocarpiso = false;
    public bool moviendo = false;
    public bool inmune = false;

    public float velocidad = 5f;
    public float golpefuerza = 3f;
    public float movhor;

    public float inmunetiempocontinuo = 0f;
    public float inmunetiempo = 0.5f;

    public LayerMask tocarpisoLayer; //si toca el piso o no
    public float radio = 0.3f;
    public float tocarpisoRayDist = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    private void Awake() //primera funcion
    {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //iniciamos nuestro componentes
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        spr= GetComponent<SpriteRenderer>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Game.obj.gamePausa)
        {
            movhor = 0f;
            return;
        }
        movhor = Input.GetAxisRaw("Horizontal");
        moviendo = (movhor != 0F);
        tocarpiso = Physics2D.CircleCast(transform.position, radio, Vector3.down, tocarpisoRayDist, tocarpisoLayer);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            salto();
        }
        if (inmune)
        {
            spr.enabled = !spr.enabled;
            inmunetiempo -= Time.deltaTime;
            if(inmunetiempocontinuo<=0)
            {
                inmune = false;
                spr.enabled = true;
            }
        }

        anim.SetBool("moviendo", moviendo);
        anim.SetBool("tocarpiso", tocarpiso);


        flip(movhor);//mover izqueirda derecha y escala

    }
    private void goInmune()
    {
        inmune = true;
        inmunetiempocontinuo = inmunetiempo;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movhor * velocidad, rb.velocity.y);
    }
    public void salto()
    {
        if (!tocarpiso) return;
        rb.velocity = Vector2.up * golpefuerza;
        AudioManager.obj.playJump();
    }

    private void flip(float _xValor)
    {
        // si la escala es menor a 0 
        Vector3 xscalar = transform.localScale;
        if (_xValor < 0)
            xscalar.x = Mathf.Abs(xscalar.x) * -1;
        else
        if (_xValor > 0)
            xscalar.x = Mathf.Abs(xscalar.x);

        transform.localScale = xscalar;
    }
    public void getDamage()
    {
        vida--;
        AudioManager.obj.playHit();

        UIManager.obj.updateLives();

        goInmune();

        if (vida <= 0)
        {
            FXManager.obj.showPop(transform.position);
            Game.obj.gameOver();
            //this.gameObject.SetActive(false);
        }

    }

    public void addLive()
    {
        vida++;
        if (vida > Game.obj.maxVidas)
            vida = Game.obj.maxVidas;
    }

    private void OnDestroy()
    {
        obj = null;
    }

}
