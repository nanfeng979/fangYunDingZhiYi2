using System.Collections.Generic;
using UnityEngine;

namespace Y9g
{
    /// <summary>
    /// 地图生成器。
    /// </summary>
    public sealed class MapGenerate
    {
        /// <summary>
        /// 生成地图，以左下角为原点，向右为 x 轴正方向，向前为 z 轴正方向。
        /// </summary>
        /// <param name="mapXCount">地图 x 轴方向上的方块数量。</param>
        /// <param name="mapZCount">地图 z 轴方向上的方块数量。</param>
        /// <param name="mapBlock">地图方块的预制体。</param>
        /// <param name="mapGapDistance">地图方块之间的间隔距离。</param>
        internal static GameObject GenerateMap(int mapXCount, int mapZCount, GameObject mapBlock, float mapGapDistance)
        {
            // 地图方块的宽度和高度。
            float mapBlockWidth = mapBlock.transform.localScale.x; // 表示地图方块的宽度。
            float mapBlockHeight = mapBlock.transform.localScale.z; // 表示地图方块的高度。

            GameObject MapList = null;

            for (int i = 0; i < mapZCount; i++)
            {
                for (int j = 0; j < mapXCount; j++)
                {
                    // 计算每个方块在 x 或 z 轴方向上的位置，基于方块尺寸和间隔距离的乘积。
                    float xPosition = j * (mapBlockWidth + mapGapDistance); // x 轴方向上的位置。
                    float zPosition = i * (mapBlockHeight + mapGapDistance); // z 轴方向上的位置。

                    Vector3 generatePosition = new Vector3(xPosition, 0, -zPosition); // 根据 x 和 z 轴方向上的位置，计算出生成位置。

                    MapList = new GameObject("MapList");

                    // 在计算出的位置生成地图方块。
                    Object.Instantiate(mapBlock, generatePosition, Quaternion.identity, MapList.transform);
                }
            }

            return MapList;
        }

        /// <summary>
        /// 生成地图，将地图中心点作为原点，向右为 x 轴正方向，向前为 z 轴正方向。
        /// </summary>
        /// <param name="mapXCount">地图 x 轴方向上的方块数量。</param>
        /// <param name="mapZCount">地图 z 轴方向上的方块数量。</param>
        /// <param name="mapBlock">地图方块的预制体。</param>
        /// <param name="mapGapDistance">地图方块之间的间隔距离。</param>
        /// <param name="isCenter">是否以地图中心点为原点。</param>
        internal static GameObject GenerateMap(int mapXCount, int mapZCount, GameObject mapBlock, float mapGapDistance, bool isCenter)
        {
            // 如果不是以中心点为原点，则调用另一个重载方法。
            if (!isCenter)
            {
                return GenerateMap(mapXCount, mapZCount, mapBlock, mapGapDistance);
            }

            // 地图方块的宽度和高度。
            float mapBlockWidth = mapBlock.transform.localScale.x; // 表示地图方块的宽度。
            float mapBlockHeight = mapBlock.transform.localScale.z; // 表示地图方块的高度。

            #region 计算地图中心点的偏移量。
            // 如果地图的 x 或 z 轴方向上的方块数量为偶数，则需要计算出地图中心点的偏移量。因为偶数个方块，中心点不在方块的中心。
            float xPositionOffset = 0; // 表示 x 轴方向上的偏移量。
            float zPositionOffset = 0; // 表示 z 轴方向上的偏移量。
            // 如果地图的 x 或 z 轴方向上的方块数量为偶数。
            if (mapXCount % 2 == 0)
            {
                xPositionOffset = (mapBlockWidth + mapGapDistance) / 2; // 计算出 x 轴方向上的偏移量。
            }
            if (mapZCount % 2 == 0)
            {
                zPositionOffset = (mapBlockHeight + mapGapDistance) / 2; // 计算出 z 轴方向上的偏移量。
            }
            #endregion

            GameObject MapList = new GameObject("MapList");

            for (int i = -mapZCount / 2; i < (mapZCount + 1) / 2; i++)
            {
                for (int j = -mapXCount / 2; j < (mapXCount + 1) / 2; j++)
                {
                    // 计算每个方块在 x 或 z 轴方向上的位置，基于方块尺寸和间隔距离的乘积和偏移量。
                    float xPosition = j * (mapBlockWidth + mapGapDistance) + xPositionOffset; // x 轴方向上的位置。
                    float zPosition = i * (mapBlockHeight + mapGapDistance) + zPositionOffset; // z 轴方向上的位置。

                    Vector3 generatePosition = new Vector3(xPosition, 0, -zPosition); // 根据 x 和 z 轴方向上的位置，计算出生成位置。
                    
                    // 在计算出的位置生成地图方块。
                    Object.Instantiate(mapBlock, generatePosition, Quaternion.identity, MapList.transform);
                }
            }

            return MapList;
        }

