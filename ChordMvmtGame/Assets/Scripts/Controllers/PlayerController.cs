using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	// === OBJECTS === //
	// get this Rigidbody
	private Rigidbody rb;
	// IntervalSystem for chords
	private IntervalSystem isys;
	// BPM
	private BPM bpm;

	// === DEBUGGING TOOLS === //
	[Header("Debugging")]
	// debugging tools and UI
	public Text myText;
	public bool keyboardDebug;
	private string intervalStr;

	// === AUDIO SETTINGS === //
	[Header("Audio")]
	// audio manager
	public AudioManager AM;

	// === PHYSICS / "HOVER" SETTINGS === //
	[Header("Physics")]
	public float speed;
	public float intervalModifier;

	// --------
	// NEW SHIT
	// ========

	float m_deadZone = 0.1f;

	public float m_hoverForce = 9.0f;
	public float baseHoverHeight = 2.0f; 
	private float m_hoverHeight = 2.0f;

	public GameObject[] m_hoverPoints;
		// forward momentum
	public float m_forwardAcl = 100.0f;
	public float m_backwardAcl = 25.0f;
	float m_currThrust = 0.0f;
		// angular momentum
	public float m_turnStrength = 10f;
	float m_currTurn = 0.0f;
		// air brakes?
	// nah
		// B I T M A S K
	int m_layerMask;



	// ======================================= //
	// ================ METHODS ============== //
	// ======================================= //

	void Start ()
	{
		// === OBJECTS === //
		// physics rigidbody
		rb = GetComponent<Rigidbody> ();
		// interval system
		isys = new IntervalSystem (AM);
		// beats per minute system
		bpm = new BPM (4);

		// === DEBUGGING === //
		intervalStr = "";
		myText.text = intervalStr;

		// ----------------------------------------- //
		//	THAT NEW SHIT
		// ----------------------------------------- //
		m_layerMask = 1 << LayerMask.NameToLayer("Player");
		m_layerMask = ~m_layerMask;
	}
		
	// ======================================= //
	// 		DEBUG GIZMOS FOR FORCES
	// ======================================= //
	void OnDrawGizmos()
	{
		//  Hover Force
		RaycastHit hit;
		for (int i = 0; i < m_hoverPoints.Length; i++)
		{
			var hoverPoint = m_hoverPoints [i];
			if (Physics.Raycast(hoverPoint.transform.position, 
				-Vector3.up, out hit,
				m_hoverHeight, 
				m_layerMask))
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(hoverPoint.transform.position, hit.point);
				Gizmos.DrawSphere(hit.point, 0.5f);
			} else
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(hoverPoint.transform.position, 
					hoverPoint.transform.position - Vector3.up * m_hoverHeight);
			}
		}
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

		// FORWARD
		m_currThrust = 0.0f;
		// get acceleration -> currentStrum (currentStrum = aclAxis)
		if (currentStrum > m_deadZone)
			m_currThrust = currentStrum * m_forwardAcl;
		else if (currentStrum < -m_deadZone)
			m_currThrust = currentStrum * m_backwardAcl;

		// TURNING
		m_currTurn = 0.0f;
		float turnAxis = rotation * intervalModifier;
		if (Mathf.Abs (rotation) > m_deadZone)
			m_currTurn = turnAxis;

	}

	// NOT EVEN GOD CAN HELP ME
	void FixedUpdate ()
	{
		// change hoverheight cause i'm a bitch
		// m_hoverHeight = (baseHoverHeight + 0.5f*Mathf.Sin(Time.time));


		// -------------------------------- //
		//		THAT GOOD GOOD HOVER
		// -------------------------------- //
		RaycastHit hit;
		for (int i = 0; i < m_hoverPoints.Length; i++)
		{
			var hoverPoint = m_hoverPoints [i];
			if (Physics.Raycast(hoverPoint.transform.position, 
				-Vector3.up, out hit,
				m_hoverHeight,
				m_layerMask))
				rb.AddForceAtPosition(Vector3.up 
					* m_hoverForce
					* (1.0f - (hit.distance / m_hoverHeight)), 
					hoverPoint.transform.position);
			else
			{
				if (transform.position.y > hoverPoint.transform.position.y)
					rb.AddForceAtPosition(
						hoverPoint.transform.up * m_hoverForce,
						hoverPoint.transform.position);
				else
					rb.AddForceAtPosition(
						hoverPoint.transform.up * -m_hoverForce,
						hoverPoint.transform.position);
			}
		}




		// -------------------------------- //
		//			actual movement
		// -------------------------------- //

		// forward
		if (Mathf.Abs (m_currThrust) > 0)
			rb.AddForce (transform.forward * m_currThrust);

		// turn
		if (m_currTurn > 0)
		{
			rb.AddRelativeTorque (Vector3.up * m_currTurn * m_turnStrength);
		} else if (m_currTurn < 0)
		{
			rb.AddRelativeTorque (Vector3.up * m_currTurn * m_turnStrength);
		}


	}



}
