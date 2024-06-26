using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{

    [Header("Movement Speeds")]
    [SerializeField] private float generalSubMoveSpeed = 10.0f;
    [SerializeField] private float horizontalAccelerationSpeed = 0.1f;
    [SerializeField] private float verticalAccelerationSpeed = 0.05f;
    [SerializeField] private float maxSubHorizontalSpeed = 10.0f;
    [SerializeField] private float maxSubVerticalSpeed = 5.0f;

    [Header("Water Paramenters")]
    [SerializeField] private float waterResistance = 2.0f;
    [SerializeField] private float waterGravValue = 0.05f; // Will change the gravity scale value on the rigidbody

    [Header("Claw Parameters")]
    [SerializeField] private float clawMaxDistace = 3.0f;
    [SerializeField] private float clawMinDistace = 0.3f;
    [SerializeField] private float clawRetractSpeed = 0.01f;
    [SerializeField] private float clawDropSpeed = 0.01f;
    [SerializeField] private float clawGravValue = 1.0f;
    private GameObject clawObject;

    // Player Components
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private PlayerInputHandler playerInputHandler;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem particleSystem;

    // Specific Movement Components
    private float horizontalInput;
    private float verticalInput;

    // Specific Claw Components
    private bool shouldRetractClaw;
    private bool shouldDropClaw;
    private bool shouldUseClaw;
    private bool clawActive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        playerInputHandler = PlayerInputHandler.Instance;
        rb.gravityScale = waterGravValue;
        //clawObject.transform.position = new Vector2(transform.position.x,transform.position.y-0.7f);
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        clawObject = transform.GetChild(0).gameObject;
        clawActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameState.ReadyLevel)
        {
            rb.gravityScale = 0.0f;
            clawObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;

        }

        if (GameManager.Instance.state == GameState.LevelInProgress)
        {
            rb.gravityScale = waterGravValue;
            clawObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            InputCallHandler();
        }
    }

        

    private void FixedUpdate()
    {
        if (GameManager.Instance.state == GameState.LevelInProgress)
        {
            ApplyMovement();
            DisplayClawLine();
            PositionClaw();
        }

            
    }

    void ApplyMovement()
    {
        
        float horizontalSpeedIncrement = horizontalInput * horizontalAccelerationSpeed;
        float verticalSpeedIncrement = verticalInput * verticalAccelerationSpeed;

        float newHorizontalSpeed = Mathf.Clamp(rb.velocity.x + horizontalSpeedIncrement, -maxSubHorizontalSpeed, maxSubHorizontalSpeed);
        float newVerticalSpeed = Mathf.Clamp(rb.velocity.y + verticalSpeedIncrement, -maxSubVerticalSpeed, maxSubVerticalSpeed);

        rb.velocity = new Vector2(newHorizontalSpeed, newVerticalSpeed);
        

       
    }

    void DisplayClawLine()
    {
        Vector3 offSet = new Vector3(0.04f, -0.25f,0.0f);

        lineRenderer.SetPosition(0, transform.position + offSet);
        lineRenderer.SetPosition(1,transform.GetChild(0).transform.position);
    }

    void PositionClaw()
    {
        if (clawObject)
        {
            //Debug.Log("Called");

            if (clawObject.TryGetComponent<SpringJoint2D>(out SpringJoint2D spring))
            {
                if (shouldDropClaw && spring.distance <= clawMaxDistace)
                {
                    spring.distance += clawDropSpeed;
                }

                if (shouldRetractClaw && spring.distance >= clawMinDistace)
                { 
                    spring.distance -= clawRetractSpeed;
                }
            }

            else
            {
                Debug.Log("Spring joint not detected");
                return;
            }
        }

        else
        {
            Debug.Log("Claw object not detected");
        }
    }

    private void FlipSprite(float horizontalMovement)
    {
        var particleShape = particleSystem.shape;

        if (horizontalMovement < 0)
        {
           // transform.localScale = new Vector3(-1, 1, 1);
            spriteRenderer.flipX = true;
            particleShape.position = new Vector3(0.48f, -0.21f, 1);
            
        }

        else if (horizontalMovement > 0)
        {
           // transform.localScale = new Vector3(1, 1, 1);
            spriteRenderer.flipX = false;
            particleShape.position = new Vector3(-0.48f, -0.21f, 1);
        }
    }

    private void InputCallHandler()
    {
        horizontalInput = playerInputHandler.MoveInput.x;
        verticalInput = playerInputHandler.MoveInput.y;
        shouldRetractClaw = playerInputHandler.RetractClawTriggered;
        shouldDropClaw = playerInputHandler.DropClawTriggered;
        shouldUseClaw = playerInputHandler.UseClawTriggered;

        if (horizontalInput != 0)
        {
            FlipSprite(horizontalInput);
        }
    }

}
