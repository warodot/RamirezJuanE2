using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    public TextMeshPro playerName;
    Color color;
    public LayerMask layers;
    public float speed = 10;
    bool isLoggedIn = false;

    public void SetData(string new_name, Color new_color)
    {
        playerName.SetText(new_name);
        color = new_color;
        GetComponent<Renderer>().material.color = color;

        isLoggedIn = true;
    }

    public void FixedUpdate()
    {
        if (isLoggedIn)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            GetComponent<CharacterController>().Move(new Vector3(horizontal, 0, vertical) * Time.deltaTime * speed);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity, layers))
            {
                MeshPaint meshPaint = hit.transform.GetComponent<MeshPaint>();
                meshPaint.PaintVertices(hit, color);
            }

        }

    }
}
