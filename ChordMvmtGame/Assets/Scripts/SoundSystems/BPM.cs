using UnityEngine;
using System.Collections;

public class BPM {
	private float beatsPerSecond;
	private int totalBeats;

	public void BpmStart()
	{
		beatsPerSecond = 0f;
		totalBeats = 0;
	}

	public void BpmLog()
	{
		Debug.Log("BPM: " + (beatsPerSecond*60) );
	}

	public void BpmAdjust()
	{
		// update BPM
		totalBeats++;
		beatsPerSecond = totalBeats / Time.realtimeSinceStartup;

		// display new BPM
		//this.BpmLog();
	}

	public float GetBpm()
	{
		return beatsPerSecond * 60;
	}
}
