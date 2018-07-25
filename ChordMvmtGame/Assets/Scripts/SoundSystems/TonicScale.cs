using UnityEngine;

public class TonicScale {

	// total intervals played
	private int sumButtonInterval;
	// the type of scale; e.g. pentatonic: tonic = 5
	private int tonic;

	// --- CONSTRUCTOR --- //
	public TonicScale(int tonicNumber)
	{
		this.tonic = tonicNumber;
		this.sumButtonInterval = 0;
	}


	// === METHODS === //

	// called when playing a new note
	//	uses sumInterval -- or the composite number of +-
	//	notes that the user has played -- and converts it
	//	to the correct pitch-interval, following the tonic.
	// this int can then be used to calculate the current pitch
	public int CalculatePitchInterval(int newButtonInterval)
	{
		int pitchInterval = 0;

		// add the new interval
		sumButtonInterval += newButtonInterval;

		// divide sumInterval by the tonic, then floor (integer math)
		// then multiply by 12 -- the number of semitones in an octave
		// e.g.	3/5 = 0, 0*12 = 0, so it remains in the "home" octave
		//		6/5 = 1, 1*12 = 12, so it goes one octave above home

		int octave = ((sumButtonInterval / tonic) * 12);
		// account for negative: subjtract an extra octave
		if (sumButtonInterval < 0)
			octave -= 12;

		// modulo-dividing the total number of intervals by the tonic 
		// will return the individual interval for this note, i.e. 
		// it's place in the tonic scale
		//	e.g.	3%5 = 3 (P5)
		//	e.g.	6%5 = 1 (M2)
		int interval = (sumButtonInterval % tonic);

		// account for negative: add tonic
		// NOTE: THIS IS GONNA MAKE A WEIRD P8 INTERVAL APPEAR, where
		// normally it would modulo out to 0.  this is to account
		// for the outlier where you're at a negative unison pitch
		if (sumButtonInterval < 0)
			interval += tonic;

		pitchInterval += octave;
		pitchInterval += ConvertPentatonicHalfStep(interval);

		return pitchInterval;
	}


	public int ConvertPentatonicHalfStep(int interval)
	{
		int halfSteps = 0;

		switch(interval)
		{
		case 0:	// unison/p1, 0^
			halfSteps = 0;
			break;
		case 1:	// M2, 2^
			halfSteps = 2;
			break;
		case 2:	// M3, 4^
			halfSteps = 4;
			break;
		case 3:	// P5, 7^
			halfSteps = 7;
			break;
		case 4:	// M6, 9^
			halfSteps = 9;
			break;
		case 5: // octave/p8, 12^
			halfSteps = 12;
			break;
		default:
			halfSteps = interval;
			break;
		}

		return halfSteps;
	}

	// === CALCULATE PITCH === //
	// calculates the current pitch
	public float CalculatePitch(int interval)
	{
		int semitones = CalculatePitchInterval (interval);

		// float transpose = -4f;
		float pitch = Mathf.Pow (2, (-(float)semitones)/12f);

		return pitch;
	}
}
