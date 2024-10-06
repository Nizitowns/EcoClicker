using UnityEngine;

//Author: Eduardo "edu05" Chiaratto
//Last Update: 10/03/2024 BR

public enum TreeState { Seed, Young, Adult, Glowing }

public class TreeManager : MonoBehaviour
{
    [Tooltip("The actual state of tree")]
    public TreeState treeState = TreeState.Seed;
    [HideInInspector] public Vector3 actualScale;
    [Tooltip("The time to tree grow up in seconds")]
    public float countdownTimer;
    public float glowingTimer;
    public float timer;

    void Start()
    {
        treeState = TreeState.Seed;
        timer = countdownTimer;
        actualScale = new Vector3(8.0f, 8.0f, 8.0f); //this will be remove in future, is just for all trees have same scale
        transform.localScale = new Vector3();
        Debug.Log(countdownTimer / 3);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        //will make tree grow up to its original scale
        if (timer > 0 && treeState != TreeState.Adult && treeState != TreeState.Glowing)
        {
            float size = countdownTimer - timer;
            transform.localScale = actualScale * (size / countdownTimer);
        }

        if (timer <= countdownTimer / 2 && treeState == TreeState.Seed) treeState = TreeState.Young;

        //when timer go to 0, will change the state
        if (timer <= 0)
        {
            if (treeState == TreeState.Young || treeState == TreeState.Glowing) treeState = TreeState.Adult;
            else if (treeState == TreeState.Adult) treeState = TreeState.Glowing;
            timer = glowingTimer;
            transform.localScale = actualScale;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //will verify if its a adult tree or a seed
            if (treeState == TreeState.Adult || treeState == TreeState.Glowing)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        TaskOnClick();
                    }
                }
            }
            else if (treeState == TreeState.Seed)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        SpeedGrow();
                    }
                }
            }
        }


        Debug.Log(Manager.deciLevel01.Energy);
    }

    void TaskOnClick()
    {
        //will check the number of trees that grew, and will add energy based on this number
        int trees = 0;
        foreach (GameObject tree in Manager.Instance.trees)
        {
            if (tree.GetComponent<TreeManager>().treeState == TreeState.Adult) trees++;
            if (tree.GetComponent<TreeManager>().treeState == TreeState.Glowing) trees += 20;
        }
        Manager.deciLevel01.Energy += trees;
    }

    private void SpeedGrow()
    {
        //will grow up the tree in exchange for water 
        Manager.deciLevel01.Water -= 1f;
        timer -= 10f;
    }

    /*private void ChangeState()
    {
        switch (treeState)
        {
            case TreeState.Seed:
                {
                    treeState = TreeState.Young;
                    break;
                }
            case TreeState.Young:
                {
                    treeState = TreeState.Adult;
                    break;
                }
            case TreeState.Adult:
                {
                    treeState = TreeState.Glowing;
                    break;
                }
            case TreeState.Glowing:
                {
                    treeState = TreeState.Adult;
                    break;
                }
        }
    }*/
}
