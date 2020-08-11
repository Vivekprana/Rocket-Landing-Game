using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionScript : MonoBehaviour
{


    private GameObject Rocketplayer; 
    private GameObject interactionMaster;
    

    public float sensitivity;


    
    void Start ()
    {

        //Set Player to Tag
        Rocketplayer = GameObject.FindWithTag("Player");
        interactionMaster = GameObject.FindWithTag("interactionMaster");
        
    }


    //Collision Time
    void OnCollisionEnter(Collision collision)
    {
        print(collision);
        if ((Math.Abs(collision.relativeVelocity.x) > sensitivity) || (Math.Abs(collision.relativeVelocity.y) > sensitivity) || (Math.Abs(collision.relativeVelocity.z) > sensitivity))
        {
            var rocketCrasherReg = interactionMaster.GetComponent<interactionMasterTutorial>();
            var rocketCrasherTut = interactionMaster.GetComponent<interactionMasterTutorial>();

            if (rocketCrasherReg != null)
            {
                rocketCrasherReg.rocketCrash();
            }
            else if (rocketCrasherTut != null)
            {
                rocketCrasherTut.rocketCrash();
            }
            
            
        
        }
    }
}
