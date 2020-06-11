using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfPropertyDrawer : PropertyDrawer
{
    // Reference to the attribute on the property.
    DrawIfAttribute drawIf;
    // Field that is being compared.
    SerializedProperty comparedField;

    // Returns the Pixel Height in Editor
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Set Height to Zero if Meant to be Hidden
        if (!ShowMe(property) && drawIf.disablingType == DrawIfAttribute.DisablingType.DontDraw)
        {
            return (0.0f);
        }

        // Return Default Height.
        return base.GetPropertyHeight(property, label);
    }

    // Errors default to Showing the Property.
    private bool ShowMe(SerializedProperty property)
    {
        drawIf = attribute as DrawIfAttribute;
        // Get the Serialized Parameter
        string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName) : drawIf.comparedPropertyName;
        comparedField = property.serializedObject.FindProperty(path);
        // No Parameter Found
        if (comparedField == null)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }

        // Get Value of Serialized Parameter & Compare - Supports Bool & Enum Currently
        switch (comparedField.type)
        {
            case "bool":
                return comparedField.boolValue.Equals(drawIf.comparedValue);
            case "Enum":
                return comparedField.enumValueIndex.Equals((int)drawIf.comparedValue);
            default:
                Debug.LogError("Error: " + comparedField.type + " is not supported of " + path);
                return true;
        }
    }

    // Drawing Field in Editor
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // If the condition is met, simply draw the field.
        if (ShowMe(property))
        {
            EditorGUI.PropertyField(position, property);
        }
        // Check if Read Only
        else if (drawIf.disablingType == DrawIfAttribute.DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property);
            GUI.enabled = true;
        }
    }
}

[CustomPropertyDrawer(typeof(DrawIfNotAttribute))]
public class DrawIfNotPropertyDrawer : PropertyDrawer
{
    // Reference to the attribute on the property.
    DrawIfNotAttribute drawIf;
    // Field that is being compared.
    SerializedProperty comparedField;

    // Returns the Pixel Height in Editor
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Set Height to Zero if Meant to be Hidden
        if (!ShowMe(property) && drawIf.disablingType == DrawIfNotAttribute.DisablingType.DontDraw)
        {
            return (0.0f);
        }

        // Return Default Height.
        return base.GetPropertyHeight(property, label);
    }

    // Errors default to Showing the Property.
    private bool ShowMe(SerializedProperty property)
    {
        drawIf = attribute as DrawIfNotAttribute;
        // Get the Serialized Parameter
        string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName) : drawIf.comparedPropertyName;
        comparedField = property.serializedObject.FindProperty(path);
        // No Parameter Found
        if (comparedField == null)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }

        // Get Value of Serialized Parameter & Compare - Supports Bool & Enum Currently
        switch (comparedField.type)
        {
            case "bool":
                return !comparedField.boolValue.Equals(drawIf.comparedValue);
            case "Enum":
                return !comparedField.enumValueIndex.Equals((int)drawIf.comparedValue);
            default:
                Debug.LogError("Error: " + comparedField.type + " is not supported of " + path);
                return true;
        }
    }

    // Drawing Field in Editor
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // If the condition is met, simply draw the field.
        if (ShowMe(property))
        {
            EditorGUI.PropertyField(position, property);
        }
        // Check if Read Only
        else if (drawIf.disablingType == DrawIfNotAttribute.DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property);
            GUI.enabled = true;
        }
    }
}