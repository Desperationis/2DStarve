using UnityEditor;
using UnityEngine;

/*
 * ReadOnly.cs
 * 
 * Creates a read-only attribute.
 * Special thanks to It3ration on unity forums!
 * https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html
*/ 

public class ReadOnlyAttribute : PropertyAttribute
{

}


// Only define read-only if current build is an editor build.
#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

#endif
