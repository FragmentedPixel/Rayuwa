using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour {

    public Transform[] images;
   
    private void Start()
    {
        for (int i = 0; i < images.Length; i++)
            new UpgradeImage(images[i], i);

    }
}

public class UpgradeImage
{
    public Text upgradeLevel;
    public Text upgradeCost;
    public Button upgradeButton;

    public UpgradeImage(Transform parent, int i)
    {
        upgradeLevel = parent.GetChild(0).GetComponent<Text>();
        upgradeCost = parent.GetChild(1).GetComponent<Text>();
        upgradeButton = parent.GetChild(2).GetComponent<Button>();

        upgradeLevel.text = UpgradesManager.instance.upgradeArray[i].ToString();
        upgradeCost.text = UpgradesManager.instance.UpgradeCost(i).ToString();
        upgradeButton.onClick.AddListener(delegate { UpgradesManager.instance.ApplyUpgrade(i); });
    }
}
/*public Text[] text;

   private void Start ()
   {
       for (int i = 0; i < text.Length; i++)
           text[i].text = UpgradesManager.instance.upgradeArray[i].ToString();
   }
   */


/*
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
 * */
