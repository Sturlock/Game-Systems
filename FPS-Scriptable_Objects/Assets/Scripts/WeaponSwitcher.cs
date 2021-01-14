using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject startCanvas;

    public PlayerLoadout_sObj loadout1;
    public PlayerLoadout_sObj loadout2;
    public PlayerLoadout_sObj loadout3;


    public void ChooseLoadout1()
    {
        GameLoadout.loadout = loadout1;
    }

    public void ChooseLoadout2()
    {
        GameLoadout.loadout = loadout2;
    }

    public void ChooseLoadout3()
    {
        GameLoadout.loadout = loadout3;
    }
    public void NextMenu(GameObject nxtCanvas)

    {
        nxtCanvas.SetActive(true);
        startCanvas.SetActive(false);
    }
}