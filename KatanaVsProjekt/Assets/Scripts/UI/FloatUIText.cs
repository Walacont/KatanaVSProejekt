using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FloatUIText : MonoBehaviour
{
    [SerializeField] private string prefix;
    [SerializeField] private string format = "F1";
    private TextMeshProUGUI label;

    private void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    public void SetValue(float value)
    {
        label.SetText(prefix + " " + value.ToString(format));
    }
}
