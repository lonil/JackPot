using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Event", order = 2)]
public class Event : ScriptableObject
{
    public string _name;
    public string description;

    public int displayDelay;

    public Curve_Tag[] curves;

    public Requirement requerement;
}

[System.Serializable]
public class Requirement
{
    public RequirementType requirementType;

    public TagRequirement_Params[] tagRequirement_Params;
}

[System.Serializable]
public class TagRequirement_Params
{
    public Tag tag;
    public float minimum;
}

//[CustomPropertyDrawer(typeof(Requirement))]
//public class IngredientDrawerUIE : PropertyDrawer
//{
//    const int curveWidth = 50;

//    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
//    {
//        var rt = prop.FindPropertyRelative("requirementType");

//        EditorGUI.EnumPopup(new Rect(pos.width - curveWidth, pos.y, curveWidth, pos.height), typeof(RequirementType));

//        if (rt.enumValueIndex == (int)RequirementType.ByTagMinimum)
//        {
//            var l = prop.FindPropertyRelative("tagRequirement_Params");

//            EditorGUI.PropertyField(new Rect(pos.width - curveWidth, pos.y, curveWidth, pos.height), l, GUIContent.none);
//        }
//        else 
//        {
//            var t = prop.FindPropertyRelative("timeDelay");

//            EditorGUI.PropertyField(new Rect(pos.width - curveWidth, pos.y, curveWidth, pos.height), t, GUIContent.none);
//        }
//    }
//}