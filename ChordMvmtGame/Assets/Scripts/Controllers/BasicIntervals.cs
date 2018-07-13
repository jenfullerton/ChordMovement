using UnityEngine;
using System.Collections;

public class IntervalSystem {

	// float for determining if strum is a new strum
	private float prevStrum;
	// previous interval played (notes only, will become chords later)
	private int prevInterval;
	// boolean - forces the interval played to be 0 when starting or resetting
	private bool reset;


	public void IntervalStart()
	{
		prevStrum = 0f;
		prevInterval = 0;
		reset = true;
	}


	// ===========================
	// 		INTERVALS
	// ===========================
	public float CheckStrum(float currentStrum, BPM bpm)
	{
		float returnVal = 0f;

		// if strum button was previously at rest
		if (prevStrum == 0) {

			// if currently the strum button is being pressed down
			if (currentStrum > 0 )
			{
				// calculate interval, returned to calculate rotation
				returnVal = (float)Strum ();
				// adjust bpm
				bpm.BpmAdjust();
			}
		}

		// set prevStrum to currentStrum for next check
		prevStrum = currentStrum;

		return returnVal;
	}


	public int Strum()
	{
		int current = GetCurrentInterval ();

		// if starting game or resetting position (empty strum)
		//		set prevInterval to current
		//		forces thisInterval to be set to 0
		//			i.e., no rotation
		if (reset)
		{
			prevInterval = current;

			// if reset is active, but there are actual notes
			//	turn off reset
			if (current != 0)
				reset = false;
		}

		int thisInterval = CalculateInterval (prevInterval, current);
		prevInterval = current;
		Debug.Log ("current interval: " + thisInterval);
		return thisInterval;
	}

	int GetCurrentInterval()
	{
		int current = 0;

		// figure out which buttons are being pressed
		if(Input.GetButton ("Green"))	{ current += 1; }
		if(Input.GetButton ("Red"))		{ current += 2; }
		if(Input.GetButton ("Yellow"))	{ current += 3; }
		if(Input.GetButton ("Blue"))	{ current += 4; }
		if(Input.GetButton ("Orange"))	{ current += 5; }

		// if on lower fret, add 5
		if (Input.GetButton ("LowerFret")) { current += 5; }

		if (current == 0)
		{
			// if 0, then no buttons were held, and user is resetting
			reset = true;
		}

		return current;
	}

	int CalculateInterval(int previous, int current)
	{
		return current - previous;
	}

}
