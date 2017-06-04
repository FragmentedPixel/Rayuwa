using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
    public static UpgradeScript instance;

    public Text moneyText;
    public AudioClip buySound;
    public AudioClip notEnoughMoney;
    public Transform[] images;

    private AudioSource audioS;
    private List<UpgradeImage> upgrades;

    private void Start()
    {
        instance = this;
        audioS = GetComponent<AudioSource>();
        upgrades = new List<UpgradeImage>();

        for (int i = 0; i < images.Length; i++)
            upgrades.Add(new UpgradeImage(images[i], i));
    }

    private void Update()
    {
        moneyText.text = UpgradesManager.instance.resources.ToString();

        foreach (UpgradeImage upgrade in upgrades)
            upgrade.Update();
    }

    public void PlaybuySound(bool success)
    {
        AudioClip clipToPlay = success ? buySound : notEnoughMoney;
        audioS.PlayOneShot(clipToPlay);
    }
}

public class UpgradeImage
{
    private Text upgradeLevel;
    private Text upgradeCost;
    public Button upgradeButton;

    private int index;

    public UpgradeImage(Transform parent, int i)
    {
        index = i;

        upgradeLevel = parent.GetChild(0).GetComponent<Text>();
        upgradeCost = parent.GetChild(1).GetComponent<Text>();
        upgradeButton = parent.GetChild(2).GetComponent<Button>();

        upgradeButton.onClick.AddListener(delegate {
            bool success = UpgradesManager.instance.ApplyUpgrade(index);
            UpgradeScript.instance.PlaybuySound(success);
        });

        Update();
    }

    public void Update()
    {
        upgradeLevel.text = UpgradesManager.instance.upgradeArray[index].ToString();
        upgradeCost.text = "Cost: " + UpgradesManager.instance.UpgradeCost(index).ToString();
    }
}
