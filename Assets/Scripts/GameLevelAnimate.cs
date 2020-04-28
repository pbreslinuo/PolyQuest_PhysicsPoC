using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelAnimate : MonoBehaviour
{ // animates the shift of the edges but the rotation of the cube is still instant
    public GameObject leftEdge;
    public GameObject rightEdge;
    public GameObject topEdge;
    public GameObject bottomEdge;
    public GameObject edges;

    private float shiftSpeed;
    private bool moveIt;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 startRot;
    private Vector3 endRot;

    private Vector3 rotatePoint;
    private Vector3 rotateAxis;

    private float startTime;
    private float journeyLength;

    void Start()
    {        
        shiftSpeed = 75f;
        moveIt = false;
    }

    public void RotateLevel(GameObject edge)
    {
        moveIt = true;
        startTime = Time.time;
        startPos = edges.transform.position;
        rotatePoint = edge.transform.position;
        rotateAxis = Vector3.up;
        transform.RotateAround(rotatePoint, rotateAxis, 90);

        // startRot = transform.eulerAngles;

        /*
        Vector3 offset = transform.position - edge.transform.position;
        foreach (Transform child in transform)
            child.transform.position += offset;
        transform.position = edge.transform.position;
        */

        if (edge == rightEdge)
        {
            endPos = startPos + new Vector3(32, 0, 0);
            // startRot + new Vector3(0, 90, 0);
        }
        if (edge == leftEdge)
        {
            endPos = startPos + new Vector3(-32, 0, 0);
            // endRot = startRot + new Vector3(0, -90, 0);
        }
        if (edge == topEdge)
        {
            endPos = startPos + new Vector3(0, 32, 0);
            // endRot = startRot + new Vector3(-90, 0, 0);
        }
        if (edge == bottomEdge)
        {
            endPos = startPos + new Vector3(0, -32, 0);
            // endRot = startRot + new Vector3(90, 0, 0);
        }
        journeyLength = Vector3.Distance(endPos, startPos);
    }

    void Update()
    {
        if (moveIt)
        {
            float distCovered = (Time.time - startTime) * shiftSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            edges.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            // transform.eulerAngles = Vector3.Lerp(startRot, endRot, fractionOfJourney);
        }
    }
}