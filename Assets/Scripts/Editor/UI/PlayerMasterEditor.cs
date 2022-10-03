using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(PlayerMaster))]
public class PlayerMasterEditor : Editor
{
    private PlayerMaster master;

    private VisualElement rootElem;
    private VisualTreeAsset visualTree;

    private void OnEnable()
    {
        master = target as PlayerMaster;

        rootElem = new VisualElement();

        visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/UI/PlayerMaster.uxml");
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = rootElem;
        root.Clear();
        visualTree.CloneTree(root);

        var ref_controller = root.Q<ObjectField>("of-controller");

        ref_controller.objectType = typeof(PlayerController);

        ref_controller.BindProperty(serializedObject.FindProperty("REF_PlayerController"));

        return root;
    }
}
