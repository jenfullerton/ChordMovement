using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

	public int width = 256;		// X
	public int height = 256;	// length - Z
	public int depth = 20;		// Y

	public float scale = 20f;
	public float offsetX = 100f;
	public float offsetY = 100f;
	public bool generateBumps = true;


	void Start ()
	{
		offsetX = Random.Range (0f, 999f);
		offsetY = Random.Range (0f, 999f);
	}


	void Update ()
	{
		Terrain terrain = GetComponent<Terrain> ();

		// generate new terrain based on current terrain data
		if (generateBumps) {
			terrain.terrainData = GenerateTerrain (terrain.terrainData);
		}
	}

	TerrainData GenerateTerrain (TerrainData terrainData)
	{
		terrainData.heightmapResolution = width + 1;
		terrainData.size = new Vector3 (width, depth, height);
		terrainData.SetHeights (0, 0, GenerateHeights ());
		return terrainData;
	}

	float[,] GenerateHeights ()
	{
		float[,] heights = new float[width, height];

		// go through 2D array and assign random perlin noise values
		for (int x = 0; x < width; x++) 
		{
			for (int y = 0; y < height; y++) 
			{
				heights [x, y] = CalculateHeight (x, y);
			}
		}

		return heights;
	}

	float CalculateHeight (int x, int y)
	{
		float xCoord = (float)x / width * scale + offsetX;
		float yCoord = (float)y / height * scale + offsetY;

		return Mathf.PerlinNoise (xCoord, yCoord);
	}

}
