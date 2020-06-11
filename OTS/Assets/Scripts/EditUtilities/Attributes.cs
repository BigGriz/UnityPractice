using UnityEngine;
using System;

/// Draws the field/property ONLY if the compared property compared by the comparison type with the value of comparedValue returns true.
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DrawIfAttribute : PropertyAttribute
{
    public string comparedPropertyName { get; private set; }
    public object comparedValue { get; private set; }
    public DisablingType disablingType { get; private set; }

    // Types of comparisons.
    public enum DisablingType
    {
        ReadOnly = 2,
        DontDraw = 3
    }

    // Only draws the field only if a condition is met. Supports enum and bools.
    public DrawIfAttribute(string comparedPropertyName, object comparedValue, DisablingType disablingType = DisablingType.DontDraw)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValue = comparedValue;
        this.disablingType = disablingType;
    }
}

/// Draws the field/property ONLY if the compared property compared by the comparison type with the value of comparedValue returns false.
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DrawIfNotAttribute : PropertyAttribute
{
    public string comparedPropertyName { get; private set; }
    public object comparedValue { get; private set; }
    public DisablingType disablingType { get; private set; }

    // Types of comparisons.
    public enum DisablingType
    {
        ReadOnly = 2,
        DontDraw = 3
    }

    // Only draws the field only if a condition is met. Supports enum and bools.
    public DrawIfNotAttribute(string comparedPropertyName, object comparedValue, DisablingType disablingType = DisablingType.DontDraw)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValue = comparedValue;
        this.disablingType = disablingType;
    }
}