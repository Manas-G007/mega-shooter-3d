using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : Interactable
{
    [SerializeField]
    private GameObject gate;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        isOpen = !isOpen;
        gate.GetComponent<Animator>().SetBool("IsOpen",isOpen);
    }
}
