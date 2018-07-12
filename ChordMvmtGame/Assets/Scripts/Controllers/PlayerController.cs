using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// get this Rigidbody
	private Rigidbody rb;

	// create an instance of IntervalSystem for chords
	public IntervalSystem isys;

	// speed and angle modifiers
	public float speed;
	public float intervalModifier;



	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		isys = new IntervalSystem ();
		isys.IntervalStart ();
	}
		

	void Update()
	{

		// ====================================
		//		BUTTON TESTS
		// ====================================


		// top frets
		if(Input.GetButton ("Green")){ Debug.Log("top green"); }
		if(Input.GetButton ("Red")){ Debug.Log("top red"); }
		if(Input.GetButton ("Yellow")){ Debug.Log("top yellow"); }
		if(Input.GetButton ("Blue")){ Debug.Log("top blue"); }
		if(Input.GetButton ("Orange")){ Debug.Log("top orange"); }

		// lower fret active
		if (Input.GetButton ("LowerFret")) { Debug.Log ("lower fret"); }


		// ===================================
		//		CALCULATE INTERVAL
		// ===================================

		// if strum is active (-1 or 1), calc interval
		float currentStrum = Input.GetAxis ("Strum");
		// Debug.Log (currentStrum);

		float rotation = 0f;

		// strum check
		rotation = isys.CheckStrum(currentStrum);

		transform.Rotate (0f, rotation * intervalModifier, 0f);
		transform.Translate (0, 0, currentStrum * speed);

	}

	// physics/player movement
	void FixedUpdate ()
	{
		// input information used to calculate physics forces

		/*
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Debug.Log (Input.GetAxis ("Horizontal"));
		float moveVertical = Input.GetAxis ("Vertical");
		bool isJumping = Input.GetButton ("Jump");
		bool isDiving = Input.GetButton ("Dive");

		// jump?
		float jumpAmt = 0.0f;
		// Input.GetKey returns true as long as the key is held
		if (isJumping) {
			jumpAmt = 2.0f;
		} else if (isDiving) {
			jumpAmt = -2.0f;
		}

		// force value
		Vector3 movement = new Vector3(moveHorizontal, jumpAmt, moveVertical);
		// force mode default through omission
		rb.AddForce(movement * speed);
		*/

		/*
		float moveVertical = Input.GetAxis ("Strum");

		Vector3 movement = new Vector3 (0f, 0f, moveVertical);
		rb.AddForce (movement * speed);
		*/
	}



}
