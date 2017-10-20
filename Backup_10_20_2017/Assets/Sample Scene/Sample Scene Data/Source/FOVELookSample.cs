using UnityEngine;
using System.Collections;

public class FOVELookSample : MonoBehaviour {
    public Light l;
	Collider my_collider;
    private Material m;
    private bool light_enabled = false;

    // Use this for initialization
    void Start() {
        my_collider = GetComponent<Collider>();

        l = this.transform.GetComponentInChildren<Light>();
        if (l)
        {
            light_enabled = true;
            l.enabled = false;
        }
        m = gameObject.GetComponent<Renderer>().material;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (FoveInterface.IsLookingAtCollider(my_collider))
        {
            if (light_enabled)
            {
                l.enabled = true;
                m.SetColor("_EmissionColor", l.color);
                DynamicGI.SetEmissive(GetComponent<Renderer>(), l.color);
                m.EnableKeyword("_EMISSION");
            }
            //bool check = FoveInterface.IsLookingAtCollider(my_collider);
        } else
		{
			gameObject.GetComponent<Renderer> ().material.color = Color.white;
            //GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            m.DisableKeyword("_EMISSION");
            if (light_enabled)
            {
                l.enabled = false;
                DynamicGI.SetEmissive(GetComponent<Renderer>(), Color.black);
            }
        }
	}
}
