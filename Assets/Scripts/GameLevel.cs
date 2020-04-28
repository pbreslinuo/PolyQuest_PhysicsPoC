using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public GameObject leftEdge;
    public GameObject rightEdge;
    public GameObject topEdge;
    public GameObject bottomEdge;
    public GameObject edges;
    public int cubeWidth;        // set this publically where cubeWidth = cube's scale
                                 
    private Vector3 rotatePoint;
    private Vector3 rotateAxis;

    private void Start()
    {
        cubeWidth += 2;          // assume that platforms on each face extrude 2 units
    }

    public void RotateLevel(GameObject edge)
    {
        rotatePoint = edge.transform.position;

        if (edge == rightEdge)
        {
            edges.transform.Translate(cubeWidth, 0, 0);
            rotateAxis = Vector3.up;
            transform.RotateAround(rotatePoint, rotateAxis, 90);
        }
        if (edge == leftEdge)
        {
            edges.transform.Translate(-cubeWidth, 0, 0);
            rotateAxis = Vector3.up;
            transform.RotateAround(rotatePoint, rotateAxis, -90);
        }
        if (edge == topEdge)
        {
            edges.transform.Translate(0, cubeWidth, 0);
            rotateAxis = Vector3.right;
            transform.RotateAround(rotatePoint, rotateAxis, -90);
        }
        if (edge == bottomEdge)
        {
            edges.transform.Translate(0, -cubeWidth, 0);
            rotateAxis = Vector3.right;
            transform.RotateAround(rotatePoint, rotateAxis, 90);
        }
    }
}