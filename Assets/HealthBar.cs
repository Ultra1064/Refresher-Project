using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Stats myStats;
    [SerializeField] Image healthBarImage;
    void Update()
    {
        healthBarImage.fillAmount = myStats.Health / 100;
    }
}
