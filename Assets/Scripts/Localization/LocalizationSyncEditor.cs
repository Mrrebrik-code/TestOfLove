using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace Assets.SimpleLocalization.Editor
{
	/// <summary>
	/// Adds "Sync" button to LocalizationSync script.
	/// </summary>
	[CustomEditor(typeof(LocalizationSync))]
    public class LocalizationSyncEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var component = (LocalizationSync) target;

            if (GUILayout.Button("Sync"))
            {
	            component.Sync();
            }
            if (GUILayout.Button("Complet"))
            {
                OpenWindow2();
            }

        }

        [MenuItem("Window/Localization Update", false, 13)]
        static void OpenWindow()
        {
            var component = GameObject.Find("LocalizationSync").GetComponent<LocalizationSync>();
            component.Sync();
        }

        [MenuItem("Window/LocalizationComplet", false, 13)]
        static void OpenWindow2()
        {
            var localization = Localization.Instance; /*GameObject.Find("[Localization]").GetComponent<Localization>();*/

			foreach (var key in LocalizationManager.Dictionary.Values)
			{
                Debug.Log(LocalizationManager.Dictionary.Keys);
				foreach (var item in key.Keys)
				{
                    Debug.Log(key.Values);
                    Debug.Log(item);
                    var ru = LocalizationManager.Dictionary["Russian"][item];
                    var en = LocalizationManager.Dictionary["English"][item];
                    var tr = LocalizationManager.Dictionary["Turkish"][item];
                    var massa = "";

                    if (LocalizationManager.Dictionary["Massa"].ContainsKey(item))
					{
                        massa  = LocalizationManager.Dictionary["Massa"][item];
                    }
                   
                    Debug.Log(ru);

                    var group = new Localization.LocalizationObject.Group(ru, en, tr, massa);

					var localizationGroup = new Localization.LocalizationObject(item, group);

					localization.LocalizationList.Add(localizationGroup);
				}
                

            }
        }
    }
}
#endif