        internal static GameObject GenerateVariousMap(int mapXCount, int mapZCount, List<GameObject> mapBlocks, int[] mapIndex, float mapGapDistance)
        {
            // 获取地图方块的宽度和高度。默认所有地图方块的宽度和高度相同。
            float mapBlockWidth = mapBlocks[0].transform.localScale.x; // 表示地图方块的宽度。
            float mapBlockHeight = mapBlocks[0].transform.localScale.z; // 表示地图方块的高度。

            GameObject MapList = new GameObject("MapList");

            int index = 0;
            for (int i = 0; i < mapZCount; i++)
            {
                for (int j = 0; j < mapXCount; j++)
                {
                    // 计算每个方块在 x 或 z 轴方向上的位置，基于方块尺寸和间隔距离的乘积。
                    float xPosition = j * (mapBlockWidth + mapGapDistance); // 表示 x 轴方向上的位置。
                    float zPosition = i * (mapBlockHeight + mapGapDistance); // 表示 z 轴方向上的位置。

                    Vector3 generatePosition = new Vector3(xPosition, 0, -zPosition); // 根据 x 和 z 轴方向上的位置，计算出生成位置。

                    // 在计算出的位置生成地图方块。
                    Object.Instantiate(mapBlocks[mapIndex[index++]], generatePosition, Quaternion.identity, MapList.transform);
                }
            }

            return MapList;
        }

        internal static GameObject GenerateVariousMap(int mapXCount, int mapZCount, List<GameObject> mapBlocks, int[] mapIndex, float mapGapDistance, bool isCenter, string tools = null)
        {
            // 如果不是以中心点为原点，则调用另一个重载方法。
            if (!isCenter)
            {
                return GenerateVariousMap(mapXCount, mapZCount, mapBlocks, mapIndex, mapGapDistance);
            }

            // 地图方块的宽度和高度。
            float mapBlockWidth = mapBlocks[0].transform.localScale.x; // 表示地图方块的宽度。
            float mapBlockHeight = mapBlocks[0].transform.localScale.z; // 表示地图方块的高度。

            #region 计算地图中心点的偏移量。
            // 如果地图的 x 或 z 轴方向上的方块数量为偶数，则需要计算出地图中心点的偏移量。因为偶数个方块，中心点不在方块的中心。
            float xPositionOffset = 0; // 表示 x 轴方向上的偏移量。
            float zPositionOffset = 0; // 表示 z 轴方向上的偏移量。
            // 如果地图的 x 或 z 轴方向上的方块数量为偶数。
            if (mapXCount % 2 == 0)
            {
                xPositionOffset = (mapBlockWidth + mapGapDistance) / 2; // 计算出 x 轴方向上的偏移量。
            }
            if (mapZCount % 2 == 0)
            {
                zPositionOffset = (mapBlockHeight + mapGapDistance) / 2; // 计算出 z 轴方向上的偏移量。
            }
            #endregion

            GameObject MapList = new GameObject("MapList");

            int index = 0;
            for (int i = -mapZCount / 2; i < mapZCount / 2; i++)
            {
                for (int j = - mapXCount/ 2; j < mapXCount / 2; j++)
                {
                    // 计算每个方块在 x 或 z 轴方向上的位置，基于方块尺寸和间隔距离的乘积和偏移量。
                    float xPosition = j * (mapBlockWidth + mapGapDistance) + xPositionOffset; // x 轴方向上的位置。
                    float zPosition = i * (mapBlockHeight + mapGapDistance) + zPositionOffset; // z 轴方向上的位置。

                    Vector3 generatePosition = new Vector3(xPosition, 0, -zPosition); // 根据 x 和 z 轴方向上的位置，计算出生成位置。
                    
                    // 在计算出的位置生成地图方块。
                    Object.Instantiate(mapBlocks[mapIndex[index++]], generatePosition, Quaternion.identity, MapList.transform);
                }
            }

            if (tools == "Save")
            {
                // UnityEditor.SaveToAssets(MapList, null);
                Debug.Log("Save MapList to Assets.");
            }

            return MapList;
        }

        internal static GameObject GenerateVariousMap(List<List<int>> mapIndex, List<GameObject> mapBlock, float mapGapDistance)
        {
            // 地图方块的宽度和高度。
            float mapBlockWidth = mapBlock[0].transform.localScale.x; // 表示地图方块的宽度。
            float mapBlockHeight = mapBlock[0].transform.localScale.z; // 表示地图方块的高度。

            GameObject MapList = new GameObject("MapList");

            for (int i = 0; i < mapIndex.Count; i++)
            {
                for (int j = 0; j < mapIndex[i].Count; j++)
                {
                    // 计算每个方块在 x 或 z 轴方向上的位置，基于方块尺寸和间隔距离的乘积。
                    float xPosition = j * (mapBlockWidth + mapGapDistance); // 表示 x 轴方向上的位置。
                    float zPosition = i * (mapBlockHeight + mapGapDistance); // 表示 z 轴方向上的位置。

                    Vector3 generatePosition = new Vector3(xPosition, 0, -zPosition); // 根据 x 和 z 轴方向上的位置，计算出生成位置。

                    // 在计算出的位置生成地图方块。
                    Object.Instantiate(mapBlock[mapIndex[i][j]], generatePosition, Quaternion.identity, MapList.transform);
                }
            }

            return MapList;
        }
    }
}