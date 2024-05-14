using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLogic : MonoBehaviour
{
    [Header("Fish Properties")]
    [SerializeField] private float fishSpeed = 1.0f;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private GameObject endPosition;

    private Vector2 targetPosition = Vector2.zero;
    private bool startPositionReached = false;
    private bool endPositionReached = false;
    private bool isFishAlive = true;


    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = new Vector2(endPosition.transform.position.x,endPosition.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        

        if (isFishAlive)
        {
            MoveFish();
        }
    }

    void MoveFish()
    {
        Vector2 currentPosition = transform.position;
        Vector2 startPosVec2 = new Vector2(startPosition.transform.position.x, startPosition.transform.position.y);
        Vector2 endPosVec2 = new Vector2(endPosition.transform.position.x, endPosition.transform.position.y);


        if (currentPosition == startPosVec2)
        {
            targetPosition = endPosVec2;
        }

        else if (currentPosition == endPosVec2)
        {
            targetPosition = startPosVec2;
        }

       // Vector2 targetDirection = (currentPosition - targetPosition).normalized;
        Vector2 newPos = Vector2.MoveTowards(currentPosition, targetPosition, Time.fixedDeltaTime * fishSpeed);
        rb.MovePosition(newPos);
    }
}
