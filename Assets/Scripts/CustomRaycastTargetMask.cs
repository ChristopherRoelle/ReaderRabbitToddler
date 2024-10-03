using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CustomRaycastTargetMask : Image
{
    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        //Convert Screen point to local coords within the RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out Vector2 localPoint);

        //Convert the local point to UV point
        Vector2 uvPoint = new Vector2(
            (localPoint.x - rectTransform.rect.x) / rectTransform.rect.width,
            (localPoint.y - rectTransform.rect.y) / rectTransform.rect.height
        );

        //Flip the y-axis for the UVs
        uvPoint.y = 1f - uvPoint.y;

        //Get the texture for the sprite
        Texture2D texture = sprite.texture;
        Rect textureRect = sprite.textureRect;

        //Convert UV to texture pixel coords
        int texX = Mathf.FloorToInt( uvPoint.x * textureRect.width + textureRect.x );
        int texY = Mathf.FloorToInt( uvPoint.y * textureRect.height + textureRect.y );

        //Check if the pixel coords are within the texture bounds
        if(texX < 0 || texX >= texture.width || texY < 0  || texY >= texture.height)
        {
            return false; //Out of bounds
        }

        //Get the pixel color at the clicked location
        Color pixColor = texture.GetPixel(texX, texY);

        //If the pixel is transparent, we want to allow the click to passthru
        return pixColor.a > 0.1f;
    }
}
