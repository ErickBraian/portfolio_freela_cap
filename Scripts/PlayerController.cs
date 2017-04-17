using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float runSpeed;
	public float walkSpeed;
	bool running;

	Rigidbody myRB;
	Animator myAnim;

	bool facingRight;

	//pulo
	bool grounded = false;
	Collider[] groundCollisions;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;
	//fim pulo

	void Start () {
		myRB = GetComponent<Rigidbody> ();
		myAnim = GetComponent<Animator> ();
		facingRight = true;
	}

	void Update () {
	
	}

	//checa a cada frame
	void FixedUpdate () {
		running = false;

		//checar colisão com o chão
		groundCollisions = Physics.OverlapSphere (groundCheck.position, groundCheckRadius, groundLayer);
		if (groundCollisions.Length > 0)
			grounded = true;
		else
			grounded = false;

		//"Grounded" é uma variável do animator, grounded é uma variável do código V
		myAnim.SetBool("Grounded", grounded);

		if(grounded && Input.GetAxis("Jump")>0){
			grounded = false;
			myAnim.SetBool ("Grounded", grounded);
			myRB.AddForce(new Vector3(0,jumpHeight,0));
		}
		//fim checar colisão com o chão

		//movimentação
		float move = Input.GetAxis ("Horizontal");
		myAnim.SetFloat ("Speed", Mathf.Abs (move));

		//Walks when shooting
		float sneaking = Input.GetAxisRaw ("Fire3");
		myAnim.SetFloat ("Sneaking", sneaking);

		float firing = Input.GetAxisRaw("Fire1");
		myAnim.SetFloat ("Shooting", firing);

		if (Input.GetMouseButtonDown (2))
			if (PlayerSpecialPower.instance.specialPowerCount > 0)
				PlayerSpecialPower.instance.DecreasePower ();

		if ((sneaking > 0 || firing > 0) && grounded)
		{
			myRB.velocity = new Vector3 (move * walkSpeed, myRB.velocity.y, 0);
		}
		else
		{
			myRB.velocity = new Vector3 (move * runSpeed, myRB.velocity.y, 0);
			if (Mathf.Abs(move)>0) running = true;
		}

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
		//fim movimentação


	}
	
	//personagem vira pra direção que está andando
	void Flip (){
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.z *= -1;
		transform.localScale = theScale;
	}
	//fim personagem vira pra direção que está andando

	//The character knows the direction he's facing so it can shoot a bullet
	public float GetFacing(){
		if (facingRight)
		{
			return 1;
		}
		else
		{
			return -1;
		}
	}

	public bool GetRunning(){
		return (running);
	}

}
