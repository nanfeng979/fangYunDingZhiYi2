using System;

namespace Y9g
{
    public sealed partial class Utils
    {
        /// <summary>
        /// 通过字符串实例化类
        /// </summary>
        /// <typeparam name="T"> 指定的类型类型 </typeparam>
        /// <param name="className"> 类的名字 </param>
        /// <param name="objects"> 参数集合 </param>
        /// <returns> 实例化后的对象 </returns>
        public static T InstanceClassByString<T>(string className, object[] objects)
        {
            return (T)Activator.CreateInstance(Type.GetType(className), objects);
        }
    }
}