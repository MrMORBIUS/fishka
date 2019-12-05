using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveAnglerFish : MonoBehaviour
{
    public float jumpSpeed;
    Transform fish;
    Rigidbody2D rb;
    public float distanceAttack;
    bool attack;
    bool backAttack;
    Vector3 saveTarget;
    Vector3 finishPoint;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fish = GameObject.FindWithTag("fish").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (attack) { Attack(); }
        else { Move(); }
    }

    private void Attack()
    {
        if (backAttack == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, saveTarget, jumpSpeed);
            if (transform.position == saveTarget) backAttack = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPoint, jumpSpeed);
            if (transform.position == finishPoint) attack = false;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(-GManager.speedObject,0);
        if(Vector2.Distance(transform.position, fish.position) < distanceAttack && backAttack == false)
        {
            attack = true;
            saveTarget = fish.position;
            finishPoint = new Vector3(transform.position.x - ((transform.position.x - saveTarget.x) * 2), transform.position.y,transform.position.z);
            rb.velocity = new Vector2(0, 0);
        }
    }

    
}
