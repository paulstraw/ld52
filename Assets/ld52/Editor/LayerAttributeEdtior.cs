using UnityEngine;
using UnityEditor;

// http://answers.unity.com/answers/1371058/view.html
[CustomPropertyDrawer(typeof(LayerAttribute))]
class LayerAttributeEditor : PropertyDrawer
{
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    property.intValue = EditorGUI.LayerField(position, label, property.intValue);
  }
}
