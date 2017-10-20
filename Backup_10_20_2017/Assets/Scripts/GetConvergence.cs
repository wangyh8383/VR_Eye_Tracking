using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetConvergence : MonoBehaviour {

    const float defaultSize = 1.0f;
    private Vector3 oldPosition;
    private Vector3 newPosition;
	// Use this for initialization
	void Start () {
        oldPosition = transform.position;
        InvokeRepeating("getConvergence", 0.0f, 0.02f);
        transform.localScale = new Vector3(defaultSize, defaultSize, defaultSize);
    }

    // Update is called once per frame
    void Update () {
        //getConvergence();
    }

    void getConvergence() {
        FoveInterface.EyeRays rays = FoveInterface.GetEyeRays();
        var leftOrigin = rays.left.origin;
        var rightOrigin = rays.right.origin;
        var leftPoint = rays.left.GetPoint(0.1f);
        var rightPoint = rays.right.GetPoint(0.1f);

        var dist1 = Vector3.Distance(leftPoint, rightPoint);
        var dist2 = Vector3.Distance(leftOrigin, rightOrigin);

        var distance = (float) 0.1 * dist2 / (dist2 - dist1); // dist between convergence and origin

        if (distance > 3.5f) { distance = 3.5f; }

        //transform.position = rays.left.GetPoint(distance);
        MoveTowardsTarget(rays.left.GetPoint(distance));
        transform.localScale = new Vector3(defaultSize * distance / 5, defaultSize * distance / 5, defaultSize * distance / 5);
    }

    void MoveTowardsTarget(Vector3 targetPosition)
    {
        float speed = 4f;
        Vector3 currentPosition = transform.position;
        //var dist = Vector3.Distance(currentPosition, targetPosition);
        //var speed = dist;
        Vector3 directionOfTravel = targetPosition - currentPosition;
        directionOfTravel.Normalize();
        transform.Translate(
            (directionOfTravel.x * speed * Time.deltaTime),
            (directionOfTravel.y * speed * Time.deltaTime),
            (directionOfTravel.z * speed * Time.deltaTime),
            Space.World);
    }
}
