using UnityEngine;
using System.Collections;

public class BPM {

	private float beatsPerSecond;
	private float prevTime;

	private float[] bpsDeltaTimes;
	private int index;
	private int sampleSize;

	// --- CONSTRUCTOR --- //
	public BPM(int size)
	{
		// set everything to 0
		beatsPerSecond = 0f;
		prevTime = 0f;
		index = 0;
		sampleSize = size;

		// set and fill array
		bpsDeltaTimes = new float[sampleSize];
		for (int i = 0; i < bpsDeltaTimes.Length; i++) {
			bpsDeltaTimes [i] = 0f;
		}
	}

	public void BpmAdjust()
	{
		

		// Time.time; // time at beginning of frame, will remain same throughout frame
		// get this deltaTime
		float dt = Time.time - prevTime;

		// place new dt in correct place
		bpsDeltaTimes[index] = dt;

		// calculate bps
		float sum = 0f;

		// BITCH LEARNIN C# WE OUT HERE...USING...FOREACH LOOPS
		foreach (float f in bpsDeltaTimes) {
			sum += f;
		}

		// prevents weird high numbers at the beginning
		if (sum > 0.001f)
			beatsPerSecond = ((float)bpsDeltaTimes.Length / sum);
		else
			beatsPerSecond = 0f;

		// set up for next call
		prevTime = Time.time;
		index = (index + 1) % bpsDeltaTimes.Length;

	}

	public float GetBpm()
	{
		return beatsPerSecond * 60;
	}
}
