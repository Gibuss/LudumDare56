using UnityEngine;
using UnityEngine.UI;

public class emptylife : MonoBehaviour
{
    public Slider slider;

    public void SetMaxLife(int life)
    {
        slider.maxValue = life;
        slider.value = life;
    }

    public void SetLife(int life)
    {
        slider.value = life;
    }
}
