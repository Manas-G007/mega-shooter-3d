using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputMotor : MonoBehaviour
{
    private CharacterController controller;
    /* this is capsule */
    private Vector3 playerVelocity;
    private float speed = 5f;
    private bool isGround;
    private float gravity = -9.8f;
    private float jumpHeight = 3f;
    private bool sprinting=false,crouching=false,lerpCrouch=false;
    private float crouchTimer = 0f;

    /* bullet values */
    public Transform gunBarrel;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = controller.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        // it for gravity
        playerVelocity.y += gravity*Time.deltaTime;
        if (isGround && playerVelocity.y < 0) playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGround)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8;
        else
            speed = 5;
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Shoot()
    {
        GameObject bullet = GameObject.Instantiate(
           Resources.Load("Prefab/PlayerBullet") as GameObject,
           gunBarrel.transform.position,
           transform.rotation);
        Vector3 shootDir = (gunBarrel.transform.position - transform.position).normalized;
        Quaternion deviation = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up);
        Vector3 finalShootDir = deviation * shootDir;

        finalShootDir += Vector3.down * 0.11f; 

        finalShootDir.Normalize();

        bullet.GetComponent<Rigidbody>().velocity = finalShootDir * 40;
    }
}

