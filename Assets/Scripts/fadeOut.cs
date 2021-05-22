using UnityEngine;
using TMPro;

public class fadeOut : MonoBehaviour
{
    public TextMeshProUGUI textToFade;

    // Update is called once per frame
    void Update()
    {
        Color colorToFade = textToFade.color;
        colorToFade.a -= .8f * Time.deltaTime;
        textToFade.color = colorToFade;
    }
}
