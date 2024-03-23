using UnityEditor;
using UnityEngine;

namespace Y9g
{
    public sealed class UnityEditor
    {
        internal static bool SaveToAssets(GameObject gameObj, string prefabPath = null)
        {
            // 检查GameObject是否为空
            if (gameObj == null)
            {
                Debug.LogError("GameObject is null.");
                return false;
            }

            // 如果prefabPath为空，则使用默认路径
            if (prefabPath == null)
            {
                string randomName = System.Guid.NewGuid().ToString();
                prefabPath = "Assets/Temp/" + randomName + ".prefab";
            }
            
            // 使用PrefabUtility.SaveAsPrefabAsset方法保存GameObject为预制体
            PrefabUtility.SaveAsPrefabAsset(gameObj, prefabPath);

            // 保存Mesh
            if (gameObj.GetComponent<MeshFilter>() != null)
            {
                Mesh mesh = gameObj.GetComponent<MeshFilter>().sharedMesh;
                string meshPath = prefabPath.Replace(".prefab", ".asset");
                AssetDatabase.CreateAsset(mesh, meshPath);
            }

            // 检查预制体是否保存成功
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) == null)
            {
                Debug.LogError("Save prefab failed.");
                return false;
            }

            return true;
        }
    }
}