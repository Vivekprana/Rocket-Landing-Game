using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraMovement : MonoBehaviour
{
    private float timeCounter = 0;
    public float circleRadius = 5;
    public GameObject Rocket;


    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3 (circleRadius, 0, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        timeCounter += Time.deltaTime/5;

        float x = Mathf.Cos(timeCounter);
        float y = Mathf.Sin(timeCounter);
        float z = 0;

        transform.position = new Vector3 (x * circleRadius, y * circleRadius,z);

        if (Rocket != null)
        {
            
            Rocket.transform.rotation = Quaternion.Euler(0, 180, -timeCounter * Mathf.Rad2Deg);
        }
    }
}
