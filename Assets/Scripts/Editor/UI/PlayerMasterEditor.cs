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

        var ref_stats = root.Q<ObjectField>("of-stats");
        var ref_controller = root.Q<ObjectField>("of-controller");
        var ref_animations = root.Q<ObjectField>("of-animations");

        ref_stats.objectType = typeof(PlayerStats);
        ref_controller.objectType = typeof(PlayerController);
        ref_animations.objectType = typeof(PlayerAnimations);

        ref_stats.BindProperty(serializedObject.FindProperty("REF_PlayerStats"));
        ref_controller.BindProperty(serializedObject.FindProperty("REF_PlayerController"));
        ref_animations.BindProperty(serializedObject.FindProperty("REF_PlayerAnimations"));

        return root;
    }
}
