using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour {

    public Text[] text;
  	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < text.Length; i++)
            text[i].text = UpgradesManager.instance.PresentUpgrades(i).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class UpgradeText
{
    public Text upgradeText;
    public int upgradeInt;

   public UpgradeText(Text _upgradeText,int _upgradeInt)
    {
        upgradeText = _upgradeText;
        upgradeInt = _upgradeInt;
    }
}
