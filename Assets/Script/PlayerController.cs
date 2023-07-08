using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer sRenderer;

    //Capture the components
    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    //Enable the playerController
    private void OnEnable()
    {
        playerControls.Enable();
    }

    //Use Update to handle input
    private void Update()
    {
        PlayerInput();
    }

    //Use FixedUpdate to handle physics
    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    //Get player Input
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        //Determine the direction of movement based on input.
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    //Handle physics of player movement
    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    //Adjust player facing 
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float differenceValue = mousePos.x - transform.position.x;

        //Flip sprite based on position of mouse to position of sprite.
        if (differenceValue > 0)
        {
            sRenderer.flipX = false;
        }
        else if (differenceValue < 0)
        {
            sRenderer.flipX = true;
        }
    }
}
