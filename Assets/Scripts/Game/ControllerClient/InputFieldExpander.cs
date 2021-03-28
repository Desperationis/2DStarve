using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

/// <summary>
/// Adds more functionality to Unity's input field.
/// </summary>
public class InputFieldExpander : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField = null;

    [System.Serializable]
    public class SubmitEvent : UnityEvent<string> { }
    private SubmitEvent onSubmit = new SubmitEvent();

    private void Awake()
    {
        inputField.onEndEdit.AddListener((string tmp) => { DeselectInputField(); }) ;
        inputField.onEndEdit.AddListener(OnSubmit);
    }

    public void AddOnSubmitListener(UnityAction<string> call)
    {
        onSubmit.AddListener(call);
    }

    private void OnSubmit(string message)
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputField.text += "\n";
            inputField.ActivateInputField();
            inputField.caretPosition = inputField.text.Length;
            return;
        }

        if(message.Length > 0)
        {
            onSubmit.Invoke(message);
        }

        inputField.SetTextWithoutNotify("");
        StartCoroutine("ClearText");
    }

    private IEnumerator ClearText()
    {
        // Waits till the next frame to remove Unity's weird ASCII character
        // that's inserted after onSubmit().
        yield return null;
        inputField.SetTextWithoutNotify("");
    }

    private void DeselectInputField()
    {
        var eventSystem = EventSystem.current;
        if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject(null); // Don't delete the IF statement; It softlocks if you do
    }
}
