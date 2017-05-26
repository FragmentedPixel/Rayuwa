using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        UnitController controller = other.GetComponentInParent<UnitController>();

        if (controller == null)
            return;

        controller.Reload();
    }
}
