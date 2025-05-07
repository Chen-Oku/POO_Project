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
    private Rigidbody m_ridigdbody;

    public float WalkSpeed = 5;
    public float RotateSpeed = 5;
    public float JumpForce = 5;

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

        m_ridigdbody = GetComponent<Rigidbody>();
        //m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_lookAmt = m_lookAction.ReadValue<Vector2>();

        if (m_jumpAction.WasPressedThisFrame())
        {
            Jump();
        }
    }

    public void Jump()
    {
        m_ridigdbody.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse);
        //m_animator.SetTrigger("Jump");
    }

    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }
    private void Walking()
    {
        //m_animator.SetFloat("Speed", m_moveAmt.y);
        m_ridigdbody.MovePosition(m_ridigdbody.position + transform.forward * m_moveAmt.y * WalkSpeed * Time.deltaTime);
    }

    private void Rotating()
    {
        if (m_moveAmt.y != 0)
        {
            float rotationAmount = m_lookAmt.x * RotateSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
            m_ridigdbody.MoveRotation(m_ridigdbody.rotation * deltaRotation);
        }
            print("Rotating: " + m_lookAmt.x);
    }
}
