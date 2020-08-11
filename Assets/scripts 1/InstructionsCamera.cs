using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class InstructionsCamera : MonoBehaviour
{
    bool moveCamera = false;
    float timeCounter;
    public float speed  = 3;
    int step = 1;

    public TextMeshProUGUI nextButton;
    
    // Start is called before the first frame update
    void LateUpdate()
    {
        if (moveCamera)
        {
             
            movement();
            switch(step)
            {
                case 1:
                    if (this.transform.position.x > 0)
                    {
                        stopMovement();
                    }
                    break;
                
                case 2:
                    if (this.transform.position.x > 9.21)
                    {
                        stopMovement();
                    }
                    break;

                case 3:
                    if (this.transform.position.x > 20.34)
                    {
                        stopMovement();    
                    }
                    break;

                case 4:
                    if (this.transform.position.x > 31.5)
                    {
                        stopMovement();
                        
                    }
                    break;

                case 5:
                    if (this.transform.position.x > 42.8)
                    {
                        stopMovement();
                        nextButton.text = "Play";
                    }
                    break;

                case 6:
                    SceneManager.LoadScene(2);
                    break;
            }
        }
    }

    public void moveCameraNext()
    {
        float timeCounter = 0;
        moveCamera = true;
        
    }
    void movement ()
    {
        timeCounter += Time.deltaTime;
        float xPosition = (timeCounter * speed) - 11;
        this.transform.position = new Vector3(xPosition, 1, -10);
    }
    void stopMovement()
    {
        moveCamera = false;
        step++;
    }
}
