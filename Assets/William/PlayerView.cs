using UnityEngine;
using UnityEngine.UI;
public class PlayerView : MonoBehaviour
{
    public Image healthBar;
    public void UpdateHealthBar(float health) { healthBar.fillAmount = health / 100; }
}