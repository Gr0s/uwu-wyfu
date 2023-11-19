using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public float MiningTime = 5f;
    private float currentDurability;
    private GameObject currentTarget;
    private Vector3 initialScale;
    private Vector3 initialPosition;

    void Start()
    {
        currentDurability = 1f;
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    void Update()
    {
        // Tutaj mo�esz doda� dodatkow� logik�, je�li to konieczne.
    }

    public void DecreaseDurability(float amount)
    {
        currentDurability -= amount;
        currentDurability = Mathf.Clamp01(currentDurability);
        UpdateScaleAndPosition();

        // Sprawd�, czy wytrzyma�o�� spad�a poni�ej 10%, je�li tak, zniszcz obiekt.
        if (currentDurability < 0.1f)
        {
            DestroyBlock();
        }
    }

    public void DestroyBlock()
    {
        // Dodaj tutaj kod do stopniowego niszczenia obiektu, np. animacje, efekty d�wi�kowe itp.
        Destroy(gameObject);
    }

    void UpdateScaleAndPosition()
    {
        // Aktualizuj skal� i pozycj� obiektu w zale�no�ci od wytrzyma�o�ci.
        float newHeight = initialScale.y * currentDurability;
        transform.localScale = new Vector3(initialScale.x, newHeight, initialScale.z);

        // Oblicz now� pozycj�, aby zachowa� doln� cz�� obiektu na sta�ym miejscu.
        float newYPosition = initialPosition.y - (initialScale.y - newHeight) / 2f;
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }

    public float CurrentDurability
    {
        get { return currentDurability; }
    }

    public GameObject CurrentTarget
    {
        get { return currentTarget; }
        set { currentTarget = value; }
    }
}