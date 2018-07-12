using UnityEngine;
using System.Collections;
using LibNoise.Generator;

public class PerlinTest : MonoBehaviour {



}

/*
// =======================================
//	PERLIN NOISE GENERATOR
// =======================================

public class NoiseProvider : INoiseProvider
{
	private Perlin PerlinNoiseGenerator;

	public NoiseProvider()
	{
		PerlinNoiseGenerator = new Perlin ();
	}

	public float GetValue(float x, float z)
	{
		return (float)(PerlinNoiseGenerator.GetValue (x, 0, z) / 2f) + 0.5f;
	}
}


// =======================================
//	TERRAIN GENERATION
// =======================================

// settings
public class TerrainChunkSettings
{
	public int HeightmapResolution { get; private set; }
	public int AlphamapResolution { get; private set; }

	public int Length { get; private set; }
	public int Height { get; private set; }
}

// generate chunk
public class TerrainChunk
{
	public int X { get; private set; }
	public int Z { get; private set; }

	private Terrain Terrain { get; set; }

	private TerrainChunkSettings Settings { get; set; }

	private NoiseProvider NoiseProvider { get; set; }
}

// generate Terrain game object
public void CreateTerrain()
{
	var terrainData = new TerrainData();
	terrainData.heightmapResolution = Settings.HeightmapResolution;
	terrainData.alphamapResolution = Settings.AlphamapResolution;

	var heightmap = GetHeightMap();
	// start here
}

*/

/* =========================
	OLD CONTROLLER
 ===========================
using UnityEngine;
using System.Collections;
//using LibNoise.Generator;

public class PlayerController : MonoBehaviour {

	// get this Rigidbody
	private Rigidbody rb;
	// public value allows access in component editor
	public float speed;

	// random # generator

	private Perlin PerlinNoiseGenerator;
	private float perlinResult;


	void Start ()
	{
		rb = GetComponent<Rigidbody> ();


		PerlinNoiseGenerator = new Perlin ();
		perlinResult = (float)(PerlinNoiseGenerator.GetValue(this.gameObject.transform.position) / 2.0f ) + 0.5f;
		Debug.Log ("perline result: " + perlinResult);

	}

	void FixedUpdate ()
	{
		// input information used to calculate physics forces
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		bool isJumping = Input.GetButton ("Jump");
		bool isDiving = Input.GetButton ("Dive");

		// jump?
		float jumpAmt = 0.0f;
		// Input.GetKey returns true as long as the key is held
		if (isJumping) {
			jumpAmt = 2.0f;
		} else if (isDiving) {
			jumpAmt = -2.0f;
		}

		// force value
		Vector3 movement = new Vector3(moveHorizontal, jumpAmt, moveVertical);

		// force mode default through omission
		rb.AddForce(movement * speed);
	}

	void LateUpdate()
	{
		// perlinResult = (float)(PerlinNoiseGenerator.GetValue(this.gameObject.transform.position) / 2.0f ) + 0.5f;
		// Debug.Log ("perline result: " + perlinResult);
	}

}

*/