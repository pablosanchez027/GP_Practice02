using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Movement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    Text txtScore;
    int score;

    [SerializeField]
    float moveSpeed = 0f;

    [SerializeField, Range(0.1f, 10f)]
    float maxSpeed = 0f;

    Animator anim;
    SpriteRenderer spr;

    float moveX;

    Vector2 clampedVelocity;

    [SerializeField]
    AudioClip sfx_coin;

    AudioSource auds;

    Rigidbody2D rb2d;

    [SerializeField, Range(0.1f, 15f)]
    float jumpForce = 3f;

    [SerializeField]
    Color rayColor = Color.magenta;

    [SerializeField, Range(0.0001f, 5f)]
    float rayLenght = 1f;

    [SerializeField]
    LayerMask dectectionLayer;

    [SerializeField]
    Vector2 rayPosition;

   

    void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        auds = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        moveX = Movement.Axis.x;
        
        //Movement.DeltaMovement(transform, moveSpeed);

        anim.SetFloat("MoveX", Mathf.Abs(moveX));

      

        spr.flipX = moveX < 0f ? true : moveX > 0f ? false : spr.flipX;

    }

    private void FixedUpdate()
    {
        Movement.PhysicMovement(rb2d, moveSpeed, maxSpeed);


        if (Movement.Btn_Jump && Grounding)
        {
            Movement.PhysicJumpUp(rb2d, jumpForce);
        }
    }

    bool Grounding
    {
        get => Physics2D.Raycast(transform.position + (Vector3)rayPosition,
            Vector2.down, rayLenght, dectectionLayer);
    }

    public void PlayCoinSFX()
    {
        auds.PlayOneShot(sfx_coin, 7f);
        score++;
        txtScore.text = "Score: " + score.ToString();

        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position + (Vector3)rayPosition, Vector2.down * rayLenght);
    }
}
