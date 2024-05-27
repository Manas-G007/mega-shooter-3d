using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorCube : Interactable
{
    MeshRenderer mesh;
    public Color[] colors;
    private int colorIndex;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = Color.red;
    }

    protected override void Interact()
    {
        mesh.material.color = colors[colorIndex++ % colors.Length];
    }
}