using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAsteroidCollision : MonoBehaviour
{
    private GameObject interactionMaster;
    private GameObject Rocketplayer; 
    private Collider RocketCollider;
    
    // Start is called before the first frame update

    void Awake()
    {
        Rigidbody asteroidRB = GetComponent<Rigidbody>();


        //Do not decrease horizontal motion of asteroid
        asteroidRB.drag = 0;


        //Get Instantiator Script Bools
        GameObject obstacle = GameObject.FindGameObjectWithTag("asteroids");

        int horizontalForce = 0;
        int ZForce = 0;

        if (obstacle.GetComponent<FallingAsteroids>().spawnXaxis)
            // Give asteroid Horizontal motion
            horizontalForce = Random.Range(-5000, 5000);

        if (obstacle.GetComponent<FallingAsteroids>().spawnZaxis)
            ZForce = Random.Range(-5000, 5000);
        asteroidRB.AddForce(horizontalForce,0,ZForce);
    }
    void Start()
    {
        //Set Player to Tag
        Rocketplayer = GameObject.FindWithTag("Player");
        interactionMaster = GameObject.FindWithTag("interactionMaster");
        RocketCollider = Rocketplayer.GetComponent<BoxCollider>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -0.5)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider Other)
    {
        print(Other);
        if (Other == RocketCollider)
            interactionMaster.GetComponent<Interactions>().rocketCrash();
            Destroy(gameObject);
    }
}
