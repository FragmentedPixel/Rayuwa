using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHover : MonoBehaviour
{
    #region Variabiles
    private Transform currentDisplayed = null;
    #endregion

    #region Ray from Mouse
    private void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        
        ChangeVisibility(currentDisplayed, false);

        if (Physics.Raycast(ray, out raycastHit, 100f) && (raycastHit.collider.tag=="Enemy"))
        {
            currentDisplayed = raycastHit.transform.GetChild(0).transform;
            ChangeVisibility(currentDisplayed, true);
        }
	}
    #endregion

    #region Display Range
    private void ChangeVisibility(Transform trans, bool value)
    {
        if (trans == null)
            return;

        MeshRenderer[] renderers = trans.GetComponentsInChildren<MeshRenderer>();
        Debug.Log(renderers.Length);
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = value;
            Debug.Log(renderer.name);
        }
    }
    #endregion
}
