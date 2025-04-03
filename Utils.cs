using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OldSkyboxes
{
    // Token: 0x02000016 RID: 22
    public static class Utilities
    {
        // Token: 0x06000066 RID: 102 RVA: 0x00004804 File Offset: 0x00002A04
        public static GameObject[] GetChilds(this GameObject obj)
        {
            GameObject[] array = new GameObject[obj.transform.childCount];
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                array[i] = obj.transform.GetChild(i).gameObject;
            }
            return array;
        }

        // Token: 0x06000067 RID: 103 RVA: 0x00004850 File Offset: 0x00002A50
        public static Transform GetChildWithName(this Transform tr, string name)
        {
            foreach (GameObject gameObject in tr.gameObject.GetChilds())
            {
                if (gameObject.name == name)
                {
                    return gameObject.transform;
                }
            }
            return null;
        }

        // Token: 0x06000068 RID: 104 RVA: 0x00004894 File Offset: 0x00002A94
        public static GameObject GetChildWithName(this GameObject obj, string name)
        {
            foreach (GameObject gameObject in obj.GetChilds())
            {
                if (gameObject.name == name)
                {
                    return gameObject;
                }
            }
            return null;
        }

        // Token: 0x06000069 RID: 105 RVA: 0x000048CC File Offset: 0x00002ACC
        public static GameObject GetChildAt(this GameObject obj, string path)
        {
            string[] array = path.Split('/', StringSplitOptions.None);
            GameObject gameObject = obj;
            foreach (string text in array)
            {
                if (text == "..")
                {
                    gameObject = gameObject.transform.parent.gameObject;
                }
                else
                {
                    gameObject = gameObject.GetChildWithName(text);
                }
            }
            return gameObject;
        }
        public static T LoadAsset<T>(string AssetName) where T : Object
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{Assembly.GetCallingAssembly().GetName().Name}.Resources.{Assembly.GetCallingAssembly().GetName().Name}");
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes);
            Il2CppAssetBundle il2cppAssetBundle = Il2CppAssetBundleManager.LoadFromMemory(bytes);
            T returningAsset = il2cppAssetBundle.Load<T>(AssetName);
            returningAsset.hideFlags = HideFlags.DontUnloadUnusedAsset;
            il2cppAssetBundle.Unload(false);
            return returningAsset;
        }
    }
}
