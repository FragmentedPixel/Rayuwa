using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopaciManager : MonoBehaviour
{
    private Grid grid;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();

        foreach (Transform tree in transform)
        {
            if (tree.localPosition.x < grid.gridWorldSize.x/2 && tree.localPosition.x > -grid.gridWorldSize.x/2 && tree.localPosition.z < grid.gridWorldSize.y/2 && tree.localPosition.z > -grid.gridWorldSize.y/2)
                tree.tag = "Tree";

            else
                tree.tag = "Untagged";
        }
    }

}
