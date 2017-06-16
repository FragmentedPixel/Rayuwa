using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopPanel : MonoBehaviour
{
    public GameObject troopRowPrefab;

    private Unit[] units;

    private void Start()
    {
        units = UnitsData.instance.units;
        PopulateList();
    }

    private void PopulateList()
    {
        foreach(Unit unit in units)
        {
            GameObject row = Instantiate(troopRowPrefab, transform);

            row.transform.GetChild(0).GetComponent<Image>().sprite = unit.prefab.GetComponent<UnitController>().image;
            row.transform.GetChild(1).GetComponent<Text>().text = unit.prefab.name;
            row.transform.GetChild(2).GetComponent<Text>().text = unit.strongPoints;
        }
    }

}
