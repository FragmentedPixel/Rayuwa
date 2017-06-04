using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsManager : MonoBehaviour
{
    private Trap[] traps;

    private void Start()
    {
        traps = FindObjectsOfType<Trap>();

        foreach (Trap trap in traps)
            trap.enabled = true;
    }
}
