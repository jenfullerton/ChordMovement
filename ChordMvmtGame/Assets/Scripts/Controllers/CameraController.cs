using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// public
	public GameObject target;	// target - attach in editor
	public float damping = 1f;	// linear interpolation for rotation

	// private
	private Vector3 offset;

	void Start () {
		offset = target.transform.position - transform.position;
	}


	// procedural generation, follow cameras, etc -> use LateUpdate
	// lateUpdate runs every frame just like Update, but does so after
	void LateUpdate ()
	{
		// LERP
		float currentAngle = transform.eulerAngles.y;	// angle now
		float desiredAngle = target.transform.eulerAngles.y;	// angle to LERP to
		float angle = Mathf.LerpAngle (currentAngle, desiredAngle, Time.deltaTime * damping);

		// ROTATE and TRANSFORM
		Quaternion rotation = Quaternion.Euler (0, angle, 0);	// get rotation
		transform.position = target.transform.position - (rotation * offset);

		// SET
		transform.LookAt (target.transform);
	}
}
