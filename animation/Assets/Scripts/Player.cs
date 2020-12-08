﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpforce;
    private float timeAtack;
    public float startTimeAttack;
    private bool isGrounded;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite; 

    // Start is called before the first frame update
    void Start()
    {
     rigidbody = GetComponent<Rigidbody2D>();
     animator = GetComponent<Animator>();
     sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(Input.GetKey(KeyCode.LeftArrow)){
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);

            animator.SetBool("IsWalking", true);

            sprite.flipX = true;
       }
       else
       {
           rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

             animator.SetBool("IsWalking", false);       
       }

       if(Input.GetKey(KeyCode.RightArrow)){
           
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            animator.SetBool("IsWalking", true);

            sprite.flipX = false;
       }

       if (Input.GetKeyDown(KeyCode.UpArrow))
       {
           rigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
           isGrounded = false;
           animator.SetBool("IsJumping", true);
       }

       if (timeAtack <= 0)
       {
           if (Input.GetKeyDown(KeyCode.Z))
           {
             animator.SetTrigger("IsAttacking");
             timeAtack = startTimeAttack;
           }
       }
       else
           {
               timeAtack -= Time.deltaTime;
               animator.SetTrigger("IsAttacking");
           }
    }
    void OnCollisionEnter2D(Collision2D coll) {
      //Persongaem tovando o chão
      if (coll.gameObject.layer == 8)
      {
          isGrounded = true;
          animator.SetBool("IsJumping", false);
      }
    }
}
