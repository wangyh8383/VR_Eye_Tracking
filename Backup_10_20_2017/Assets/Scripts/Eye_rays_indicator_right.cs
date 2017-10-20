using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_rays_indicator_right : MonoBehaviour {

    private LineRenderer lineRenderer;
    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        InvokeRepeating("drawRightEyeRay", 0.0f, 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void drawRightEyeRay()
    {
        FoveInterface.EyeRays rays = FoveInterface.GetEyeRays();
        var origin = rays.right.origin;
        var merge = rays.right.GetPoint(10f);
        Debug.Log(origin.x);
        Debug.Log(merge.x);

        lineRenderer.numCapVertices = 2;
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, merge);
    }
}
