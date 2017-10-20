using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Controller : MonoBehaviour {

    const int targetNum = 100;

    private GameObject convergence;
    private GameObject canvas;
    private List<GameObject> targetList = new List<GameObject>(); // store all the "target_*" in the scene.
    private List<GameObject> selectedList = new List<GameObject>(); // store all the selected targets in the scene.
    // Use this for initialization
    void Start () {
        convergence = GameObject.Find("Convergence");
        canvas = GameObject.Find("Canvas");
        selectedList = TargetsController.selectedList;

        for(var i=0; i<targetNum; i++)
        {
            string newTargetName = "target_" + (i + 1);
            var newTarget = GameObject.Find(newTargetName);
            if(newTarget != null)
            {
                targetList.Add(newTarget);
            }
        }

        Debug.Log(targetList.Count);

        canvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space)) {
            //perform selection
            //findInsideObjects(); // put all selected targets into the selectedList
            canvas.SetActive(true);
            convergence.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(false);
            convergence.SetActive(true);
            //selectedList.Clear();
        }
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    selectedList.Clear();
        //    Debug.Log("Get Downarrow");
        //    //furtherSelection(); // put certain list to the selectedList
        //}
    }
    void separateTargets()
    {
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

    void findInsideObjects()
    {
        for (var i = 0; i < targetList.Count; i++)
        {
            var dist = Vector3.Distance(targetList[i].transform.position, convergence.transform.position);
            if (dist <= convergence.transform.localScale.x)
            {
                selectedList.Add(targetList[i]);
                Debug.LogFormat("target{0} got!", i);
            }
        }
    }
    //void furtherSelection()
    //{
    //    //for(var i=0; i< Selection_Menu.targetsOnMenu; i++)
    //    if (Selection_Menu.leftPart.active) // pass list1 to selectedList
    //    {
    //        //section 1
    //        for (var i = 0; i < Selection_Menu.list1.Count; i++)
    //        {
    //            selectedList.Add(Selection_Menu.list1[i]);
    //        }
    //    }
    //    else if (Selection_Menu.upperPart.active) // pass list2 to selectedList
    //    {
    //        //section 2
    //        for (var i = 0; i < Selection_Menu.list2.Count; i++)
    //        {
    //            selectedList.Add(Selection_Menu.list2[i]);
    //        }
    //    }
    //    else if (Selection_Menu.rightPart.active) // pass list3 to selectedList
    //    {
    //        //section 3
    //        for (var i = 0; i < Selection_Menu.list3.Count; i++)
    //        {
    //            selectedList.Add(Selection_Menu.list3[i]);
    //        }
    //    }
    //    else if (Selection_Menu.bottomPart.active) // pass list4 to selectedList
    //    {
    //        //section 4
    //        for (var i = 0; i < Selection_Menu.list4.Count; i++)
    //        {
    //            selectedList.Add(Selection_Menu.list4[i]);
    //        }
    //    }
    //    separateTargets();
    //}
}
