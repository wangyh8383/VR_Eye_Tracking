using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_rays_indicator_left : MonoBehaviour {

    private LineRenderer lineRenderer;
    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        InvokeRepeating("drawLeftEyeRay", 0.0f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {

    }

    void drawLeftEyeRay() {
        FoveInterface.EyeRays rays = FoveInterface.GetEyeRays();
        var origin = rays.left.origin;
        var merge = rays.left.GetPoint(10f);
        Debug.Log(origin.x);
        Debug.Log(merge.x);

        lineRenderer.numCapVertices = 2;
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, merge);
    }
}
