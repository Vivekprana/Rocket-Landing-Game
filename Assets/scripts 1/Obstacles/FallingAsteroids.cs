using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAsteroids : MonoBehaviour
{
    public GameObject Asteroid;
    public float timeDifficulty;
    private float asteroidStartPositionX;
    private float asteroidStartPositionZ;

    public bool spawnXaxis = true;
    public bool spawnZaxis = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke ("createAsteroids", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createAsteroids ()
    {
        if (spawnXaxis)
        {
            float numerator = Random.Range(0, 100);
            asteroidStartPositionX =  numerator/100;
        }
        else 
        {
            asteroidStartPositionX = 0;

        }
        if (spawnZaxis)
        {
            float numeratorZ = Random.Range(0, 100);
            asteroidStartPositionZ =  numeratorZ/100;
        }
        else {

            asteroidStartPositionZ = 0.27f;
        }

        Instantiate(Asteroid, new Vector3(asteroidStartPositionX,1, asteroidStartPositionZ), Quaternion.identity);
        Invoke("createAsteroids", timeDifficulty);
    }
}
