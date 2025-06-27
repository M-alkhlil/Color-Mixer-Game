using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ColorMixerManager : MonoBehaviour
{
    public Image targetColorDisplay;
    public Image[] mixBoxes;
    public GameObject guidePanel;
    public GameObject resultPopup;
    public TMP_Text resultText;

    private Color targetColor;
    private List<Color> selectedColors = new List<Color>();

    void Start()
    {
        SetNewTargetColor();
        if (resultPopup != null)
            resultPopup.SetActive(false);
    }

    public void SetNewTargetColor()
    {
        targetColor = new Color(Random.value, Random.value, Random.value);
        targetColorDisplay.color = targetColor;
    }

    public void AddColorToBox(Color color)
    {
        if (selectedColors.Count >= 3) return;

        mixBoxes[selectedColors.Count].color = color;
        selectedColors.Add(color);
    }

    public void ClearBoxes()
    {
        selectedColors.Clear();
        foreach (var box in mixBoxes)
        {
            box.color = Color.white;
        }
    }

    public void CheckColorMatch()
    {
        if (selectedColors.Count == 0) return;

        Color mixed = MixColors(selectedColors);
        float diff = Vector3.Distance(new Vector3(mixed.r, mixed.g, mixed.b), new Vector3(targetColor.r, targetColor.g, targetColor.b));

        if (diff < 0.1f)
        {
            ShowPopup(" Correct! You matched the color.");
        }
        else
        {
            ShowPopup(" Try Again. Keep mixing!");
        }
    }

    Color MixColors(List<Color> colors)
    {
        float r = 0, g = 0, b = 0;
        foreach (var c in colors)
        {
            r += c.r;
            g += c.g;
            b += c.b;
        }
        return new Color(r / colors.Count, g / colors.Count, b / colors.Count);
    }

    public void ToggleGuide()
    {
        guidePanel.SetActive(!guidePanel.activeSelf);
    }

    void ShowPopup(string message)
    {
        resultText.text = message;
        resultPopup.SetActive(true);
        Invoke(nameof(HidePopup), 2f);
    }

    void HidePopup()
    {
        resultPopup.SetActive(false);
    }
}
