using UnityEngine;
using UnityEngine.UI;

namespace Y9g
{
    public sealed class UIAbout
    {
        public static Color GetPixelColor(GameObject gameObject, Vector2 localPoint)
        {
            // 获取 Image 的 Texture。
            Texture2D texture = gameObject.GetComponent<Image>().sprite.texture;

            // 获取 Texture 的像素坐标。
            int x = (int)localPoint.x;
            int y = (int)localPoint.y;

            // 获取像素颜色。
            Color pixelColor = texture.GetPixel(x, y);

            return pixelColor;
        }
    }
}