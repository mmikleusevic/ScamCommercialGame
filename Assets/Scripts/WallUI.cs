using TMPro;
using UnityEngine;

public class WallUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro healthText;

    public void SetNumberText(int health)
    {
        healthText.text = health.ToString();
    }
}