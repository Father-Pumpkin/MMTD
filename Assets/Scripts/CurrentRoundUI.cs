using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class CurrentRoundUI : MonoBehaviour
{
    public TMP_Text RoundsText;
    public GameObject GameMasterObject;
    // Update is called once per frame
    void Update()
    {
        RoundsText.text = "Current Round: " + GameMasterObject.GetComponent<PlayerStats>().roundsPublic;
    }
}
