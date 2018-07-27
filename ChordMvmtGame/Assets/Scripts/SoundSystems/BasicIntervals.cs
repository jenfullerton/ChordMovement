using UnityEngine;
using System.Collections;

public class IntervalSystem {

	// === STRUM SETTINGS === //
	// float for determining if strum is a new strum
	private float prevStrum;
	// previous interval played (notes only, will become chords later)
	private int prevNoteButton;
	// current interval relative to last
	private int currentRelativeInterval;
	// boolean - forces the interval played to be 0 when starting or resetting
	private bool reset;

	// === OBJECT REFERENCES === //
	// AudioManager -- used to actually play sounds
	private AudioManager AM;
	// TonicScale -- used to handle maintaining a pitch
	//	in a specific scale tonic (e.g. pentatonic)
	private TonicScale TS;


	// === CONSTRUCTOR === //
	public IntervalSystem(AudioManager audioManager)
	{
		prevStrum = 0f;
		prevNoteButton = 0;
		reset = true;
		AM = audioManager;
		TS = new TonicScale (5);
	}


	// ===========================
	// 		INTERVALS
	// ===========================
	public float CheckStrum(float currentStrum, BPM bpm)
	{
		float currentInterval = 0f;

		// if strum button was previously at rest
		if (prevStrum == 0) {
			// if currently the strum button is being pressed down
			if (currentStrum > 0 )
			{
				// calculate interval, returned to calculate rotation
				currentInterval = (float)Strum ();
				// adjust bpm
				bpm.BpmAdjust();
				// play note
				float pitch = TS.CalculatePitch((int)currentInterval);
				AM.Play ("Note", pitch);
			}
		}

		// set prevStrum to currentStrum for next check
		prevStrum = currentStrum;

		return currentInterval;
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
			prevNoteButton = current;

			// if reset is active, but there are actual notes
			//	turn off reset
			if (current != 0)
				reset = false;
		}

		int thisInterval = CalculateInterval (prevNoteButton, current);
		prevNoteButton = current;
		currentRelativeInterval = thisInterval;
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

	public int GetCurrentRelativeInterval()
	{
		return currentRelativeInterval;
	}
}
