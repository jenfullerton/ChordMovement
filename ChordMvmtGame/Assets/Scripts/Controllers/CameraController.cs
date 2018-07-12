using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// edit in console to attach Player to the GameObject reference
	//	seen here
	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	/*
	void Update () {
		transform.position = player.transform.position + offset;
	}
	*/

	// procedural generation, follow cameras, etc -> use LateUpdate
	// lateUpdate runs every frame just like Update, but does so after
	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}
}
