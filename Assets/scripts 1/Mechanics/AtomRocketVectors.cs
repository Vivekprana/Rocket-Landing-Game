using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System;

public class AtomRocketVectors : MonoBehaviour
{

    //PhysicsBodies
    private Rigidbody rb;
    private bool ignite;


    //Stability Variables
    private float xFactorZ;
    private float yFactorZ;
    private float yFactorX;
    private float zFactorX;
    private float xFactorY;
    private float zFactorY;


    //Power Amount
    public float power;
    
    //Thrust Visual Effects
    public ParticleSystem NorthThrust;
    public ParticleSystem SouthThrust;
    public ParticleSystem EastThrust;
    public ParticleSystem WestThrust;

    

    //Difficulty 
    public float sensitivity;
    public float torqueSensitivity;
    public float angleDragFactor;

    //Particle Systems
    public ParticleSystem ThrustEffect;
    AudioSource mainThrustAudio;
    AudioSource stabilizerAudio;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainThrustAudio = ThrustEffect.GetComponent<AudioSource>();
        stabilizerAudio = GetComponent<AudioSource>();


        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
  
        float x = 0;
        float z = 0;
        if (CrossPlatformInputManager.GetAxis("z1") == 1){
            z = 1;
            stabilizerAudioPlay();
            
        }
        if (CrossPlatformInputManager.GetAxis("z2") == 1){
            z = -1;
            stabilizerAudioPlay();
        }
        if (CrossPlatformInputManager.GetAxis("x1") == 1){
            x = 1;
            stabilizerAudioPlay();
            
        }
        if (CrossPlatformInputManager.GetAxis("x2") == 1){
            x = -1;
            stabilizerAudioPlay();
        }

        if (CrossPlatformInputManager.GetAxis("z1") != 1 && CrossPlatformInputManager.GetAxis("z2") != 1 && CrossPlatformInputManager.GetAxis ("x1") != 1 && CrossPlatformInputManager.GetAxis("x2") != 1)
        {
            stabilizerAudioStop();
        }


        /*
        //print(Input.GetAxis("Launcher"));

        // Get the angels

        Quaternion curPos = transform.rotation;

        // Get the Angles
        Vector3 currentAngle = curPos.eulerAngles;

        float zAngle = currentAngle.z * 0.01745329F;
        xFactorZ = Convert.ToSingle(Math.Sin(zAngle));
        yFactorZ = Convert.ToSingle(Math.Cos(zAngle));

        float yAngle = currentAngle.y * 0.01745329F;
        xFactorY = Convert.ToSingle(Math.Cos(yAngle));
        zFactorY = Convert.ToSingle(Math.Sin(yAngle)); 

        float xAngle = currentAngle.x * 0.01745329F;
        zFactorX = Convert.ToSingle(Math.Sin(xAngle));
        yFactorX = Convert.ToSingle(Math.Cos(xAngle));

        
        
        */

        if (CrossPlatformInputManager.GetAxis("Fire1") == 1 )
        {
            Vector3 movement = this.transform.up;
            //new Vector3(-1 * xFactorZ * yFactorX, 1 * yFactorZ * yFactorX, 1 * zFactorX * yFactorX);

            rb.AddForce(movement * power * Time.deltaTime);
            ThrustEffect.Emit(1);
            //Play thrust sound
            if (mainThrustAudio.isPlaying != true)
            {
                mainThrustAudio.Play();
            }
        
        }

        if (CrossPlatformInputManager.GetAxis("Fire1") != 1)
        {
            if (mainThrustAudio.isPlaying)
            {
                mainThrustAudio.Stop();
            }
        }


        if (x > 0)
        {
            EastThrust.Emit(1);
        }
        if (x < 0)
        {
            WestThrust.Emit(1);
        }
        if (z > 0)
        {
            SouthThrust.Emit(1);
        }
        if (z < 0)
        {
            NorthThrust.Emit(1);
        }

        //Rotate Rocket
        rb.AddTorque(-z/torqueSensitivity * Time.deltaTime, 0 , x/torqueSensitivity * Time.deltaTime);

        
        
        rb.angularDrag = angleDragFactor;
        



        
        
    }

    //Define Functions
    void stabilizerAudioPlay()
    {
        if (stabilizerAudio.isPlaying == false)
        {
            stabilizerAudio.Play();
        }
    }
    void stabilizerAudioStop()
    {
        if (stabilizerAudio.isPlaying == true)
        {
            stabilizerAudio.Stop();
        }
    }
    

}
