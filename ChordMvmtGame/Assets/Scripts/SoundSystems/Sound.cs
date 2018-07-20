using UnityEngine.Audio;
using UnityEngine;

// [this is an attribute]

// for custom classes that you want to appear in the editor,
//	you must make them Serializabale
//	Do I know wtf that means? NO! but Brackeys said to do it
[System.Serializable]
public class Sound
{
	public string name;
	public AudioClip clip;

	[Range(0f, 1f)]	// puts slider in editor
	public float volume;
	// range maybe
	public float pitch;

	// hide in editor because it will be populated automatically
	[HideInInspector]
	public AudioSource source;

	public bool playOnAwake;
}
