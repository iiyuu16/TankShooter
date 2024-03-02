using UnityEngine;
using UnityEngine.UI;

public class BaseSliders : MonoBehaviour
{
    public PlayerBase playerBase; // Reference to the PlayerBase script
    public Slider hpSlider; // Reference to the HP slider
    public Slider shieldSlider; // Reference to the shield slider

    void Update()
    {
        if (playerBase != null && hpSlider != null && shieldSlider != null)
        {
            // Update HP slider value based on current HP
            hpSlider.value = (float)playerBase.currentHP / playerBase.maxHP;

            // Update shield slider value based on current shield
            shieldSlider.value = (float)playerBase.currentShield / playerBase.maxShield;
        }
    }
}