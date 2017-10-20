using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetsController : MonoBehaviour {
    //define a box (x,y,z) with width 2R, instantiate targets inside this box
    const float boxX = 2.0f;
    const float boxY = 2.0f;
    const float boxZ = 2.0f;
    const float boxR = 2.0f;
    //
    public static int numOfTargets = 200;

    private GameObject sampleTarget;
    private GameObject convergence;
    private GameObject fove;
    private GameObject upperPart;
    private GameObject bottomPart;
    private GameObject leftPart;
    private GameObject rightPart;

    public Material lightMaterial;
    public Material darkMaterial;
    private List<GameObject> targetList = new List<GameObject>();
    public static List<GameObject> selectedList = new List<GameObject>();

    //control the user's positions
    private Vector3 oldPosition;
    private Vector3 newPosition = new Vector3(5,5,5);
    //
    private int numHitSpace = 0;
    // Use this for initialization
    void Start () {
        sampleTarget = GameObject.Find("Target_no_1");
        convergence = GameObject.Find("Convergence");
        fove = GameObject.Find("Fove Rig");
        upperPart = GameObject.Find("UpperPart");
        bottomPart = GameObject.Find("BottomPart");
        leftPart = GameObject.Find("LeftPart");
        rightPart = GameObject.Find("RightPart");

        for (var i=0; i<numOfTargets; i++)
        {
            var target = Instantiate(sampleTarget, getRandomPosition(), getRandomRotation());
            targetList.Add(target);
        }
        Debug.Log(targetList.Count);
        oldPosition = fove.transform.position;
	}
	// Update is called once per frame
	void Update () {
        highlightTargets();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fove.transform.position = newPosition;
            if (numHitSpace != 0)
            {
                Debug.Log(upperPart.activeInHierarchy);
            }

            if(fove.transform.position != newPosition)
            {
                findInsideTargets(); // only when user was in the selection area, will perform the initial selection
                numHitSpace = 1;
            }
            separateTargets(); // separate targets into 4 lists
            Debug.LogFormat("{0}, {1}, {2}, {3}", Selection_Menu.list1.Count, Selection_Menu.list2.Count, Selection_Menu.list3.Count, Selection_Menu.list4.Count);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fove.transform.position = oldPosition;
            convergence.transform.position = oldPosition;
            numHitSpace = 0;
        }
    }
    Vector3 getRandomPosition()
    {
        var x = Random.Range(boxX - boxR, boxX + boxR);
        var y = Random.Range(boxY - boxR, boxY + boxR);
        var z = Random.Range(boxZ - boxR, boxZ + boxR);
        var position = new Vector3(x, y, z);
        return position;
    }
    Quaternion getRandomRotation() {
        return Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }
    void findInsideTargets()
    {
        for (var i = 0; i < targetList.Count; i++)
        {
            var bubbleRadius = convergence.transform.localScale.x;
            //if target is inside the bubble, add it to the selectedList
            if (Vector3.Distance(targetList[i].transform.position, convergence.transform.position) <= bubbleRadius)
            {
                selectedList.Add(targetList[i]);
                Debug.LogFormat("target{0} got!", i);
            }
        }
    }
    void highlightTargets()
    {
        for(var i=0; i<targetList.Count; i++)
        {
            var bubbleRadius = convergence.transform.localScale.x;
            //if target is inside the bubble, highlight it
            if (Vector3.Distance(targetList[i].transform.position, convergence.transform.position) <= bubbleRadius)
            {
                //Debug.Log("Change Material");
                targetList[i].GetComponent<Renderer>().material = lightMaterial;
            }
            else
            {
                targetList[i].GetComponent<Renderer>().material = darkMaterial;
            }
        }
    }
    void separateTargets()
    {
        Selection_Menu.list1.Clear();
        Selection_Menu.list2.Clear();
        Selection_Menu.list3.Clear();
        Selection_Menu.list4.Clear();

        int len = selectedList.Count / 4;
        Debug.LogFormat("I got {0} targets", selectedList.Count);
        for (var i = 0; i < len; i++)  // separate data into list1, list2, list3
        {
            Selection_Menu.list1.Add(selectedList[i]);
            Selection_Menu.list2.Add(selectedList[i + len]);
            Selection_Menu.list3.Add(selectedList[i + 2 * len]);
        }
        for (var i = 3 * len; i < selectedList.Count; i++)  // list4 covers all left elements
        {
            Selection_Menu.list4.Add(selectedList[i]);
        }
        Debug.LogFormat("list1: {0}, list2: {1}, list3: {2}, list4: {3}", Selection_Menu.list1.Count, Selection_Menu.list2.Count, Selection_Menu.list3.Count, Selection_Menu.list4.Count);
    }

}
