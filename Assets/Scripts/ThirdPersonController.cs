using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;

    [Header("Camera Settings")]
    public Transform cameraTransform;

    private CharacterController characterController;
    private Vector2 inputMovement;
    private Vector3 playerVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Convert input to world space movement
        Vector3 moveDirection = new Vector3(inputMovement.x, 0, inputMovement.y);
        moveDirection = cameraTransform.forward * moveDirection.z + cameraTransform.right * moveDirection.x;
        moveDirection.y = 0f; // Keep movement on the horizontal plane

        if (moveDirection.magnitude >= 0.1f)
        {
            // Rotate player towards movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the player
            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }

        // Apply gravity
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Reset vertical velocity if grounded
        if (characterController.isGrounded)
        {
            playerVelocity.y = 0f;
        }
    }
}