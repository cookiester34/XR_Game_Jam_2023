using System;
using UnityEngine;

namespace CookieUtils.UtilSubHelpers.DataTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "IntData", menuName = "CookieHelpers/DataType/Int")]
    public class IntData : ScriptableObject
    {
        [SerializeField] private int maxValue;
        [SerializeField] private int currentValue;
        [HideInInspector] public bool useMaxValue = true;
        
        public void ResetValue()
        {
            if (!useMaxValue) return;
            currentValue = maxValue;
        }
        
        public void SetCurrentValue(int value, bool increaseMax = false)
        {
            currentValue = value;
            if (!useMaxValue) return;
            switch (increaseMax)
            {
                case true when currentValue > maxValue:
                    maxValue = currentValue;
                    break;
                default:
                {
                    if(currentValue > maxValue)
                        ResetValue();
                    break;
                }
            }
        }

        public int GetCurrentValue()
        {
            return currentValue;
        }
        
        public void AdjustCurrentValue(int value)
        {
            currentValue += value;
            if (!useMaxValue) return;
            if(currentValue > maxValue)
                ResetValue();
        }
        
        public void SetMaxValue(int value)
        {
            maxValue = value;
        }

        public int GetMaxValue()
        {
            return maxValue;
        }
        
        public void AdjustMaxValue(int value)
        {
            maxValue += value;
        }

        public bool AtMaxValue()
        {
            if (!useMaxValue) return false;
            return currentValue >= maxValue;
        }
    }
    
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(IntData))]
    public class IntDataEditor : UnityEditor.Editor
    {
        private UnityEditor.SerializedProperty maxValue, currentValue;
        

        private void OnEnable()
        {
            currentValue = serializedObject.FindProperty("currentValue");
            maxValue = serializedObject.FindProperty("maxValue");
        }

        public override void OnInspectorGUI()
        {
            IntData data = (IntData) target;
            serializedObject.Update();
            UnityEditor.EditorGUILayout.PropertyField(currentValue);
            UnityEditor.EditorGUILayout.Space();
            data.useMaxValue =  UnityEditor.EditorGUILayout.ToggleLeft("Use Max Value", data.useMaxValue);
            if(data.useMaxValue)
                UnityEditor.EditorGUILayout.PropertyField(maxValue);
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
