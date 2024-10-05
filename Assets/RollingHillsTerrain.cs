using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingHillsTerrain : MonoBehaviour
{
    public int terrainWidth = 512;   // Width of the terrain
    public int terrainHeight = 512;  // Height of the terrain map
    public int terrainDepth = 500;    // Maximum height of hills

    public float scale = 50.0f;      // Scale of the Perlin noise (smaller scale = larger hills)
    public float hillFrequency = 10.0f; // Controls how frequent hills are

    public Color baseColor = Color.green; // Base color for the terrain

    void Start()
    {
        // Generate the terrain
        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            terrain.terrainData = GenerateTerrain(terrain.terrainData);

            // Apply color to the terrain using a material
            ApplyColorToTerrain(terrain);
        }
        else
        {
            Debug.LogError("Terrain component not found on the GameObject.");
        }
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = terrainWidth + 1;
        terrainData.size = new Vector3(terrainWidth, terrainDepth, terrainHeight);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[terrainWidth, terrainHeight];
        for (int x = 0; x < terrainWidth; x++)
        {
            for (int z = 0; z < terrainHeight; z++)
            {
                heights[x, z] = CalculateHeight(x, z);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int z)
    {
        float xCoord = (float)x / terrainWidth * scale * hillFrequency;
        float zCoord = (float)z / terrainHeight * scale * hillFrequency;

        // Add multiple layers of Perlin noise for rougher hills
        float height = Mathf.PerlinNoise(xCoord, zCoord) * 0.5f;
        height += Mathf.PerlinNoise(xCoord * 2.0f, zCoord * 2.0f) * 0.25f;
        height += Mathf.PerlinNoise(xCoord * 4.0f, zCoord * 4.0f) * 0.125f;
        
        return height;
    }

    void ApplyColorToTerrain(Terrain terrain)
    {
        // Create a material for the terrain
        Material terrainMaterial = new Material(Shader.Find("Standard"));

        // Create a texture with half darker and half lighter shades of green
        Texture2D terrainTexture = new Texture2D(terrainWidth, terrainHeight);
        for (int x = 0; x < terrainWidth; x++)
        {
            for (int z = 0; z < terrainHeight; z++)
            {
                float perlinValue = Mathf.PerlinNoise((float)x / terrainWidth * scale, (float)z / terrainHeight * scale);
                Color color;
                if (x < terrainWidth / 2)
                {
                    color = Color.Lerp(baseColor * 0.5f, baseColor, perlinValue); // Darker green on one half
                }
                else
                {
                    color = Color.Lerp(baseColor, baseColor * 1.5f, perlinValue); // Lighter green on the other half
                }
                terrainTexture.SetPixel(x, z, color);
            }
        }
        terrainTexture.Apply();

        // Assign the texture to the material
        terrainMaterial.mainTexture = terrainTexture;

        // Assign the material to the terrain
        terrain.materialTemplate = terrainMaterial;
    }
}