using UnityEngine;
using System.Collections;

public class NoteTest : MonoBehaviour {
	// stolen directly from aldonaletto
	// < https://answers.unity.com/questions/141771/whats-a-good-way-to-do-dynamically-generated-music.html?points=1 >

	//float tranpose = -4;	// transpose in semitones
		// do i know what a semitone is? NO!
		// but that's fine

	// component used by unity to play the clip
	public AudioSource musicSource;

	void Start () {
		
	}
	

	void Update () {
		/*
		float note = -1f; // ...invalid note

		if (Input.GetKeyDown("a")) note = 0f;  // C
		if (Input.GetKeyDown("s")) note = 2f;  // D
		if (Input.GetKeyDown("d")) note = 4f;  // E
		if (Input.GetKeyDown("f")) note = 5f;  // F
		if (Input.GetKeyDown("g")) note = 7f;  // G
		if (Input.GetKeyDown("h")) note = 9f;  // A
		if (Input.GetKeyDown("j")) note = 11f; // B
		if (Input.GetKeyDown("k")) note = 12f; // C
		if (Input.GetKeyDown("l")) note = 14f; // D

		// if something is pressed
		if (note >= 0)
		{ 
			
		}
		*/

		// test
		if (Input.GetKeyDown (KeyCode.Space))
			musicSource.Play ();
	}
}
