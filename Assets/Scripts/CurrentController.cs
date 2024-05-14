using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CurrentController : MonoBehaviour
{
    [Header("Current Properties")]
    [Range(-1, 1), SerializeField]
    private int currentDirX = 0;
    [Range(-1, 1), SerializeField]
    private int currentDirY = 0;


    [SerializeField] private Vector2 currentDirection;
    [SerializeField] private float currentStrength = 2.0f;
    private bool isCurrentActive;
    private bool canApplyCurrent;
    private List<GameObject> objectsInCurrent = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        isCurrentActive = true;
        canApplyCurrent = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentDirection = new Vector2(currentDirX, currentDirY);

        //ApplyCurrentForce();
    }

    private void FixedUpdate()
    {
        ApplyCurrentForce();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            Debug.Log("Object has entered the current");

            if (isCurrentActive)
            { 
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(currentDirection*currentStrength,ForceMode2D.Impulse);
                Debug.Log("Force should be being applied");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Debug.Log("Object has entered the current");

            if (isCurrentActive)
            {
                canApplyCurrent = true;
                objectsInCurrent.Add(collision.gameObject);
                //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(currentDirection * currentStrength, ForceMode2D.Force);
                //Debug.Log("Force should be being applied");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < objectsInCurrent.Count; i++)
        {
            if (collision.gameObject.name == objectsInCurrent[i].name)
            {
                objectsInCurrent.Remove(collision.gameObject);
            }
        }

        if (objectsInCurrent.Count < 1)
        {
            canApplyCurrent = false;
        }
    }

    void ApplyCurrentForce()
    {
        if (canApplyCurrent && objectsInCurrent.Count > 0)
        {
            for (int i = 0; i < objectsInCurrent.Count; i++)
            {
                objectsInCurrent[i].GetComponent<Rigidbody2D>().AddForce(currentDirection * currentStrength, ForceMode2D.Force);
            }
        }
        
    }
}
