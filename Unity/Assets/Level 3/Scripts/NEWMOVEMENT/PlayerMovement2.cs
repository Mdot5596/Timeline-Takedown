using System.Collections;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private float defaultSpeed; 
    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        defaultSpeed = speed; 
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void ActivateSpeedBoost(float boostedSpeed, float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(boostedSpeed, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float boostedSpeed, float duration)
    {
        speed = boostedSpeed;
        Debug.Log("Speed boost active!");

        yield return new WaitForSeconds(duration);

        speed = defaultSpeed;
        Debug.Log("Speed boost ended.");
    }
}
