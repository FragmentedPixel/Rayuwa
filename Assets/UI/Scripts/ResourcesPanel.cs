using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanel : MonoBehaviour
{
    public Text gained;
    public Text win;
    public Text bonus;
    public Text total;

    public void SetResources(int _gained, int _win, int _bonus, int _total)
    {
        gained.text = _gained.ToString();
        win.text = _win.ToString();
        bonus.text = _bonus.ToString();
        total.text = _total.ToString();
    }

}
