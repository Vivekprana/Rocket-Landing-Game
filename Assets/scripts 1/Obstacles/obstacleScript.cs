using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleScript : MonoBehaviour
{
    private GameObject interactionMaster;
    private GameObject Rocketplayer; 
    private Collider RocketCollider;
    // Start is called before the first frame update
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
        
    }

    void OnTriggerEnter(Collider Other)
    {
        print(Other);
        if (Other == RocketCollider)
            interactionMaster.GetComponent<Interactions>().rocketCrash();
    }
}
