using System;
using UnityEngine;

namespace CookieUtils.UtilSubHelpers.DataTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "TransformData", menuName = "CookieHelpers/DataType/Transform")]
    public class TransformData : ScriptableObject
    {
        [SerializeField] private Transform _transform;

        public TransformData(Transform transform) => SetTransform(transform);

        public Transform GetTransform() => _transform;

        public void SetTransform(Transform transform) => _transform = transform;

        public void SetTrPosition(Vector3 position) => _transform.position = position;
        
        public void SetTrLocalPosition(Vector3 position) => _transform.localPosition = position;
    }
}
