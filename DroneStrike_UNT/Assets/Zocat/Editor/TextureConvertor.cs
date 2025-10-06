using UnityEditor;
using UnityEngine;

namespace Zocat
{
    public class TextureConvertor
    {
        [MenuItem("Tools/Textures/Set Selected -> ETC2 RGBA Crunched (Android+iOS) _F6")]
        public static void SetSelected()
        {
            foreach (var obj in Selection.objects)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                if (tex == null) continue;

                var importer = AssetImporter.GetAtPath(path) as TextureImporter;
                if (importer == null) continue;

                importer.textureCompression = TextureImporterCompression.CompressedHQ;
                importer.alphaSource = TextureImporterAlphaSource.FromInput;
                importer.crunchedCompression = true;
                importer.compressionQuality = 50;

                // 🔹 Sprite Mode'u Single yap
                importer.textureType = TextureImporterType.Sprite;
                importer.spriteImportMode = SpriteImportMode.Single;

                // 🔹 Android platform ayarları
                var androidSettings = new TextureImporterPlatformSettings
                {
                    name = "Android",
                    overridden = true,
                    maxTextureSize = importer.maxTextureSize,
                    format = TextureImporterFormat.ETC2_RGBA8Crunched,
                    compressionQuality = 50
                };
                importer.SetPlatformTextureSettings(androidSettings);

                // 🔹 iOS platform ayarları
                var iosSettings = new TextureImporterPlatformSettings
                {
                    name = "iPhone",
                    overridden = true,
                    maxTextureSize = importer.maxTextureSize,
                    format = TextureImporterFormat.ETC2_RGBA8Crunched,
                    compressionQuality = 50
                };
                importer.SetPlatformTextureSettings(iosSettings);

                importer.SaveAndReimport();

                Debug.Log($"ETC2 RGBA8Crunched + Sprite(Single) uygulandı (Android + iOS): {path}");
            }
        }
    }
}