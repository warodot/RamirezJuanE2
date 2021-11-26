using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBot : MonoBehaviour
{
    public PlaneGenerator plane;

    int trianglesPainted;
    public LayerMask layers;
    public string botName;
    Color color;
    NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        GenerateNewDestination();

        color = new Color(Random.value,Random.value,Random.value);
        GetComponent<Renderer>().material.color = color;
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity,layers))
        {
            MeshPaint meshPaint = hit.transform.GetComponent<MeshPaint>();
            meshPaint.PaintVertices(hit, color);
        }



        if (nav.remainingDistance < 0.1f)
        {
            GenerateNewDestination();
        }
    }

    void GenerateNewDestination()
    {
        Vector3 newDestination = new Vector3(Random.Range(0,plane.tileAmountX), 0f, Random.Range(0, plane.tileAmountY));
        for (int i = 0; i < 10; i++)
        {
            if (Physics.Raycast(newDestination, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity, layers))
            {
                if (!hit.transform.GetComponent<MeshPaint>().CheckEqualColor(hit,color))
                {
                    break;
                }
                else
                {
                    newDestination = new Vector3(Random.Range(0, plane.tileAmountX), 0f, Random.Range(0, plane.tileAmountY));
                }
            }
        }

        nav.SetDestination(newDestination);
    }
}
