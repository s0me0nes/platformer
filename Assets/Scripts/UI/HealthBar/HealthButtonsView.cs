using UnityEngine;
using UnityEngine.UI;

public class HealthButtonsView : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [SerializeField] protected Button Button;

    [SerializeField] protected int Value = 10;

    private void OnEnable()
    {
        Button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnClicked);
    }

    protected virtual void OnClicked() { }
}