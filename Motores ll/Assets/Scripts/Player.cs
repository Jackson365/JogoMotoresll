using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public int health = 3;
    public float speed;
    public float jumpForce;
    
    public AudioSource tiro;
    public AudioSource pulo;
    
    public GameObject bow;
    public Transform Firepoint;

    private bool isJumping;
    private bool doubleJump;
    private bool isFire;
    
    private Rigidbody2D rig;
    private Animator anim;

    public Vector3 respowCheck;

    private float movement;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respowCheck = transform.position;

        GameController.instance.UpdateLives(health);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        BowFire();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //se nao precionar nada valor e 0. Precionar direita, valor maximo 1. Esquerda valor maximo -1
        movement = Input.GetAxis("Horizontal");

        //adiciona velocidade ao corpo do personagem no eixo x e y
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        //Andando para direita
        if (movement > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        //Andando para esquerda
        if (movement < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);    
            }
            
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (movement == 0 && !isJumping && !isFire)
        {
            anim.SetInteger("transition", 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                isJumping = true;
                pulo.Play();
            }
            else
            {
                if (doubleJump)
                {
                    anim.SetInteger("transition", 2);
                    rig.AddForce(new Vector2(0, jumpForce * 1), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void BowFire()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {   
            isFire = true;
            anim.SetInteger("transition", 3);
            GameObject Bow = Instantiate(bow, Firepoint.position, Firepoint.rotation);
            tiro.Play();

            if (transform.rotation.y == 0)
            {
                Bow.GetComponent<Bow>().isRight = true;
            }
            
            if (transform.rotation.y == 180)
            {
                Bow.GetComponent<Bow>().isRight = false;
            }
            
            yield return new WaitForSeconds(0.1f);
            isFire = false;
            anim.SetInteger("transition", 0);
        }
    }
    
    public void Damage(int dmg)
    {
        health -= dmg;
        GameController.instance.UpdateLives(health);
        anim.SetTrigger("hit");

        if (transform.rotation.y == 0)
        {
            transform.position += new Vector3(-0.5f, 0, 0);
        }
            
            if (transform.rotation.y == 180)
            {
                transform.position += new Vector3(0.5f, 0, 0);
            }

        if(health <= 0)
        {
            //Chamar game over
            GameController.instance.GameOver();
            Time.timeScale = 0f;
        }
    }

    public void IncreaseLife(int value)
    {
        health += value;
        GameController.instance.UpdateLives(health);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            isJumping = false;
        }
        if (coll.gameObject.layer == 9)
        {
            GameController.instance.GameOver();
        }
    }
}
