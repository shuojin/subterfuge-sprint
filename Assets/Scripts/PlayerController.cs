using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float moveSpeedStore;
	public float speedMultiplier;

	public float speedInterval;
	private float speedIntervalStore;
	private float speedMilestoneCounter;
	private float speedMilestoneCounterStore;

	public float jumpForce;
	public float jumpTime;
	private float jumpTimeCounter;

	private bool stoppedJumping;
	//private bool canDoubleJump;

	private Rigidbody2D myRigidbody;

	public bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;

	//private Collider2D myCollider;
	private Animator myAnimator;

	public GameManager gameManager;

	public AudioSource jumpSound;
	public AudioSource deathSound;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();

		//myCollider = GetComponent<Collider2D> ();

		myAnimator = GetComponent<Animator> ();

		jumpTimeCounter = jumpTime;

		speedMilestoneCounter = speedInterval;

		moveSpeedStore = moveSpeed;
		speedMilestoneCounterStore = speedMilestoneCounter;
		speedIntervalStore = speedInterval;

		stoppedJumping = true;
	}
	
	// Update is called once per frame
	void Update () {
		//grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		if(transform.position.x > speedMilestoneCounter)
		{
			speedMilestoneCounter += speedInterval;
			speedInterval = speedInterval * speedMultiplier;
			moveSpeed *= speedMultiplier;
		}
		myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

		if(Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()))
		{
			if (grounded) 
			{
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				stoppedJumping = false;
				jumpSound.Play ();
			}
		}
			
		if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) || !stoppedJumping) 
		{
			if (jumpTimeCounter > 0) 
			{
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}
		if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0)) 
		{
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}
		if (grounded) 
		{
			jumpTimeCounter = jumpTime;
		}

		myAnimator.SetFloat ("Speed", myRigidbody.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "killbox") 
		{			
			gameManager.Restart ();
			moveSpeed = moveSpeedStore;
			speedMilestoneCounter = speedMilestoneCounterStore;
			speedInterval = speedIntervalStore;
			deathSound.Play ();
		}
	}
}
