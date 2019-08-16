using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    // generates terrain from height map
    public static MeshData GenerateTerrainMesh (float[,] heightMap, float heightMultiplier, AnimationCurve p_heightCurve, int levelOfDetail) {
        AnimationCurve heightCurve = new AnimationCurve(p_heightCurve.keys);
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        int meshSimplificationIncrement = (levelOfDetail == 0) ? 1 :levelOfDetail * 2;
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;
        int vertexIndex = 0;
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;
        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);

        // loop through height map
        for (int y = 0; y < height; y += meshSimplificationIncrement) {
            for (int x = 0; x < width; x += meshSimplificationIncrement) {
                // create vertices and triangles
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);
                if (x < width - 1 && y < height - 1) {
                    meshData.AddTriangle(vertexIndex, (vertexIndex + verticesPerLine + 1), (vertexIndex + verticesPerLine));
                    meshData.AddTriangle((vertexIndex + verticesPerLine + 1), vertexIndex, (vertexIndex + 1));
                }
                vertexIndex++;
            }
        }
        // return mesh data
        return meshData;
    }
}

// Mesh data to create vertices
public class MeshData {
    public Vector3[] vertices;
    public Vector2[] uvs;
    public int[] triangles;
    int triangleIndex;

    public MeshData (int meshWidth, int meshHeight) {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    // Adding Triangles
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    // get mesh from the mesh data
    public Mesh CreateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
