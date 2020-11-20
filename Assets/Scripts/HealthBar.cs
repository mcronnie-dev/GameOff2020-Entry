using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HealthUI
{
    public Gradient gradient;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void SetColor()
    {
        float healthRatio = currentHealth / maxHealth;
        _image.fillAmount = healthRatio;
        _image.color = gradient.Evaluate(healthRatio);
    }

    new public void InitializeHealth(float newHealth)
    {
        base.InitializeHealth(newHealth);
        SetColor();
    }

    new public void SetHealth (float newHealth)
    {
        base.SetHealth(newHealth);
        SetColor();
    }
}
