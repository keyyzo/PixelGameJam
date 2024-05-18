using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishLogic : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Rubbish Properties")]
    [SerializeField] private int plasticBottleScore = 5;
    [SerializeField] private int duckToyScore = 10;
    [SerializeField] private int GreenThingScore = 20;
    [SerializeField] private int TireScore = 40;
    private int scoreToAdd;
    public RubbishType rubbishType;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(5, 6, true);

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls) 
        {
            Physics2D.IgnoreCollision(wall.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        switch (rubbishType)
        {
            case RubbishType.PlasticBottle:
                scoreToAdd = plasticBottleScore;
                break;
            case RubbishType.DuckToy:
                scoreToAdd = duckToyScore;
                break;
            case RubbishType.GreenThing:
                scoreToAdd = GreenThingScore;
                break;
            case RubbishType.Tire:
                scoreToAdd = TireScore;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore() 
    {
        return scoreToAdd;
    }
}

public enum RubbishType
{ 
    PlasticBottle,
    DuckToy,
    GreenThing,
    Tire
}
