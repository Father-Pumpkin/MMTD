using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class MoneyUI : MonoBehaviour {

	public TMP_Text moneyText;
	// Update is called once per frame
	void Update () {
		moneyText.text = "$" + PlayerStats.Money.ToString ();
	}
}
