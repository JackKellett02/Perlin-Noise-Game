using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGeneratorScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (Serialize Fields)
	[SerializeField]
	private int chunkWidth = 100;

	[SerializeField]
	private int chunkLength = 100;

	[SerializeField]
	private int scale = 5;

	[SerializeField]
	private GameObject grassBlockPrefab = null;

	[SerializeField]
	private GameObject dirtBlockPrefab = null;
	#endregion

	#region Variable Declarations
	private Vector2 chunkPosition = Vector2.zero;
	private float[,] noiseMap;
	private bool chunkGenerated = false;
	#endregion

	#region Private Functions
	// Start is called before the first frame update
	void Start() {

	}

	private void Awake() {
		chunkPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
		noiseMap = GenerateNoiseMap();
	}

	// Update is called once per frame
	void Update() {
		if (!chunkGenerated) {
			SpawnChunk();
			chunkGenerated = true;
		}
	}

	/// <summary>
	/// Generates a noise map using perlin noise and returns the said noise map.
	/// </summary>
	/// <returns></returns>
	private float[,] GenerateNoiseMap() {
		//Declare noise map variable.
		float[,] tempNoiseMap = new float[chunkWidth, chunkLength];

		//Generate values for the noise map.
		for (int z = 0; z < chunkLength; z++) {
			for (int x = 0; x < chunkWidth; x++) {
				float sampleX = (((float)x + chunkPosition.x) / chunkWidth);
				float sampleZ = (((float)z + chunkPosition.y) / chunkLength);

				float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ);
				tempNoiseMap[x, z] = perlinValue * (float)scale;
			}
		}

		//Return the noise map.
		return tempNoiseMap;
	}

	private void SpawnChunk() {
		Quaternion rotationToSpawnBlock = gameObject.transform.rotation;
		int width = noiseMap.GetLength(0);
		int length = noiseMap.GetLength(1);
		for(int z = 0; z < length; z++) {
			for(int x = 0; x < width; x++) {
				if((int)noiseMap[x,z] > 0) {
					for(int y = 1; y <= (int)noiseMap[x,z]; y++) {
						Vector3 posToSpawnBlock = new Vector3(x + gameObject.transform.position.x - (width / 2), y, z + gameObject.transform.position.z - (length / 2));
						if(y == (int)noiseMap[x, z]) {
							Instantiate(grassBlockPrefab, posToSpawnBlock, rotationToSpawnBlock, gameObject.transform);
						} else {
							Instantiate(dirtBlockPrefab, posToSpawnBlock, rotationToSpawnBlock, gameObject.transform);
						}
					}
				}
			}
		}
		gameObject.GetComponent<CombineMeshesScript>().CombineMeshes();
	}
	#endregion

	#region Public Access Functions (Getters and Setters)

	#endregion
}
