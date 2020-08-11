using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class interactionMasterTutorial : MonoBehaviour
{
    private GameObject Rocketplayer;
    

    public GameObject tiltControls;
    public GameObject thrustControls;

    public GameObject explosion;
    public ParticleSystem EndExplosion;

    public GameObject tutorialCompleteMenu;

   
    

    private Rigidbody rocketphysics;
    private string LevelName;
    

    private GameObject mainMenuScriptHolder;
    private mainMenu mainMenuScript;
    

    //Control variables


    
    AudioSource audioData;
    AudioClip audio;

    private int updateCounter;

    // Start is called before the first frame update
    void Start()
    {

        Scene scene = SceneManager.GetActiveScene();
        


        Rocketplayer = GameObject.FindWithTag("Player");
        rocketphysics = Rocketplayer.GetComponent<Rigidbody>();


        audioData = explosion.GetComponent<AudioSource>();
        tutorialCompleteMenu.SetActive(false);
        

        
        


        
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy Object if it goes out of bounds
        if (Rocketplayer != null)
        {
            if (Rocketplayer.transform.position.x < - 1.5 || Rocketplayer.transform.position.x > 1.5 || 
                Rocketplayer.transform.position.y < - 1.5 || Rocketplayer.transform.position.y > 1.5 ||
                Rocketplayer.transform.position.z < - 1.5 || Rocketplayer.transform.position.z > 1.5)
            {
                rocketCrash();
            }
        }
       
    }

    public void rocketCrash()

    {   


        //Coroutine
        StartCoroutine(Waiter());

        IEnumerator Waiter() 
        {

            //play explosion first?
            explosion.transform.position = Rocketplayer.transform.position;
            EndExplosion.Play(true);
            audioData.Play();

            

            //Give Rocket new life
            Rocketplayer.transform.localPosition = new Vector3(-0.0939400f,-0.00674993f, -0.1039641f);
            Rocketplayer.transform.localEulerAngles = new Vector3(0, 180, 0);
            rocketphysics.constraints = RigidbodyConstraints.FreezeAll;
            
            
            //Wait for 2 seconds
            yield return new WaitForSecondsRealtime(1);

            
            rocketphysics.constraints = RigidbodyConstraints.None;
            rocketphysics.constraints = RigidbodyConstraints.FreezeRotationY;
        }
        StopCoroutine(Waiter());

        
        
    }

    public void winner()
    {
      
        
        if (thrustControls.activeSelf == true)
        {
            thrustControls.SetActive(false);
        }
        if (tiltControls.activeSelf == true)
        {
            tiltControls.SetActive(false);
        }

        //Get MainMenuScript
        tutorialCompleteMenu.SetActive(true);
    }

   


}
