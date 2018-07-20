using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

	// sound array
	public Sound[] sounds;

	// static reference to self
	public static AudioManager instance;

	void Awake () {

		// check if an instance of this AudioManager exists
		if (instance == null)
			instance = this;
		else 
		{	// if it does exist, destroy the new object and return
			Destroy (gameObject);
			return;	// prevent from running anymore code
		}

		// this will only be useful if i have a menu or something
		DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds) 
		{
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.playOnAwake = s.playOnAwake;
		}
			
	}

	public void Play(string name, float pitch = 1f)
	{
		// whatever the fuck this syntax is
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning ("Sound: " + name + " not found.");
			return;
		}

		// adjust pitch
		s.source.pitch = pitch;
		s.source.Play ();	// AudioSource.Play()
	}
}
