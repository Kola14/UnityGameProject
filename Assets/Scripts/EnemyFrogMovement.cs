using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyFrogMovement : MonoBehaviour
{
    GameObject player;
    GameObject[] frogs;
    public EnemyController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;

    void Start()
    {
        controller = GetComponent<EnemyController2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        frogs = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }

        StartCoroutine(Wait());
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        horizontalMove = 0;
    }
    private IEnumerator Wait()
    {
        foreach (GameObject frog in frogs)
        {
            if (controller.isGrounded && jump == false)
            {
                horizontalMove = 0;
                yield return new WaitForSeconds(.5f);
                if ((Math.Abs(player.transform.position.x - frog.transform.position.x) < 10) &&
                    (Math.Abs(player.transform.position.y - frog.transform.position.y) < 15))
                {
                    if (player.transform.position.x > frog.transform.position.x)
                    {
                        horizontalMove = 5 * runSpeed;
                    }
                    else
                    {
                        horizontalMove = -5 * runSpeed;
                    }
                    jump = true;
                }
                else
                {
                    horizontalMove = 0f;
                    jump = false;
                }
            }
        }
    }
}
