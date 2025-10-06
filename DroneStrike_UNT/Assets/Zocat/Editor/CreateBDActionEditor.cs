using System.Collections;
using UnityEngine;

namespace Zocat
{
    using UnityEngine;
    using UnityEditor;
    using System.IO;

    public class CreateBDActionEditor : EditorWindow
    {
        private int selectionIndex = 0;
        private string[] options = { "Action", "Condition" };
        private string className = "";
        private string category = "Zocat";
        private string savePath = "Assets/Zocat/Runtime/Movie/Scripts/Modules/Opsive/";


        [MenuItem("Tools/BD Action Creator")]
        public static void ShowWindow()
        {
            GetWindow<CreateBDActionEditor>("BD Action Creator");
        }

        private void OnGUI()
        {
            /*--------------------------------------------------------------------------------------*/
            for (int i = 0; i < options.Length; i++)
            {
                bool isSelected = selectionIndex == i;
                bool newSelection = GUILayout.Toggle(isSelected, options[i], "Radio");

                if (newSelection && !isSelected)
                {
                    selectionIndex = i;
                    Debug.Log("Seçilen: " + options[i]);
                }
            }

            /*--------------------------------------------------------------------------------------*/
            // GUILayout.Label("Yeni Behavior Designer Action Oluştur", EditorStyles.boldLabel);
            className = EditorGUILayout.TextField("Class Name", className);
            category = EditorGUILayout.TextField("Task Category", category);
            // Object selected = Selection.activeObject;
            // savePath = EditorGUILayout.TextField("Save Path", selected != null ? AssetDatabase.GetAssetPath(selected) : savePath);
            savePath = EditorGUILayout.TextField("Save Path", savePath);
            // savePath = AssetDatabase.GetAssetPath(selected);

            if (GUILayout.Button("Create"))
            {
                CreateScript();
            }
        }

        private void CreateScript()
        {
            if (string.IsNullOrEmpty(className))
            {
                Debug.LogError("Class adı boş olamaz.");
                return;
            }

            // string script = GetActionTemplate(className, category);
            var scr = selectionIndex == 0 ? GetActionTemplate(className, category) : GetConditionalTemplate(className, category);
            string script = GetConditionalTemplate(className, category);
            string fullPath = Path.Combine(savePath, className + ".cs");

            Directory.CreateDirectory(savePath);
            File.WriteAllText(fullPath, scr);
            AssetDatabase.Refresh();

            // Debug.Log("Yeni BD Action oluşturuldu: " + fullPath);
        }

        private string GetActionTemplate(string className, string category)
        {
            return $@"
using Opsive.BehaviorDesigner.Runtime.Tasks;
using Opsive.BehaviorDesigner.Runtime.Tasks.Actions;
using Opsive.GraphDesigner.Runtime.Variables;
using UnityEngine;
namespace Zocat
{{
public class {className} : ActionBase
{{
    public SharedVariable<GameObject> targetObject;

    public override void OnAwake()
    {{
     
    }}

    public override void OnStart()
    {{
       
    }}

    public override TaskStatus OnUpdate()
    {{
        return TaskStatus.Running;
    }}

    public override void OnEnd()
    {{
     
    }}
}}
}}";
        }

        private string GetConditionalTemplate(string className, string category)
        {
            return $@"
using Opsive.BehaviorDesigner.Runtime.Tasks;
using Opsive.BehaviorDesigner.Runtime.Tasks.Conditionals;
using Opsive.GraphDesigner.Runtime.Variables;
using UnityEngine;
namespace Zocat
{{
public class {className} : Conditional
{{
 

    public override void OnAwake()
    {{
     
    }}

    public override void OnStart()
    {{
       
    }}

    public override TaskStatus OnUpdate()
    {{
       
    }}
}}
}}";
        }
    }
}