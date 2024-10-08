using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakClick : MonoBehaviour
{
    
    // Start is called before the first frame update
DeciLevel01 deciLevel01 = new DeciLevel01();
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // Check if the clicked object is this Oak object
                {
                    TaskOnClick();
                }
            }
        }
    }

    void TaskOnClick()
    {
       deciLevel01.Energy += 1;
    }
}
