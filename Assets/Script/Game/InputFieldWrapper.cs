using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// A wrapper for Unity's input field.
/// </summary>
public class InputFieldWrapper : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The input field to wrap around.")]
    private TMP_InputField inputField;

    [System.Serializable]
    public class ReturnEvent : UnityEvent<string> { }

    public ReturnEvent onReturn = new ReturnEvent();
    public UnityEvent onFocused = new UnityEvent();
    public UnityEvent onNotFocused = new UnityEvent();

    [HideInInspector]
    public bool isFocused = false;
    private bool alreadyFocused = false;
    private bool returnPressed = false;

    /// <summary>
    /// Invoke events once they are needed.
    /// </summary>
    void Update()
    {
        if (isFocused && Input.GetKey(KeyCode.Return) && !returnPressed)
        {
            onReturn.Invoke(inputField.text);
            inputField.text = ""; // Automatically clear text
        }

        returnPressed = Input.GetKey(KeyCode.Return);
        isFocused = inputField.isFocused;

        if (isFocused && !alreadyFocused)
        {
            onFocused.Invoke();
        }

        if (!isFocused && alreadyFocused)
        {
            onNotFocused.Invoke();
        }

        alreadyFocused = isFocused;
    }

    /// <summary>
    /// Forces the input field to accept input.
    /// </summary>
    public void ActivateInputField()
    {
        inputField.ActivateInputField();
    }
}
