using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerWalk : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_jumpAction;
    
    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;
    private Rigidbody m_ridigbody;

    public float WalkSpeed = 5;
    public float RotateSpeed = 5;
    public float JumpForce = 8; 
    public float FallMultiplier = 2.5f; 
    public float GroundCheckDistance = 1.1f;

    public Transform cameraTransform; 

    private int jumpCount = 0;
    private int maxJumps = 1; // 1 salto normal + 1 doble salto

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        m_moveAction = inputActions.FindAction("Player/Move");
        m_lookAction = inputActions.FindAction("Player/Look");
        m_jumpAction = inputActions.FindAction("Player/Jump");

        m_ridigbody = GetComponent<Rigidbody>();
        m_ridigbody.freezeRotation = false;
        m_ridigbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_lookAmt = m_lookAction.ReadValue<Vector2>();

        if (IsGrounded())
        {
            jumpCount = 0; // para resetea el contador de saltos al tocar el suelo
        }

        if (m_jumpAction.WasPressedThisFrame())
        {
            if (jumpCount < maxJumps)
            {
                Jump();
                jumpCount++;
            }
        }
    }

    public void Jump()
    {
        Vector3 velocity = m_ridigbody.linearVelocity;
        velocity.y = 0;
        m_ridigbody.linearVelocity = velocity;
        m_ridigbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        //m_animator.SetTrigger("Jump");
    }

    private void FixedUpdate()
    {
        Walking();
        Rotating();
        ApplyBetterFalling();
    }

    private void ApplyBetterFalling()
    {
        if (m_ridigbody.linearVelocity.y < 0)
        {
            m_ridigbody.AddForce(Vector3.up * Physics.gravity.y * (FallMultiplier - 1) * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    private bool IsGrounded()
    {
        // Raycast simple hacia abajo para comprobar si está en el suelo
        return Physics.Raycast(transform.position, Vector3.down, GroundCheckDistance);
    }

    private void Walking()
    {
        //m_animator.SetFloat("Speed", m_moveAmt.y);
        if (cameraTransform != null)
        {
            // Movimiento relativo a la cámara
            Vector3 moveDirection = cameraTransform.forward * m_moveAmt.y + cameraTransform.right * m_moveAmt.x;
            moveDirection.y = 0;
            moveDirection.Normalize();
            m_ridigbody.MovePosition(m_ridigbody.position + moveDirection * WalkSpeed * Time.deltaTime);
        }
        else
        {
            m_ridigbody.MovePosition(m_ridigbody.position + transform.forward * m_moveAmt.y * WalkSpeed * Time.deltaTime);
        }
    }

    private void Rotating()
    {
        if (cameraTransform != null && (m_moveAmt.x != 0 || m_moveAmt.y != 0))
        {
            // Calcula la dirección real de movimiento según el input y la cámara
            Vector3 moveDirection = cameraTransform.forward * m_moveAmt.y + cameraTransform.right * m_moveAmt.x;
            moveDirection.y = 0;
            if (moveDirection.sqrMagnitude > 0.001f)
            {
                moveDirection.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                float angle = Quaternion.Angle(m_ridigbody.rotation, targetRotation);
                float angleThreshold = 1f;

                if (angle > angleThreshold)
                {
                    m_ridigbody.MoveRotation(Quaternion.Slerp(m_ridigbody.rotation, targetRotation, RotateSpeed * Time.deltaTime));
                }
            }
        }
    }
}
