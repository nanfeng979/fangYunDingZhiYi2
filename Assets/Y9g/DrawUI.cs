using UnityEngine;
using UnityEngine.UI;

namespace Y9g
{
    public sealed class DrawUI
    {
        public static void DrawRing(GameObject gameObject, float ringThickness, Color ringColor)
        {
            // 检查 Image 组件。
            if (gameObject.GetComponent<Image>() == null)
            {
                gameObject.AddComponent<Image>(); // 如果没有 Image 组件，则添加一个 Image 组件。
            }
            if (gameObject.GetComponent<Image>().enabled == false)
            {
                gameObject.GetComponent<Image>().enabled = true; // 如果 Image 组件被禁用，则启用 Image 组件。
            }
            Image ringImage = gameObject.GetComponent<Image>();

            RectTransform rectTransform = ringImage.GetComponent<RectTransform>(); // 获取 RectTransform 组件。
            float ringRadius = (rectTransform.rect.width - ringThickness) / 2f; // 计算圆环的半径。

            // 根据 RectTransform 的宽高创建一个 Texture2D 对象。用于存储圆环的像素数据。
            Texture2D texture = new Texture2D((int)rectTransform.rect.width, (int)rectTransform.rect.height);
            // 遍历 Texture2D 的每一个像素。
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color pixelColor = Color.clear; // 默认像素颜色为透明。

                    // 计算像素到圆心的距离。
                    float distanceToCenter = Vector2.Distance(new Vector2(x, y), new Vector2(texture.width / 2, texture.height / 2));
                    // 如果像素到圆心的距离在圆环的厚度范围内，则将像素颜色设置为圆环的颜色。
                    if (distanceToCenter > ringRadius - ringThickness / 2 && distanceToCenter < ringRadius + ringThickness / 2)
                    {
                        pixelColor = ringColor; // 将像素颜色设置为圆环的颜色。
                    }

                    texture.SetPixel(x, y, pixelColor); // 根据像素坐标设置像素颜色。
                }
            }

            texture.Apply(); // 将像素数据应用到 Texture2D 对象。

            // 根据 Texture2D 对象创建一个 Sprite 对象。
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            ringImage.sprite = sprite; // 将 Sprite 对象设置到 Image 组件上。
        }

        public static void DrawCircle(GameObject gameObject, Color circleColor, float circleRadius = 0)
        {
            // 检查 Image 组件。
            if (gameObject.GetComponent<Image>() == null)
            {
                gameObject.AddComponent<Image>(); // 如果没有 Image 组件，则添加一个 Image 组件。
            }
            if (gameObject.GetComponent<Image>().enabled == false)
            {
                gameObject.GetComponent<Image>().enabled = true; // 如果 Image 组件被禁用，则启用 Image 组件。
            }
            Image circleImage = gameObject.GetComponent<Image>();

            RectTransform rectTransform = circleImage.GetComponent<RectTransform>();
            if (circleRadius == 0)
            {
                circleRadius = rectTransform.rect.width / 2f;
            }

            Texture2D texture = new Texture2D((int)rectTransform.rect.width, (int)rectTransform.rect.height);
            Vector2 center = new Vector2(texture.width / 2, texture.height / 2);

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color pixelColor = Color.clear;

                    float distanceToCenter = Vector2.Distance(new Vector2(x, y), center);
                    if (distanceToCenter <= circleRadius)
                    {
                        pixelColor = circleColor;
                    }

                    texture.SetPixel(x, y, pixelColor);
                }
            }

            texture.Apply();

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            circleImage.sprite = sprite;
            
        }
    }
}