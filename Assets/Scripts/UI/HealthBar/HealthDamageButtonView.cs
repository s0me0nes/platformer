using UnityEngine;

public class HealthDamageButtonView : HealthButtonsView
{
    protected override void OnClicked()
    {
        Health.TakeDamage(Value);
    }   
}