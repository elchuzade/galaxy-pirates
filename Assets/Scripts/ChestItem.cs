using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class ChestItem : MonoBehaviour
{
    [SerializeField] ChestColors chestColor;
    [SerializeField] Text chestCount;
    [SerializeField] Text chestPrice;

    // @access frm ChestsStatus
    public void SetCount(int count)
    {
        chestCount.text = count.ToString();
        // Hide price tag if there are some chests
        if (count > 0)
        {
            chestPrice.transform.parent.gameObject.SetActive(false);
        } else
        {
            chestPrice.transform.parent.gameObject.SetActive(true);
        }
    }

    // @access frm ChestsStatus
    public void SetPrice(int price)
    {
        chestPrice.text = price.ToString();
    }
}
