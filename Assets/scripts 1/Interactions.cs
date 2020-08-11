using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class Interactions : MonoBehaviour
{
    private GameObject Rocketplayer;
    public GameObject WinningMenu;


    public GameObject tiltControls;
    public GameObject thrustControls;

    public GameObject explosion;
    public ParticleSystem EndExplosion;

   
    public TextMeshProUGUI scoreDisplay;

    private Rigidbody rocketphysics;
    private string LevelName;
    private string ScoreFileName;
    private string LevelBeatName;

    //Control variables


    
    AudioSource audioData;
    AudioClip audio;

    private int updateCounter;

    // Start is called before the first frame update
    void Start()
    {

        Scene scene = SceneManager.GetActiveScene();
        LevelName = "Level" + (scene.buildIndex - 1).ToString();
        ScoreFileName = LevelName + "RocketsDestroyed";
    


        //turn on Rocket Counter
        updateCounter = PlayerPrefs.GetInt(ScoreFileName, 0);
        scoreDisplay.text = "ROCKETS: " + updateCounter.ToString();

        //Turn off the winners Menu
        WinningMenu.SetActive(false);


        Rocketplayer = GameObject.FindWithTag("Player");
        rocketphysics = Rocketplayer.GetComponent<Rigidbody>();


        audioData = explosion.GetComponent<AudioSource>();
        //scoreDisplay = scoreCounter.GetComponent<Text>();

        


        
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

        //Respawn


        //Coroutine
        StartCoroutine(Waiter());

        IEnumerator Waiter() 
        {

            //play explosion first?
            explosion.transform.position = Rocketplayer.transform.position;
            EndExplosion.Play(true);
            audioData.Play();

            

            //Give Rocket new life
            Rocketplayer.transform.position = new Vector3(0, 0.103F, 0.3F);
            Rocketplayer.transform.localEulerAngles = new Vector3(0, 180, 0);
            rocketphysics.constraints = RigidbodyConstraints.FreezeAll;
            
            
            
            

            

            //gameOverText.SetActive(true);
            
            updateCounter += 1;
            PlayerPrefs.SetInt(ScoreFileName, updateCounter);
            scoreDisplay.text = "ROCKETS: " + updateCounter.ToString();

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

        PlayerPrefs.SetString(LevelName, "Beat");

        WinningMenu.SetActive(true);
    }

   


}
