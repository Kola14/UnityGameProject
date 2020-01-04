using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
    public Animator animator;

	public float runSpeed = 40f;

	private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    private bool alive = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update () {
        alive = controller.IsAlive;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (!alive)
        {
            StartCoroutine(Wait());
        }

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
        }

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
            animator.SetBool("IsCrouching", true);
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
            animator.SetBool("IsCrouching", false);
        }
        if (controller.isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }
	}

	void FixedUpdate ()
	{
        
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}

    private IEnumerator Wait()
    {
        animator.SetBool("IsAlive", false);
        controller.enabled = false;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator GameOver()
    {
        yield return StartCoroutine(Wait());
    }
}
