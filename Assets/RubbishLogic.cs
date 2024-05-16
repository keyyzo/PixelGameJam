using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishLogic : MonoBehaviour
{
    // Start is called before the first frame update



    void Start()
    {
        Physics2D.IgnoreLayerCollision(5, 6, true);

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls) 
        {
            Physics2D.IgnoreCollision(wall.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
