using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// get this Rigidbody
	private Rigidbody rb;

	// audio manager
	public AudioManager AM;

	// IntervalSystem for chords
	private IntervalSystem isys;

	// BPM
	private BPM bpm;

	// speed and angle modifiers
	public float speed;
	public float intervalModifier;

	public bool keyboardDebug;



	void Start ()
	{
		// physics rigidbody
		rb = GetComponent<Rigidbody> ();

		// interval system
		isys = new IntervalSystem ();
		isys.IntervalStart (AM);
		// beats per minute system
		bpm = new BPM ();
		bpm.BpmStart ();

		/*
		 * // TEST TONIC SCALE
		TonicScale ts = new TonicScale(5);

		Debug.Log ("ts stage " + 0 + " = " + ts.CalculatePitchInterval (0));
		 * 
		for (int i = 1; i <= 5; i++) {
			Debug.Log ("ts stage " + i + " = " + ts.CalculatePitchInterval (1));
		}

		for (int i = 5; i >= 0; i--) {
			Debug.Log ("ts stage " + i + " = " + ts.CalculatePitchInterval (-1));
		}
		*/

		/*
		Debug.Log ("ts stage " + (-3) + " = " + ts.CalculatePitchInterval (-3));
		Debug.Log ("ts stage " + (-3) + " = " + ts.CalculatePitchInterval (-3));
		Debug.Log ("ts stage " + (-4) + " = " + ts.CalculatePitchInterval (-4));
		Debug.Log ("ts stage " + (-5) + " = " + ts.CalculatePitchInterval (-5));
		Debug.Log ("ts stage " + (+1) + " = " + ts.CalculatePitchInterval (1));
		*/
	}
		

	void Update()
	{

		// ====================================
		//		BUTTON TESTS
		// ====================================

		/*
		// top frets
		if(Input.GetButton ("Green")){ Debug.Log("top green"); }
		if(Input.GetButton ("Red")){ Debug.Log("top red"); }
		if(Input.GetButton ("Yellow")){ Debug.Log("top yellow"); }
		if(Input.GetButton ("Blue")){ Debug.Log("top blue"); }
		if(Input.GetButton ("Orange")){ Debug.Log("top orange"); }

		// lower fret active
		if (Input.GetButton ("LowerFret")) { Debug.Log ("lower fret"); }
		*/


		// ===================================
		//		CALCULATE INTERVAL
		// ===================================

		// if strum is active (-1 or 1), calc interval
		float currentStrum = Input.GetAxis ("Strum");
		float debugStrum = Input.GetAxis ("Vertical");

		if(keyboardDebug)
		{
			if (currentStrum == 0) {
				if (debugStrum <= -0.2 || debugStrum >= 0.2)
					currentStrum = debugStrum;
			}
		}


		float rotation = 0f;

		// strum check
		rotation = isys.CheckStrum(currentStrum, bpm);

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


		/*
		float moveVertical = Input.GetAxis ("Strum");

		Vector3 movement = new Vector3 (0f, 0f, moveVertical);
		rb.AddForce (movement * speed);
		*/
	}



}
