using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour {

    float maxLife;
    public static Lifebar instance;

    Slider slider;


    void Start()
    {
        instance = this;
        slider = this.GetComponent<Slider>();
        slider.value = 100;
        maxLife = slider.value;
        Debug.Log("Currentlife " + slider.value);

    }


    void Update()
    {
        if(slider.value < maxLife)
        {
           //slider.value += increaseSpeed;
        }
        if(slider.value <= 0)
        {
            Debug.Log("Game Over");
            GameController.instance.GameOver();
        }
    }

    public void DecreaseLife(float amount)
    {
        slider.value += amount;
        Debug.Log("Currentlife " + slider.value);

    }

    public void IncreaseLife(float amount)
    {
        if((slider.value + amount) > maxLife)
        {
            slider.value = maxLife;
            return;
        }
        slider.value += amount;
        Debug.Log("Currentlife " + slider.value);

    }
}
