using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCameraController : MonoBehaviour {

    public float xSpeed = 1;
    public float ySpeed = 1;
    public float zoomSpeed = .05f;
    public float maxCameraDistance = 5;
    public float minCameraDistance = 2;
    public float maxCameraX = 10;
    public float minCameraX = -10;
    public float maxCameraY = 10;
    public float minCameraY = 0;


    // Update is called once per frame
    void Update () {
        float vertical = Input.GetAxis("Camera Y");
        float horizontal = Input.GetAxis("Camera X");
        float zoomIn = Input.GetAxis("Zoom In");
        float zoomOut = Input.GetAxis("Zoom Out");
        float newX = transform.position.x + horizontal * xSpeed * Time.deltaTime;
        float newY = transform.position.y + vertical * ySpeed * Time.deltaTime;
        if (newX > maxCameraX || newX < minCameraX)
        {
            horizontal = 0;
        }
        if (newY > maxCameraY || newY < minCameraY)
        {
            vertical = 0;
        }

        transform.Translate(new Vector2(horizontal * xSpeed * Time.deltaTime, vertical * ySpeed * Time.deltaTime));
        gameObject.GetComponent<Camera>().orthographicSize += -zoomSpeed * zoomIn + zoomSpeed * zoomOut;
        if(gameObject.GetComponent<Camera>().orthographicSize < minCameraDistance)
        {
            gameObject.GetComponent<Camera>().orthographicSize = minCameraDistance;
        }
        if (gameObject.GetComponent<Camera>().orthographicSize > maxCameraDistance)
        {
            gameObject.GetComponent<Camera>().orthographicSize = maxCameraDistance;
        }

        
    }
}
