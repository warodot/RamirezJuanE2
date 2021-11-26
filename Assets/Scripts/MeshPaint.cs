using UnityEngine;

public class MeshPaint : MonoBehaviour
{
    Color[] colors;
    Vector3[] vertices;
    Mesh mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;
        colors = mesh.colors;
        vertices = mesh.vertices;
    }

    public void PaintVertices(RaycastHit hit, Color color)
    {


        colors[mesh.triangles[hit.triangleIndex * 3]] = color;
        colors[mesh.triangles[hit.triangleIndex * 3 + 1]] = color;
        colors[mesh.triangles[hit.triangleIndex * 3 + 2]] = color;

        GetComponent<MeshFilter>().sharedMesh.SetColors(colors);

    }

    public bool CheckEqualColor(RaycastHit hit, Color color)
    {
        if (colors[mesh.triangles[hit.triangleIndex * 3]] == color)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
