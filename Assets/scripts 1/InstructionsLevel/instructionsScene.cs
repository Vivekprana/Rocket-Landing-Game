using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionsScene : MonoBehaviour
{

    //Objects
    public GameObject rocket;
    private Rigidbody rocketPhysics;
    public GameObject platform1Red;
    public GameObject platform1Green;
    

    //Rocket1 Effects////IMPORTANT//
    
    //Thrust Visual Effects
    public ParticleSystem NorthThrust1;
    public ParticleSystem SouthThrust1;
    public ParticleSystem EastThrust1;
    public ParticleSystem WestThrust1;

    //Particle Systems
    public ParticleSystem ThrustEffect1;
    AudioSource mainThrustAudio1;
    AudioSource stabilizerAudio1;

    //Rocket1 Effects////IMPORTANT//
    
    //Thrust Visual Effects
    public ParticleSystem NorthThrust2;
    public ParticleSystem SouthThrust2;
    public ParticleSystem EastThrust2;
    public ParticleSystem WestThrust2;

    //Particle Systems
    public ParticleSystem ThrustEffect2;
    public ParticleSystem BigExplosion;
    AudioSource mainThrustAudio2;
    AudioSource stabilizerAudio2;

    //Instructions
    public GameObject UIFindRocketAssist;
    public GameObject instructions1;
    public GameObject instructions2;
    public GameObject instructions2a;
    public GameObject instructions2b;
    public GameObject instructions3;
    public GameObject instructions3a;
    public GameObject instructions3b;
    public GameObject instructions4;
    public GameObject instructions5;
    public GameObject instructions6;
    public GameObject instructions7;
    public GameObject instructions8;
    public GameObject instructions9;

    //camera
    public Camera camera;
    

    //axis
    public GameObject axisX;
    public GameObject axisZ;

    //Controls
    
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject forwardsButton;
    public GameObject backwardsButton;
    public GameObject ThrustButton;
    public GameObject RocketControls;

    //Buttons
    public GameObject NextButton1;
    public GameObject NextButton2;

    //Parts
    public GameObject Part1Objects;
    public GameObject Part2Objects;
    public GameObject AutopilotFolder;

  


    //Bool-Ints
    private int movedCloser = 0;
    private int rotated = 0;
    private int rotatedX = 0;
    private int thrusted = 0;
    bool Setfalling = false;
    bool falling = false;
    bool Rockeplayer = false;

    //Checks for turns
    bool checkFor45Left = false;
    bool checkFor45Right = false;
    bool checkFor45Forward = false;
    bool checkFor45Backward = false;
    bool step1Completed = false;
    bool step4Acomplete = false;

    //Checks for horizontal/depth button presses
    bool horizontalpressed = false;
    bool verticalpressed = false;



    /* Part 2 part of Script */

    public GameObject RocketP2;
    private Rigidbody RocketP2Physics;
    private Vector3 upVector;

    
    // Start is called before the first frame update
    void Start()
    {
        //parts
        AutopilotFolder.SetActive(false);
        Part2Objects.SetActive(false);
        rocketPhysics = rocket.GetComponent<Rigidbody>();
        rocketPhysics.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        RocketP2Physics = RocketP2.GetComponent<Rigidbody>();
        

        //objects that are turned off
        platform1Red.SetActive(false);
        platform1Green.SetActive(false);

        axisZ.SetActive(false);
        axisX.SetActive(false);
        
        instructions1.SetActive(true);
        instructions2.SetActive(false);
        instructions2a.SetActive(false);
        instructions2b.SetActive(false);
        instructions3.SetActive(false);
        instructions3a.SetActive(false);
        instructions3b.SetActive(false);
        instructions4.SetActive(false);
        instructions5.SetActive(false);
        instructions6.SetActive(false);
        instructions7.SetActive(false);
        instructions8.SetActive(false);
        //instructions9.SetActive(false);

        //Buttons
        NextButton1.SetActive(false);
        NextButton2.SetActive(false);


        //Rocket Controls
        RocketControls.SetActive(false);

        

        
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rockrot = rocket.transform.eulerAngles;

        if (camera.transform.position.z > 0.3)
        {
            movedCloser++;
            if (movedCloser == 1)
            {
                StepOneAccomplished();
            }
        }

        //Step1a
        if (movedCloser >= 1 && checkFor45Right == false && horizontalpressed == true)
        {
            
            checkFor45Right = true;
            
            Step1A();
        }

        print(rockrot.z);
        //Step1b
        if (checkFor45Left == false && rockrot.z >= 45 && rockrot.z <=180 && checkFor45Right == true)
        {
            checkFor45Left = true;
            Step1B();

        }

        //Step 2
        if (rockrot.z <=  315 && rockrot.z >=180 && checkFor45Left == true)
        {
            
            rotated += 1;
            if (rotated == 1)
            {
                StepTwoAccomplised();
            }
        }

        //Step2a
        if (checkFor45Forward == false && rotated >= 1 && verticalpressed)
        {
            checkFor45Forward = true;
            Step2A();
        }
        //Step2b
        if (checkFor45Backward == false && rockrot.x <= 315 && rockrot.x >= 180 && checkFor45Forward == true)
        {
            checkFor45Backward = true;
            Step2B();
        }

        //Step 3
        if ( rockrot.x >= 45 && rockrot.x <= 180 && checkFor45Backward == true)
        {
            rotatedX ++;
            if (rotatedX == 1)
                StepThreeAccomplished();
        }  

        //Step 4
        if  (rocketPhysics.velocity.y > 0.1 && rotatedX >= 1)
        {
            thrusted++;
            if (thrusted == 1)
                StepFourAccomplished();

        }

        //step 4a
        if (thrusted >= 1 && camera.transform.eulerAngles.y >= 45 && camera.transform.position.y <= 180 && step4Acomplete == false)
        {
            Step4a();
            step4Acomplete = true;
        }

        //Rocket Crash Demo 
        if (Setfalling)
        {
            RocketCrashExampleSet();
        }
        if (falling)
        {
            RocketCrashExample();
        }

       

    }
    void StepOneAccomplished()
    {

        //Controls
        UIFindRocketAssist.SetActive(false);
        RocketControls.SetActive(true);
        ThrustButton.SetActive(false);
        
        instructions1.SetActive(false);
        instructions2.SetActive(true);
        axisZ.SetActive(true);
        rocketPhysics.constraints &= ~RigidbodyConstraints.FreezeRotationZ;

    }
    //Step 1A
    void Step1A() {
        instructions2.SetActive(false);
        instructions2a.SetActive(true); 
    }
    void Step1B() {
        instructions2a.SetActive(false);
        instructions2b.SetActive(true); 
    }

    void StepTwoAccomplised()
    {
        instructions2b.SetActive(false);
        instructions3.SetActive(true);
        axisZ.SetActive(false);
        axisX.SetActive(true);

        rocketPhysics.constraints = RigidbodyConstraints.FreezeRotation| RigidbodyConstraints.FreezePosition;
        rocket.transform.eulerAngles = new Vector3(0,180,0);

        rocketPhysics.constraints &= ~RigidbodyConstraints.FreezeRotationX;
    }

    //Step 2a
    void Step2A() {
        instructions3.SetActive(false);
        instructions3a.SetActive(true); 
    }
    void Step2B() {
        instructions3a.SetActive(false);
        instructions3b.SetActive(true); 
    }
    
    
    void StepThreeAccomplished()
    {
        instructions3b.SetActive(false);
        instructions4.SetActive(true);
        platform1Red.SetActive(true);
        axisX.SetActive(false);
        rocket.transform.rotation = Quaternion.Euler(0,180,0);
        rocketPhysics.constraints = RigidbodyConstraints.FreezeRotation;
        ThrustButton.SetActive(true);
    }

    void StepFourAccomplished()
    {
        AutopilotFolder.SetActive(true);
        instructions4.SetActive(false);
        instructions5.SetActive(true);
        instructions6.SetActive(true);
        //StartCoroutine(rocketLanding());
        
    }
    //Step 4A
    void Step4a() {

        NextButton1.SetActive(true);
        //NextButton2.SetActive(false);
        RocketControls.SetActive(false);
        Part1Objects.SetActive(false);
    }

    public void StepFiveAccomplished()
    {
        AutopilotFolder.SetActive(false);
        Part2Objects.SetActive(true);
        instructions5.SetActive(false);
        instructions6.SetActive(false);
        NextButton1.SetActive(false);
        

        
        RocketCrashExampleSet();
        NextButton2.SetActive(true);
        instructions7.SetActive(true);

    }

    public void StepSixAccomplished()
    {
        
        
        instructions7.SetActive(false);
        instructions8.SetActive(true);
        NextButton2.SetActive(false);
        Part1Objects.SetActive(true);
        falling = false;
        Setfalling = false;
        platform1Green.SetActive(true);
        rocketPhysics.constraints = RigidbodyConstraints.None;

        RocketControls.SetActive(true);


    }

    

    

    // Use this to demo rocket crash

    //Set RocketCrash 
    void RocketCrashExampleSet()
    {
        RocketP2.transform.position = new Vector3(0.5f, 1, 0.3254641f);
        falling = true;
        Setfalling = false;
    }
    void RocketCrashExample()
    {   

        if (RocketP2Physics.velocity.y >= -0.01)
        {
            //play explosion
            BigExplosion.Play(true);
            (BigExplosion.GetComponent<AudioSource>()).Play();
            
            Setfalling = true;
        }
    }


    //Button Press For Steps 2 and 3
    public void horizontalpress()
    {
        horizontalpressed = true;
    }
    public void verticalpress()
    {
        if (rotated >= 1)
        {
            verticalpressed = true;
        }
    }

}
