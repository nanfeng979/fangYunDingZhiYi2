using System.Collections.Generic;
using UnityEngine;

namespace Y9g
{
    public sealed class Utils
    {
        /// <summary>
        /// 16进制颜色转换为Color
        /// </summary>
        /// <param name="hex"> 16进制颜色 </param>
        /// <returns> Color 值 </returns>
        public static Color HexToColor(string hex)
        {
            Color color = Color.white;
            if (ColorUtility.TryParseHtmlString(hex, out color))
            {
                return color;
            }
            else
            {
                Debug.LogError("HexToColor: " + hex);
                return Color.white;
            }
        }

        /// <summary>
        /// 用于二维数组，根据 direction 方向，判断下一个位置是否越界。
        /// </summary>
        /// <param name="array"> 二维数组 </param>
        /// <param name="currentIndex"> 当前位置 </param>
        /// <param name="direction"> 方向 </param>
        /// <returns> 是否越界 </returns>
        public static bool IsCrossTheBorderBy2DArray(ref List<List<int>> array, Vector2Int currentIndex, int direction)
        {
            Vector2Int nextIndex = currentIndex;

            switch (direction)
            {
                case 0:
                    nextIndex.x--;
                    break;
                case 1:
                    nextIndex.x++;
                    break;
                case 2:
                    nextIndex.y--;
                    break;
                case 3:
                    nextIndex.y++;
                    break;
            }

            if (nextIndex.x <= 0 || nextIndex.x > array.Count || nextIndex.y <= 0 || nextIndex.y > array[0].Count)
            {
                return true;
            }

            return false;
        }

        public static bool IsCrossContainsList(ref List<List<int>> array, Vector2Int currentIndex, int direction, List<int> containsList)
        {
            Vector2Int nextIndex = currentIndex;

            switch (direction)
            {
                case 0:
                    nextIndex.x--;
                    break;
                case 1:
                    nextIndex.x++;
                    break;
                case 2:
                    nextIndex.y--;
                    break;
                case 3:
                    nextIndex.y++;
                    break;
            }

            if (containsList.Contains(array[nextIndex.x - 1][nextIndex.y - 1]))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 将百分比字符串转换为浮点数
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static float PercentToFloat(string percent)
        {
            return float.Parse(percent.Replace("%", "")) / 100;
        }

    }

    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = (T) this;
                DontDestroyOnLoad(gameObject);

                AwakeCreateSingleTonLate();
            }
            else
            {
                Debug.LogWarning("单例模式，已经存在一个实例。");
                Destroy(gameObject);
            }
        }

        protected virtual void AwakeCreateSingleTonLate() { }

        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }

    public abstract class Singleton_sub<T> : MonoBehaviour where T : Singleton_sub<T>
    {
        public static T Instance { get; private set; }

        protected void Awake()
        {
            Instance = (T) this;
        }
    }

    /// <summary>
    /// 单例观察者。
    /// </summary>
    /// <typeparam name="T"> 单例类型 </typeparam>
    public abstract class SingletonObserver<T> : Singleton<T>, ISubject where T : SingletonObserver<T>
    {
        /// <summary>
        /// 观察者列表。
        /// </summary>
        protected List<IObserver> observers = new List<IObserver>();

        /// <summary>
        /// 注册观察者。
        /// </summary>
        /// <param name="observer"> 观察者 </param>
        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        /// 注销观察者。
        /// </summary>
        /// <param name="observer"> 观察者 </param>
        public void UnregisterObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        /// <summary>
        /// 通知观察者。
        /// </summary>
        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.UpdateObserver();
            }
        }
    }

    /// <summary>
    /// 主题接口。
    /// </summary>
    public interface ISubject
    {
        void RegisterObserver(IObserver observer);

        void UnregisterObserver(IObserver observer);

        void NotifyObservers();
    }

    /// <summary>
    /// 观察者接口。
    /// </summary>
    public interface IObserver
    {
        void UpdateObserver();
    }
}