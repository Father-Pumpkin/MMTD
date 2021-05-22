using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour {

	public TMP_Text LivesText;
    // Update is called once per frame
    void Update () {
		LivesText.text = PlayerStats.Lives + " LIVES LEFT";
	}
}
