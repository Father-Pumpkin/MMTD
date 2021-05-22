using UnityEngine.UI;
using UnityEngine;

public class ManaUI : MonoBehaviour
{
    public Text ManaText;

    private void Update()
    {
        ManaText.text = "Chore Money: " + PlayerStats.Mana;
    }
}
