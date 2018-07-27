using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// get this Rigidbody
	private Rigidbody rb;

	// debugging tools and UI
	public Text myText;
	private string intervalStr;
	public bool keyboardDebug;

	// audio manager
	public AudioManager AM;

	// IntervalSystem for chords
	private IntervalSystem isys;

	// BPM
	private BPM bpm;

	// speed and angle modifiers
	public float speed;
	public float intervalModifier;


	void Start ()
	{
		// physics rigidbody
		rb = GetComponent<Rigidbody> ();

		// interval system
		isys = new IntervalSystem (AM);
		// beats per minute system
		bpm = new BPM (4);

		intervalStr = "";
		myText.text = intervalStr;
	}
		

	void Update()
	{

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

		// i literally don't understand CS but this works now
		// DEBUGGING UI
		intervalStr = ("current interval: " + isys.GetCurrentRelativeInterval ());
		intervalStr += ("\nbpm: " + bpm.GetBpm ());
		myText.text = (intervalStr);

		// MOVEMENT
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
		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
		// force mode default through omission
		rb.AddForce(movement * speed);
*/

		/*
		float moveVertical = Input.GetAxis ("Strum");

		Vector3 movement = new Vector3 (0f, 0.1f, moveVertical);
		rb.AddForce (movement * speed);
		*/

	}



}
