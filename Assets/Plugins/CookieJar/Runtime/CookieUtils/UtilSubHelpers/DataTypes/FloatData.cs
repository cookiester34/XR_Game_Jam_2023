using System;
using UnityEngine;

namespace CookieUtils.UtilSubHelpers.DataTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "FloatData", menuName = "CookieHelpers/DataType/Float")]
    public class FloatData : ScriptableObject
    {
        [SerializeField] private float maxValue;
        [SerializeField] private float currentValue;
        [HideInInspector] public bool useMaxValue = true;
        
        public void ResetValue()
        {
            if (!useMaxValue) return;
            currentValue = maxValue;
        }
        
        public void SetCurrentValue(float value, bool increaseMax = false)
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
        public float GetCurrentValue()
        {
            return currentValue;
        }

        public void AdjustCurrentValue(float value)
        {
            currentValue += value;
            if (!useMaxValue) return;
            if(currentValue > maxValue)
                ResetValue();
        }
        
        public void SetMaxValue(float value)
        {
            maxValue = value;
        }

        public float GetMaxValue()
        {
            return maxValue;
        }
        
        public void AdjustMaxValue(float value)
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
    [UnityEditor.CustomEditor(typeof(FloatData))]
    public class FloatDataEditor : UnityEditor.Editor
    {
        private UnityEditor.SerializedProperty maxValue, currentValue;
        

        private void OnEnable()
        {
            currentValue = serializedObject.FindProperty("currentValue");
            maxValue = serializedObject.FindProperty("maxValue");
        }

        public override void OnInspectorGUI()
        {
            FloatData data = (FloatData) target;
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
