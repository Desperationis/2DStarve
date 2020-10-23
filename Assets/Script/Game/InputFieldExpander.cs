using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Adds more functionality to Unity's input field. 
/// </summary>
public class InputFieldExpander : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The input field to listen to.")]
    private TMP_InputField inputField = null;

    private void Awake()
    {
        inputField.onEndEdit.AddListener(OnSubmit);
    }

    private void OnSubmit(string message)
    {
        inputField.text = "";

        // Deselects the input field.
        var eventSystem = EventSystem.current;
        if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null); // Don't delete the IF statement; It softlocks if you do
    }

}