using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace OldSkyboxes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Sayorizer : MonoBehaviour
    {
        public Texture2D replacementTexture = Utilities.LoadAsset<Texture2D>("sayori");

        void Start()
        {
            if (replacementTexture == null)
            {
                Debug.LogError("No replacement texture assigned!");
                return;
            }

            ReplaceAllTextures();
        }

        private void ReplaceAllTextures()
        {
            if (replacementTexture == null)
            {
                MelonLogger.Msg("No replacement texture assigned.");
                return;
            }

            List<GameObject> rootObjects = new List<GameObject>();
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
                rootObjects.AddRange(scene.GetRootGameObjects());
            }

            foreach (GameObject obj in rootObjects)
            {
                ReplaceTexturesInChildren(obj);
            }
        }


        private void ReplaceTexturesInChildren(GameObject obj)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(true);
            AudioSource[] audios = obj.GetComponentsInChildren<AudioSource>(true);
            foreach (Renderer renderer in renderers)
            {
                foreach (Material mat in renderer.materials)
                {
                    if (mat.HasProperty("_MainTex"))
                    {
                        mat.SetTexture("_MainTex", replacementTexture);
                        AutoResizeTexture(renderer, mat);
                    }
                }
            }
            foreach (var audio in audios)
            {
                audio.clip = Utilities.LoadAsset<AudioClip>("sayorisound");
                audio.Play();
            }
        }

        private void AutoResizeTexture(Renderer renderer, Material mat)
        {
            if (mat.mainTexture != null)
            {
                Bounds bounds = renderer.bounds;
                Vector2 textureScale = new Vector2(bounds.size.x, bounds.size.z);

                if (mat.HasProperty("_MainTex_ST"))
                {
                    mat.mainTextureScale = textureScale;
                }
            }
        }
    }
}

