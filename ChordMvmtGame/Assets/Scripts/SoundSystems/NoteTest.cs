using UnityEngine;
using System.Collections;

public class NoteTest : MonoBehaviour {
	// stolen directly from aldonaletto
	// < https://answers.unity.com/questions/141771/whats-a-good-way-to-do-dynamically-generated-music.html?points=1 >

	// component used by unity to play the clip
	// basically a reference to itsel but okay
	// i know how to program amirite fellas
	public AudioSource musicSource;

	float tranpose = -4;	// transpose in semitones
	// i know exactly what the fuck i'm doing

	void Update () {
		
		float note = -1f;

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
			// I. DON'T. KNOW. HOW. THIS. WORKS. OR. WHAT. IT.
			//	MEANS. BUT. IT. WORKS.   B I T C H !
			musicSource.pitch = Mathf.Pow (2, (note+tranpose)/12f);
			musicSource.Play ();
		}

		/*
		// test
		if (Input.GetKeyDown (KeyCode.Space))
			musicSource.Play ();
		*/
	}
}
