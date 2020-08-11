using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public GameObject Rocket;
    /*** EFFECTS **/

    //Particle Systems 
    

    public ParticleSystem EastThrust;
    public ParticleSystem WestThrust;

    //AudioSources
    public ParticleSystem ThrustEffect;
    AudioSource mainThrustAudio;
    AudioSource stabilizerAudio;

    
    public GameObject Waypoint1;
    public GameObject Waypoint2;
    public GameObject Waypoint3;
    public GameObject Waypoint4;
    public GameObject Waypoint5;

    //LookPoints 
    public GameObject LookPoint1;
    public GameObject LookPoint2;
    public GameObject LookPoint3;

    public GameObject Endpoint;
    private BoxCollider EndTrigger;


    public float speed = 0.03f;
    public float rotSpeed = 10000f;


    //bools
    bool Starter = false;
    bool Objective1Complete = false;
    bool Objective2Complete = false;
    bool Objective3Complete = false;
    bool Objective4Complete = false;
    bool Objective5Complete = false;
    bool totalDone = false;


    void Start() {
        mainThrustAudio = ThrustEffect.GetComponent<AudioSource>();
        stabilizerAudio = Rocket.GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void Update()
    {
        if (Starter == false)
        {
            //StartCoroutine(endPause());
        
            Starter = true;
            if (!ThrustEffect.isPlaying)
                StartCoroutine(ThrustMain());
            //Objective1Complete = true;
        }
        if (Vector3.Distance(Rocket.transform.position, Waypoint1.transform.position) < 0.01 && Objective1Complete == false)
        {
            StartCoroutine(turnRight());
            StartCoroutine(ThrustMain());
            Objective1Complete = true;
            

        }
        if (Vector3.Distance(Rocket.transform.position, Waypoint2.transform.position) <0.01 && Objective2Complete == false)
        {
            StartCoroutine(turnLeft());
            StartCoroutine(ThrustMain());
            Objective2Complete = true;
        }
            
        if (Vector3.Distance(Rocket.transform.position, Waypoint3.transform.position) <0.01 && Objective3Complete == false)
        {
            Objective3Complete = true;
            StartCoroutine(turnRight());
            StartCoroutine(ThrustMain());
        }
        if (Vector3.Distance(Rocket.transform.position, Waypoint4.transform.position) <0.01 && Objective4Complete == false)
        {
            Objective4Complete = true;
            StartCoroutine(turnRight());

        }

        if (Vector3.Distance(Rocket.transform.position, Waypoint5.transform.position) <0.01 && Objective5Complete == false)
        {
            StartCoroutine(ThrustMain());
            Objective5Complete = true;
        }
        if (Vector3.Distance(Rocket.transform.position, Endpoint.transform.position) <0.04 && totalDone == false)
        {
            totalDone = true;
            
        }
        
        print(Vector3.Distance(Rocket.transform.position, Endpoint.transform.position));
        

        if (totalDone)
            movetoStart();
        else if (Objective5Complete)
            movetoEnd();
        else if (Objective4Complete)
            movetoObjective5();
        else if (Objective3Complete)
            movetoObjective4();
        else if (Objective2Complete)
            movetoObjective3();
        else if (Objective1Complete)
            movetoObjective2();
        
        else if (Starter)
        {   
            
            
            movetoObjective1();
            
        }
    }

    void movetoObjective1() {
        
        Rocket.transform.Translate(0,speed * Time.deltaTime, 0);
        

    }

    void movetoObjective2() {
        //Quaternion lookatWP = Quaternion.LookRotation(
        waypointCalc(Waypoint2, LookPoint1, true, 1F);
        //Rocket.transform.Translate(0,speed * Time.deltaTime, 0);

    }

    void movetoObjective3() {
        waypointCalc(Waypoint3, LookPoint2, true, 0.7F);
        //Rocket.transform.Translate(0,speed * Time.deltaTime, 0);

    }

    void movetoObjective4() {

        waypointCalc(Waypoint4, LookPoint2, false, 0.7F);
        //Rocket.transform.Translate(0,-speed * Time.deltaTime, 0);

    }

    void movetoObjective5() {

        waypointCalc(Waypoint5, LookPoint3, false, 1);
        //Rocket.transform.Translate(0,-speed * Time.deltaTime, 0);
    }
    void movetoEnd() {
        Rocket.transform.eulerAngles = new Vector3(0,-90,0);
        Rocket.transform.Translate(0, -speed * 0.7F * Time.deltaTime, 0);

    }

    void waypointCalc(GameObject Waypoint, GameObject LookPoint, bool backwards, float speedhandicap) {

        // Determine if the rocket is thrusting forward.
        float multiplier = 1;
        if (backwards)
            multiplier = -1;
        
        //Determine the angle
        Vector3 direction = (Waypoint.transform.position - Rocket.transform.position);
        Vector3 LookDirection = (LookPoint.transform.position - Rocket.transform.position);
        float angle = 0;
        angle = (Mathf.Atan2(LookDirection.z, LookDirection.y)) * Mathf.Rad2Deg;
        angle = angle;
        //Set Angle
        Quaternion targetAngle = Quaternion.Euler(0, -90 , -angle);
        //Set the angle to change graduall
    
        Rocket.transform.rotation = Quaternion.Slerp(Rocket.transform.rotation, targetAngle, Time.deltaTime * rotSpeed);
    
        //Move Rocket to next
        Rocket.transform.Translate(direction.normalized * Time.deltaTime * speed *speedhandicap, Space.World);
        


    }
    

    void movetoStart() {
        StartCoroutine(endPause());
    }

    //Thrust Effects
    IEnumerator ThrustMain()
    {
        ThrustEffect.Play();
        mainThrustAudio.Play();

        yield return new WaitForSeconds(0.5f);

        ThrustEffect.Stop();
        mainThrustAudio.Stop();
    }

    IEnumerator turnRight()
    {

        WestThrust.Play();
        stabilizerAudio.Play();

        yield return new WaitForSeconds(0.2f);

        WestThrust.Stop();
        stabilizerAudio.Stop();
    }
    IEnumerator turnLeft()
    {

        EastThrust.Play();
        stabilizerAudio.Play();

        yield return new WaitForSeconds(0.2f);

        EastThrust.Stop();
        stabilizerAudio.Stop();
    }

    IEnumerator endPause()
    {
        yield return new WaitForSeconds(1);
        

        Objective1Complete = false;
        Objective2Complete = false;
        Objective3Complete = false;
        Objective4Complete = false;
        Objective5Complete = false;
        totalDone = false;
        Starter = false;
        Rocket.transform.position = new Vector3(Waypoint1.transform.position.x, Waypoint1.transform.position.y - 0.1F, Waypoint1.transform.position.z);
        yield return new WaitForSeconds(0.2f);
        ThrustEffect.Play();
        mainThrustAudio.Play();

        yield return new WaitForSeconds(2);

        ThrustEffect.Stop();
        mainThrustAudio.Stop();

    }
}
