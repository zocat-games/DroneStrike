using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Zocat
{
    [ExecuteInEditMode]
    public class CustomSlider : MonoBehaviour
    {
        public Image ValueDisplay;
        public TextMeshProUGUI Text;
        public float MaxValue;
        public ValueType ValueType;

        [PropertyRange(0, "MaxValue")] [PropertyOrder(3)]
        public float Dynamic;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) SetValue(Random.Range(0, 100));
        }

        private void OnGUI()
        {
            // SetValue(Dynamic);
        }


        public void SetValue(float value)
        {
            ValueDisplay.DOFillAmount(value / MaxValue, .2f);
            if (ValueType == ValueType.INT) Text.text = value.ToString();
            else Text.text = value.ToString();
        }
    }

    public enum ValueType
    {
        INT,
        FLOAT
    }
}