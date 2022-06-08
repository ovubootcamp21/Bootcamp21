using System.Collections;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private TMP_Text goldCountText;
    private int goldCount = 0;

    // Use this for initialization
    void Start()
    {
        goldCountText.text = goldCount.ToString();
    }

   
    public void UpdateCoin()
    {
        goldCount++;
        goldCountText.text = goldCount.ToString();
    }
}