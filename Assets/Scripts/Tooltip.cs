using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;

    public TextMeshProUGUI tooltipText; // Reference to the TextMeshProUGUI asset to update with the tooltip text
    public string[] tooltipData; // Array of ScriptableObject data containing tooltips

    private int currentTooltipIndex = 0; // Index of the current tooltip being displayed

    private void Start() {
        instance = this;
        tooltipText = FindObjectOfType<ObjectiveText>().GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Check if the user pressed the enter bar
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Increment the index to move to the next tooltip in the array
            currentTooltipIndex++;

            // Check if we've reached the end of the array
            if (currentTooltipIndex >= tooltipData.Length)
            {
                // Reset the index back to the beginning
                currentTooltipIndex = 0;
            }

            // Update the tooltip text with the new tooltip data
            tooltipText.text = tooltipData[currentTooltipIndex];
        }
    } 
}
