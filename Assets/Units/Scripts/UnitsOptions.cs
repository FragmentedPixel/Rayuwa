using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsOptions : MonoBehaviour
{
    public Text unitsCountText;
    public Transform panel;

    public GameObject unitRowPrefab;

    private void Start()
    {
        Unit[] units = UnitsData.instance.units;

        foreach (Unit unit in units)
            AddRow(unit);
    }

    private void AddRow(Unit unit)
    {
        if (!unit.unlocked)
            return;

        GameObject row = Instantiate(unitRowPrefab, panel);
        UnitRow unitRow = new UnitRow(unit, row);
    }

    private void Update()
    {
        Unit[] units = UnitsData.instance.units;
        float value = 0;

        foreach (Unit unit in units)
            value += unit.count;

        unitsCountText.text = value.ToString();
        unitsCountText.color = Color.Lerp(Color.yellow, Color.red, value / UnitsData.instance.maxUnits);
    }
}

public class UnitRow
{
    public Unit unit;

    private Text name;
    private Button add;
    private Button remove;
    private Text count;

    public UnitRow(Unit _unit, GameObject row)
    {
        unit = _unit;

        name = row.transform.GetChild(0).GetComponent<Text>();
        add = row.transform.GetChild(1).GetComponent<Button>();
        remove = row.transform.GetChild(2).GetComponent<Button>();
        count = row.transform.GetChild(3).GetComponent<Text>();

        name.text = unit.prefab.name;
        add.onClick.AddListener(delegate { Add(); });
        remove.onClick.AddListener(delegate { Remove(); });
        count.text = unit.count.ToString();
    }

    public void Add()
    {
        if(UnitsData.instance.canAddUnits)
            unit.count = Mathf.Clamp(unit.count + 1, 0, UnitsData.instance.maxUnits);
        count.text = unit.count.ToString();
    }

    public void Remove()
    {
        unit.count = Mathf.Clamp(unit.count - 1, 0, UnitsData.instance.maxUnits);
        count.text = unit.count.ToString();
    }
}
