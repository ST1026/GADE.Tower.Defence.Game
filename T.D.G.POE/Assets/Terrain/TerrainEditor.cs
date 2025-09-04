using UnityEngine;

public class TerrainEditor : MonoBehaviour
{
    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public int scale = 20;
    public Transform Tower;

    public float offsetX = 100f;
    public float offsetY = 100f;

    void Start()
    {
        //enables a varying terrain upon start
        offsetX = Random.Range(0, 9999f);
        offsetY = Random.Range(0, 9999f);

    }

    void Update()
    {
        //converted start to update to be able to update terrain in realtime.

        //generate terrain upon start
        Terrain terrain = GetComponent<Terrain>();
        //set terrain data equal to a newly generated terrain based on current terrain data
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        //populating terrain data
        terrainData.size = new Vector3(width, depth, height);

        //generate array of floats for different point heights
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        //2 dimensional array for different point heights
        float[,] Heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Heights[x, y] = CalculateHeight(x, y); //calculates a perlin noise value
            }
        }

        return Heights;
    }

    float CalculateHeight(int x, int y)
    {
        //cast coordinate values into floats
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        //convert coordinates into noise map values
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
