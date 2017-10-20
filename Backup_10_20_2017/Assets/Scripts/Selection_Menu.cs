using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Menu : MonoBehaviour {

    public GameObject upperPart;
    public GameObject bottomPart;
    public GameObject leftPart;
    public GameObject rightPart;
    public GameObject node1;
    public GameObject node2;

    public GameObject indicator;
    //private LineRenderer lineRenderer;
    public static List<GameObject> list1 = new List<GameObject>(); // targets will be displayed in menu section 1
    public static List<GameObject> list2 = new List<GameObject>(); // targets will be displayed in menu section 2
    public static List<GameObject> list3 = new List<GameObject>(); // targets will be displayed in menu section 3
    public static List<GameObject> list4 = new List<GameObject>(); // targets will be displayed in menu section 4
    public static List<GameObject> targetsOnMenu = new List<GameObject>(); // targets will be displayed in menu section 4
    // Use this for initialization
    void Start () {
        //lineRenderer = GetComponent<LineRenderer>();
        displayTargetsOnMenu();
	}
	
	// Update is called once per frame
	void Update () {
        FoveInterface.EyeRays rays = FoveInterface.GetEyeRays();
        //var point = rays.left.GetPoint(1.0f);
        //indicator.transform.position = point;
        RaycastHit hit;
        Physics.Raycast(rays.right, out hit, Mathf.Infinity);
        var vector1 = hit.point - node1.transform.position;
        var vector2 = node2.transform.position - node1.transform.position;
        var angle1 = Vector3.Angle(vector1, vector2);

        var vector3 = hit.point - node2.transform.position;
        var vector4 = node1.transform.position - node2.transform.position;
        var angle2 = Vector3.Angle(vector3, vector4);
        //Debug.LogFormat("angle 1: {0}, angle 2: {1}", angle1, angle2);
        highlight(angle1, angle2);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cleanTargetsOnMenu();
            //further selection
            displayTargetsOnMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //clear all targets on menu
            TargetsController.selectedList.Clear();
        }
    }

    void cleanTargetsOnMenu()
    {
        for (var i = 0; i < targetsOnMenu.Count; i++)
        {
            //Destory(targetsOnMenu[i]); 
            DestroyObject(targetsOnMenu[i]);
        }
    }

    void displayTargetsOnMenu()
    {
        //menu section 1:
        for(var i=0; i<list1.Count; i++)
        {
            var obj = Instantiate(list1[i], this.transform, false);
            targetsOnMenu.Add(list1[i]);
            obj.transform.localScale = new Vector3(30f, 30f, 30f);
            obj.transform.localPosition = new Vector3(Random.Range(-300, -150), 0, 0);  //  -260     -400 ~ -0      
        }
        //menu section 2:
        for (var i = 0; i < list2.Count; i++)
        {
            var obj = Instantiate(list2[i], this.transform, false);
            targetsOnMenu.Add(list2[i]);
            obj.transform.localScale = new Vector3(30f, 30f, 30f);
            obj.transform.localPosition = new Vector3(0, Random.Range(-250, -150), 0);  //   180    -300 ~ -0  Random.Range(-280, 20)
        }
        //menu section 3:
        for (var i = 0; i < list3.Count; i++)
        {
            var obj = Instantiate(list3[i], this.transform, false);
            targetsOnMenu.Add(list3[i]);
            obj.transform.localScale = new Vector3(30f, 30f, 30f);
            obj.transform.localPosition = new Vector3(Random.Range(150, 300), 0, 0);  // 260    0 ~ 400  Random.Range(20, 380)
        }
        //menu section 4:
        for (var i = 0; i < list4.Count; i++)
        {
            var obj = Instantiate(list4[i], this.transform, false);
            targetsOnMenu.Add(list4[i]);
            obj.transform.localScale = new Vector3(30f, 30f, 30f);
            obj.transform.localPosition = new Vector3(0, Random.Range(150, 250), 0);  // 180    0 ~ 300  Random.Range(20, 280)
        }
    }

    void highlight(float angle1, float angle2)
    {
        if(angle1 < 51.13)
        {
            if(angle2 < 51.13)
            {
                //section 1
                hideAll();
                leftPart.SetActive(true);
                //Debug.Log("111111");
            }
            else if(angle2 > 51.13 && angle2 < 90)
            {
                //section 2
                hideAll();
                bottomPart.SetActive(true);
                //Debug.Log("222222");
            }
        }
        else
        {
            if(angle2 < 51.13)
            {
                //section 4
                hideAll();
                upperPart.SetActive(true);
                //Debug.Log("444444");
            }
            else if(angle2 > 51.13 && angle2 < 90)
            {
                //section 3
                hideAll();
                rightPart.SetActive(true);
                //Debug.Log("333333");
            }
        }
    }
    void hideAll()
    {
        leftPart.SetActive(false);
        rightPart.SetActive(false);
        upperPart.SetActive(false);
        bottomPart.SetActive(false);
    }
}