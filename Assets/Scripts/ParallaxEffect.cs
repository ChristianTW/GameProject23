using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // Starting possition for the parallax game object
    Vector2 startingPosition;

    // Start 2 values of the parallax game object
    float startingZ;

    Vector2 camMoveSinceStart => (Vector2) cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    //can? fix it
    float clippingPlane => (can.transform.position.z + (zDistanceFromSubject > 0 ? can.farClipLane : can.nearClipLane));

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
