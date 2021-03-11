using UnityEngine;
using static GlobalVariables;

public class ChestOpenAnimation : MonoBehaviour
{
    [SerializeField] ChestColors chestColor;
    // Reward views
    [SerializeField] GameObject chestClosedView;
    [SerializeField] GameObject chestOpenedView;

    // Open chest reward view after the animation of opening chest is complete
    // Access from chest open animation
    public void OpenChestOpenedView()
    {
        chestClosedView.SetActive(false);
        chestOpenedView.SetActive(true);
        chestOpenedView.GetComponent<OpenedChestView>().OpenChestReward(chestColor);
    }
}
