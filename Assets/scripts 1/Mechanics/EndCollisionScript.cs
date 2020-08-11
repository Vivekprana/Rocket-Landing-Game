using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EndCollisionScript : MonoBehaviour
{


    private GameObject Rocketplayer; 
    private Rigidbody rb;
    private GameObject interactionMaster;
    public Collider triggerCollider;

    private Interactions scriptMain;
    private interactionMasterTutorial scriptTutorial;
    
    
    

    public float sensitivity;
    

    // Winning Check Variables
    private float stayCheck;


    
    void Start ()
    {

        //Set Player to Tag
        Rocketplayer = GameObject.FindWithTag("Player");
        rb = Rocketplayer.GetComponent<Rigidbody>();
        interactionMaster = GameObject.FindWithTag("interactionMaster");

        //if(interactionMaster.GetComponent<Interactions>() != null) {
        scriptMain = interactionMaster.GetComponent<Interactions>();
        scriptTutorial = interactionMaster.GetComponent<interactionMasterTutorial>();
        
        
        
    }


    //Collision Time
    private void OnTriggerStay(Collider other)
    {
        if ((Mathf.Abs(triggerCollider.bounds.center.x - other.gameObject.GetComponent<Collider>().bounds.center.x) <= 
        triggerCollider.bounds.extents.x - other.gameObject.GetComponent<Collider>().bounds.extents.x) && (Mathf.Abs(triggerCollider.bounds.center.z - other.gameObject.GetComponent<Collider>().bounds.center.z) <= 
        triggerCollider.bounds.extents.z - other.gameObject.GetComponent<Collider>().bounds.extents.z))
        {
            // Make sure object is in correct rotation position.
            print("rotation.x" + other.gameObject.transform.rotation.x * Mathf.Rad2Deg);
            print("rotation.z" + other.gameObject.transform.rotation.z * Mathf.Rad2Deg);

            if (Mathf.Abs(other.gameObject.transform.rotation.x) < 0.1F && Mathf.Abs(other.gameObject.transform.rotation.z) < 0.1F)
            {
                if (scriptMain != null)
                    scriptMain.winner();
                else if (scriptTutorial != null)
                    scriptTutorial.winner();
            }
            else if ((Mathf.Abs(other.gameObject.transform.rotation.x) * Mathf.Rad2Deg) > (45)|| (Mathf.Abs(other.gameObject.transform.rotation.z)* Mathf.Rad2Deg) > 45)
            {
                if (scriptMain != null)
                    scriptMain.rocketCrash();
                else if (scriptTutorial != null)
                    scriptTutorial.rocketCrash();
            }
        } 
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if ((Math.Abs(collision.relativeVelocity.x) > sensitivity) || (Math.Abs(collision.relativeVelocity.y) > sensitivity) || (Math.Abs(collision.relativeVelocity.z) > sensitivity))
        {

            if (scriptMain != null)
                scriptMain.rocketCrash();
            else if (scriptTutorial != null)
                scriptTutorial.rocketCrash();
            
        }
      
    }

}