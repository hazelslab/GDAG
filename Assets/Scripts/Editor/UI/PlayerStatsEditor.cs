using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

//[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{

    private PlayerStats _stats;

    private VisualElement _rootElem;
    private VisualTreeAsset _visualTree;

    private void OnEnable()
    {
        _stats = target as PlayerStats;

        _rootElem = new VisualElement();

        _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/UI/PlayerStats.uxml");
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = _rootElem;
        root.Clear();
        _visualTree.CloneTree(root);

        //Components
        var button_fillStamina = root.Q<Button>("button-fillStamina");
        var slider_currentStamina = root.Q<Slider>("slider-currentStamina");
        slider_currentStamina.SetEnabled(false);
        var slider_maxStamina = root.Q<Slider>("slider-maxStamina");
        var slider_drainMultiplier = root.Q<Slider>("slider-drainMultiplier");

        //Bindings
        slider_currentStamina.BindProperty(serializedObject.FindProperty("_currentStamina"));
        slider_maxStamina.BindProperty(serializedObject.FindProperty("_maxStamina"));
        slider_drainMultiplier.BindProperty(serializedObject.FindProperty("_drainMultiplier"));

        //Events
        slider_maxStamina.RegisterValueChangedCallback(evt =>
        {
            slider_currentStamina.highValue = slider_maxStamina.value;
        });
        button_fillStamina.clicked += () =>
        {
            slider_currentStamina.value = slider_currentStamina.highValue;
        };


        return root;
    }


}
