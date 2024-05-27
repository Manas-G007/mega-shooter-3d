using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float rotateX = 0f;

    public float sensitivityX = 30f;
    public float sensitivityY = 30f;

   public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // cal for look movement
        rotateX -= (mouseY * Time.deltaTime)*sensitivityY;
        rotateX = Mathf.Clamp(rotateX, -80f, 80f);

        // setting relative value
        cam.transform.localRotation = Quaternion.Euler(rotateX, 0, 0);
        // actual config change
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * sensitivityX);
    }
}
