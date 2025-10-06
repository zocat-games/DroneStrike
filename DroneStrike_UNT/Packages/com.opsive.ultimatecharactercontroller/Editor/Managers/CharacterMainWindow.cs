﻿/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Editor.Managers
{
    using Opsive.Shared.Editor.Managers;
    using Opsive.UltimateCharacterController.Utility;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
#if !OPSIVE_IMPORT_DEBUG
    using Opsive.Shared.Editor.Import;
    using System.IO;
#endif
    /// <summary>
    /// The MainManagerWindow is an editor window which contains all of the sub managers. This window draws the high level menu options and draws
    /// the selected sub manager.
    /// </summary>
    [InitializeOnLoad]
    public class CharacterMainWindow : MainManagerWindow
    {
#if !OPSIVE_IMPORT_DEBUG
        public const string c_ImportStatusPath = "Assets/Opsive/ImportStatus.asset";
#endif
        protected override string AssetName => AssetInfo.Name;
        protected override string AssetVersion => AssetInfo.Version;
        protected override string UpdateCheckURL => string.Format("https://opsive.com/asset/UpdateCheck.php?asset=UltimateCharacterController&type={0}&version={1}&unityversion={2}&devplatform={3}&targetplatform={4}",
                                            AssetInfo.Name.Replace(" ", ""), AssetInfo.Version, Application.unityVersion, Application.platform, EditorUserBuildSettings.activeBuildTarget);
        protected override string LatestVersionKey => "Opsive.UltimateCharacterController.Editor.LatestVersion";
        protected override string LastUpdateCheckKey => "Opsive.UltimateCharacterController.Editor.LastUpdateCheck";
        protected override string ManagerNamespace => "Opsive.UltimateCharacterController.Editor";

        /// <summary>
        /// Perform editor checks as soon as the scripts are done compiling.
        /// </summary>
        static CharacterMainWindow()
        {
            EditorApplication.update += EditorStartup;
        }

        /// <summary>
        /// The window has been enabled.
        /// </summary>
        protected override void OnEnable()
        {
            rootVisualElement.styleSheets.Add(Shared.Editor.Utility.EditorUtility.LoadAsset<StyleSheet>("a46bc2a887de04846a522116cc71dd3b")); // Controller stylesheet.

            base.OnEnable();
        }

        /// <summary>
        /// Initializes the Main Manager.
        /// </summary> 
        [MenuItem("Tools/Opsive/Ultimate Character Controller/Main Manager", false, 1)]
        public static MainManagerWindow ShowWindow()
        {
            var window = EditorWindow.GetWindow<CharacterMainWindow>(false, "Character Manager");
            window.minSize = new Vector2(680, 650);
            return window;
        }

        /// <summary>
        /// Initializes the Setup Manager.
        /// </summary> 
        [MenuItem("Tools/Opsive/Ultimate Character Controller/Setup Manager", false, 11)]
        public static void ShowSetupManagerWindow()
        {
            var window = ShowWindow();
            window.Open(typeof(SetupManager));
        }

        /// <summary>
        /// Initializes the Main Manager and shows the Character Manager.
        /// </summary> 
        [MenuItem("Tools/Opsive/Ultimate Character Controller/Character Manager", false, 12)]
        public static void ShowCharacterManagerWindow()
        {
            var window = ShowWindow();
            window.Open(typeof(CharacterManager));
        }

        /// <summary>
        /// Initializes the Main Manager and shows the Item Type Manager.
        /// </summary> 
        [MenuItem("Tools/Opsive/Ultimate Character Controller/Item Type Manager", false, 13)]
        public static void ShowItemTypeManagerWindow()
        {
            var window = ShowWindow();
            window.Open(typeof(ItemTypeManager));
        }

        /// <summary>
        /// Initializes the Main Manager and shows the Item Manager.
        /// </summary> 
        [MenuItem("Tools/Opsive/Ultimate Character Controller/Item Manager", false, 14)]
        public static void ShowItemManagerWindow()
        {
            var window = ShowWindow();
            window.Open(typeof(ItemManager));
        }

        /// <summary>
        /// Initializes the Main Manager and shows the Object Manager.
        /// </summary> 
        [MenuItem("Tools/Opsive/Ultimate Character Controller/Object Manager", false, 15)]
        public static void ShowObjectManagerWindow()
        {
            var window = ShowWindow();
            window.Open(typeof(ObjectManager));
        }

        /// <summary>
        /// Initializes the Main Manager and shows the Animation Replacer
        /// </summary> 
        /// <returns>A reference to the opened Animation Replacer window.</returns>
        [MenuItem("Tools/Opsive/Ultimate Character Controller/Animation Replacer", false, 16)]
        public static AnimationReplacer ShowAnimationReplacerWindow()
        {
            var window = ShowWindow();
            return (AnimationReplacer)window.Open(typeof(AnimationReplacer));
        }

        /// <summary>
        /// Initializes the Main Manager and shows the Migration Manager.
        /// </summary> 
        public static void ShowMigrationManagerWindow()
        {
            var window = ShowWindow();
            window.Open(typeof(MigrationManager));
        }

        /// <summary>
        /// Initializes the Main Manager and shows the Integrations Manager.
        /// </summary> 
        [MenuItem("Tools/Opsive/Ultimate Character Controller/Integrations Manager", false, 36)]
        public static void ShowIntegrationsManagerWindow()
        {
            var window = ShowWindow();
            window.Open(typeof(IntegrationsManager));
        }

        /// <summary>
        /// Initializes the Main Manager and shows the Add-Ons Manager.
        /// </summary> 
        [MenuItem("Tools/Opsive/Ultimate Character Controller/Add-Ons Manager", false, 37)]
        public static void ShowAddOnsManagerWindow()
        {
            var window = ShowWindow();
            window.Open(typeof(AddOnsManager));
        }

        /// <summary>
        /// Show the editor window if it hasn't been shown before and also setup.
        /// </summary>
        private static void EditorStartup()
        {
            if (EditorApplication.isCompiling) {
                return;
            }
            EditorApplication.update -= EditorStartup;

#if !OPSIVE_IMPORT_DEBUG
            AssetDatabase.Refresh();
            ImportStatus importStatus = null;
            var importStatusAssets = AssetDatabase.FindAssets("t:ImportStatus");
            if (importStatusAssets != null && importStatusAssets.Length > 0) {
                for (int i = 0; i < importStatusAssets.Length; ++i) {
                    var path = AssetDatabase.GUIDToAssetPath(importStatusAssets[i]);
                    if (string.IsNullOrEmpty(path)) {
                        path = importStatusAssets[i];
                    }
                    importStatus = AssetDatabase.LoadAssetAtPath(path, typeof(ImportStatus)) as ImportStatus;
                    if (importStatus != null) {
                        break;
                    }
                }
            }
            if (importStatus == null) {
                // The import status hasn't been created yet. Create it in the same location as the Opsive folder.
                importStatus = ScriptableObject.CreateInstance<ImportStatus>();
                Directory.CreateDirectory(Path.GetDirectoryName(c_ImportStatusPath));
                AssetDatabase.CreateAsset(importStatus, c_ImportStatusPath);
                AssetDatabase.Refresh();
            }

            if (!importStatus.CharacterProjectSettingsShown) {
                var window = ShowWindow();
                var setupManager = window.Open(typeof(SetupManager));
                (setupManager as SetupManager).OpenProjectSetup();

                importStatus.CharacterProjectSettingsShown = true;
                UnityEditor.EditorUtility.SetDirty(importStatus);
            }
#endif
        }
    }
}