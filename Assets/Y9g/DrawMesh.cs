using UnityEngine;

namespace Y9g
{
    public sealed class DrawMesh
    {
        
        /// <summary>
        /// 生成六边形的网格。
        /// </summary>
        /// <param name="mesh"> 修改的网格对象。</param>
        /// <param name="vertices"> 顶点索引数据。</param>
        /// <param name="triangles"> 三角形索引数据。</param>
        public static void GenerateGrid(Mesh mesh, Vector3[] vertices, int[] triangles)
        {
            /// <summary>
            /// 修改顶点索引。
            /// </summary>
            mesh.vertices = vertices;

            /// <summary>
            /// 修改三角形索引。
            /// </summary>
            mesh.triangles = triangles;

            /// <summary>
            /// 重新计算法线。
            /// </summary>
            mesh.RecalculateNormals();

            /// <summary>
            /// 重新计算边界。
            /// </summary>
            mesh.RecalculateBounds();

            /// <summary>
            /// 重新计算切线。
            /// </summary>
            mesh.RecalculateTangents();
        }
    }
}