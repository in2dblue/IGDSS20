﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    public GameManager gameManager;
    public GameObject mainCamera;
    public WorldGen worldGen;
    public float speed = 1.0f;
    public Vector3 lastPosition;

    CameraMovement mainCameraMovement;

    void Start()
    {
        mainCameraMovement = mainCamera.GetComponent<CameraMovement>();
    }
    // Update is called once per frame
    void reactOnLeftMouseButton()
    {
        int layerMask = LayerMask.GetMask("Tiles");
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, layerMask))
        {
            if(gameManager.buildMode())
            {
                gameManager.createBuilding(hit.collider.gameObject);
            }
           //Debug.Log(hit.collider.gameObject.name);
         // Debug.Log(hit.collider.gameObject.transform.position.ToString() +" " + chunkID.ToString());
        }
    }

    // Code to move the camera while holding down right mouse button
    void reactOnRightMouseButtonDown(){
        lastPosition = Input.mousePosition;
    }
    void reactOnRightMouseButtonHeldDown()
    {
        Vector3 delta = Input.mousePosition - lastPosition;
        mainCamera.transform.Translate(delta.x * speed, 0, delta.y * speed);
        lastPosition = Input.mousePosition;
    }

    void reactOnMouseWheel(float p_deltaMouseWheel)
    {
        mainCameraMovement.handleZoom(p_deltaMouseWheel);
    }

    void reactOnMouseOnEdge()
    {
        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        //mainCameraMovement.moveOnEdge(mousePosX, mousePosY);
    }

    void Update()
    {
        // Code to get the name of object on left click of mouse
        if (Input.GetMouseButtonDown(0)) // When left button of mouse is pressed
            reactOnLeftMouseButton();

        // Code to move the camera while holding down right mouse button
        if (Input.GetMouseButtonDown(1)) // When right button of mouse is pressed
            reactOnRightMouseButtonDown();

        if (Input.GetMouseButton(1)) // When right button of mouse held down then its true
            reactOnRightMouseButtonHeldDown();

        float deltaMouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if(deltaMouseWheel  != 0)
            reactOnMouseWheel(deltaMouseWheel);

        reactOnMouseOnEdge();
    }

}
