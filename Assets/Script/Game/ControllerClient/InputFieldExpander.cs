using UnityEngine;
using UnityEngine.Events;
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

    [System.Serializable]
    public class SubmitEvent : UnityEvent<string> { }
    public SubmitEvent onSubmit = new SubmitEvent();

    private void Awake()
    {
        inputField.onEndEdit.AddListener(OnEndEdit);
        inputField.onSubmit.AddListener(OnSubmit);
    }

    private void OnEndEdit(string message = "")
    {
        Deselect();
    }

    private void OnSubmit(string message)
    {
        onSubmit.Invoke(message);
        inputField.text = "";
    }

    private void Deselect(string message = "")
    {
        // Deselects the input field. 
        var eventSystem = EventSystem.current;
        if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null); // Don't delete the IF statement; It softlocks if you do
    }

}