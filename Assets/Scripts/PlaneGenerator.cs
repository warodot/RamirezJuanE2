using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class PlaneGenerator : MonoBehaviour
{
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    public float tileSize = 1f;
    public int tileAmountX = 30;
    public int tileAmountY = 30;
    Mesh mesh;


    // Update is called once per frame
    void Awake()
    {

        GenerateVertices();
        GenerateTriangles();
        GenerateMesh();


    }


    void GenerateVertices()
    {
        vertices.Clear();
        for (int i = 0; i <= tileAmountY; i++)
        {
            for (int j = 0; j <= tileAmountX; j++)
            {
                vertices.Add(new Vector3(j * tileSize, 0, i * tileSize));
            }
        }

    }

    void GenerateTriangles()
    {
        triangles.Clear();

        int[] tmpTriangles = new int[tileAmountX*tileAmountY*6];
        for (int ti = 0, vi = 0, y = 0; y < tileAmountY; y++, vi++)
        {
            for (int x = 0; x < tileAmountX; x++, ti+=6, vi++)
            {
                tmpTriangles[ti] = vi;
                tmpTriangles[ti + 3] = tmpTriangles[ti + 2] = vi+1;
                tmpTriangles[ti + 4] = tmpTriangles[ti + 1] = vi + tileAmountX + 1;
                tmpTriangles[ti + 5] = vi + tileAmountX + 2;


                /*
                triangles.Add(x + (y * tileAmountX));
                triangles.Add(x + (y * tileAmountX) + tileAmountX+1);
                triangles.Add(x + (y * tileAmountX) + 1);
                */
            }
        }

        triangles.AddRange(tmpTriangles);

    }
    void GenerateMesh()
    {
        mesh = new Mesh();
        mesh.SetVertices(vertices);


        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();

        Color[] vertexColors = new Color[vertices.Count];

        for (int i = 0; i < vertexColors.Length; i++)
        {
            vertexColors[i] = Color.white;
        }
        mesh.SetColors(vertexColors);

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<NavMeshSurface>().BuildNavMesh();
        

    }
}
