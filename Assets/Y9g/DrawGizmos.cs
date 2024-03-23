using UnityEngine;

namespace Y9g
{
    /// <summary>
    /// 自定义 Gizmos 工具类。
    /// </summary>
    public sealed class DrawGizmos
    {
        /// <summary>
        /// 绘制线。
        /// </summary>
        /// <param name="origin">线段的起点。</param>
        /// <param name="direction">线段的方向。</param>
        /// <param name="length">线段的长度。</param>
        /// <param name="color">线段的颜色。</param>
        internal void DrawLine(Vector3 origin, Vector3 direction, float length, Color color)
        {
            Vector3 endPosition = origin + direction * length; // 起点加上方向乘以长度，得到线段的终点。
            Gizmos.color = color;
            Gizmos.DrawLine(origin, endPosition);
        }

        /// <summary>
        /// 绘制线。
        /// </summary>
        /// <param name="origin">线段的起点。</param>
        /// <param name="direction">线段的方向。</param>
        /// <param name="length">线段的长度。</param>
        internal void DrawLine(Vector3 origin, Vector3 direction, float length)
        {
            DrawLine(origin, direction, length, Color.red);
        }
        
        /// <summary>
        /// 绘制线。
        /// </summary>
        /// <param name="origin">线段的起点。</param>
        /// <param name="direction">线段的方向。</param>
        internal void DrawLine(Vector3 origin, Vector3 direction)
        {
            DrawLine(origin, direction, 1.0f, Color.red);
        }

        /// <summary>
        /// 绘制XYZ轴。
        /// </summary>
        /// <param name="length">轴的长度。</param>
        internal void DrawLineXYZ(float length)
        {
            DrawLine(Vector3.zero, Vector3.up, length, Color.red);
            DrawLine(Vector3.zero, Vector3.right, length, Color.green);
            DrawLine(Vector3.zero, Vector3.forward, length, Color.blue);
        }
    }
}