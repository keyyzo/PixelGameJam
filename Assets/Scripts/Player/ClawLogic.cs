using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ClawLogic : MonoBehaviour
{

    
    private PlayerInputHandler playerInputHandler;

    private Rigidbody2D rb;
    private bool shouldUseClaw;
    private bool clawActive;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputHandler = PlayerInputHandler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        shouldUseClaw = playerInputHandler.UseClawTriggered;

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Debug.Log("Something has been hit");

            if (collision.gameObject.CompareTag("Rubbish") && shouldUseClaw)
            {
                Destroy(collision.gameObject);
                GameManager.Instance.AddScore(2);
            }
        }
    }
}
