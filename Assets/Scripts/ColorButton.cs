using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    public Color colorToSend = Color.white; // Default fallback
    public ColorMixerManager mixerManager;

    void Start()
    {
        if (colorToSend.a < 0.1f)
            colorToSend = GetComponent<Image>().color;

        GetComponent<Button>().onClick.AddListener(() => {
            if (mixerManager != null)
                mixerManager.AddColorToBox(colorToSend);
        });
    }
}
