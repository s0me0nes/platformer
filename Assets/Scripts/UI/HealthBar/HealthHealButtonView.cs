using UnityEngine;

public class HealthHealButtonView : HealthButtonsView
{
    protected override void OnClicked()
    {
        Health.Heal(Value);
    }
}
