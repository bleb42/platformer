using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class VampirizmTimer : MonoBehaviour
{
    private TextMeshProUGUI _timerText;

    private void Awake()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateValue(int remainingTime)
    {
        _timerText.text = remainingTime.ToString();
    }
}