using MelonLoader;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Reflection;
using BooksAndFractals;

[assembly: MelonInfo(typeof(OldSkyboxes.Core), "Old Skyboxes", "1.1.1", "sangoodman", null)]
[assembly: MelonGame("Haze Games", "Fractal Space")]
[assembly: MelonColor(255, 190, 124, 103)]
[assembly: MelonAuthorColor(255, 128, 128, 128)]

namespace OldSkyboxes
{
    public class Core : MelonMod
    {
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasLoaded(buildIndex, sceneName);
            if(buildIndex > 2)
            {
                ChangeSkybox(buildIndex);
            }     
        }

        private void ChangeSkybox(int buildIndex)
        {
            switch (buildIndex)
            {
                case 1:
                    RenderSettings.skybox = LoadAsset<Material>("Menu_and_Chapter_4");
                    break;
                case 3:
                    RenderSettings.skybox = LoadAsset<Material>("Chapter_1");
                    GameObject[] planets_ch1 = ObjectManipulation.GetChildrenOfObject(GameObject.Find("Planets"));
                    if (planets_ch1 != null)
                    {
                        foreach (var planet in planets_ch1)
                        {
                            planet.SetActive(false);
                        }
                    }
                    
                    break;
                case 4:
                    RenderSettings.skybox = LoadAsset<Material>("Chapter_2");
                    GameObject[] planets_ch2 = ObjectManipulation.GetChildrenOfObject(GameObject.Find("Planets"));
                    if (planets_ch2 != null)
                    {
                        foreach (var planet in planets_ch2)
                        {
                            planet.SetActive(false);
                        }
                    }
                    break;
                case 5:
                    RenderSettings.skybox = LoadAsset<Material>("Chapter_3");
                    GameObject[] planets_ch3 = ObjectManipulation.GetChildrenOfObject(GameObject.Find("Planets"));
                    if (planets_ch3 != null)
                    {
                        foreach (var planet in planets_ch3)
                        {
                            planet.SetActive(false);
                        }
                    }
                    break;
                case 6:
                    RenderSettings.skybox = LoadAsset<Material>("Menu_and_Chapter_4");
                    GameObject[] planets_ch4 = ObjectManipulation.GetChildrenOfObject(GameObject.Find("Planets"));
                    if (planets_ch4 != null)
                    {
                        foreach (var planet in planets_ch4)
                        {
                            planet.SetActive(false);
                        }
                    }
                    break;
            }
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