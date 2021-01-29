using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once IdentifierTypo
public class TransformMarginScalerBehaviour : SceneObject
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform transformToScale;
    [SerializeField] private float sizeOfMarginInUnits;

    private const int AmountOfMargins = 2;

    void Start()
    {
        Sprite sprite = spriteRenderer.sprite;
        Vector2 spriteSize = sprite.rect.size;
        Vector2 worldSize = spriteSize / sprite.pixelsPerUnit;
        Vector3 lossyScale = transform.lossyScale;
        worldSize.x *= lossyScale.x;
        worldSize.y *= lossyScale.y;

        float scaledX = (worldSize.x - sizeOfMarginInUnits * AmountOfMargins) / worldSize.x;
        float scaledY = (worldSize.y - sizeOfMarginInUnits * AmountOfMargins) / worldSize.y;

        transformToScale.localScale = new Vector3(
            scaledX > 0 ? scaledX : 0,
            scaledY > 0 ? scaledY : 0,
            1);
    }
}
