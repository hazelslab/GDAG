using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(PlayerStats))]
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
        var slider_currentStamina = root.Q<Slider>("slider-currentStamina");
        slider_currentStamina.SetEnabled(false);
        var slider_maxStamina = root.Q<Slider>("slider-maxStamina");
        var slider_drainMultiplier = root.Q<Slider>("slider-drainMultiplier");
        var slider_walkRegenMultiplier = root.Q<Slider>("slider-walkRegenMultiplier");
        var slider_idleRegenMultiplier = root.Q<Slider>("slider-idleRegenMultiplier");
        var slider_timeUntilStaminaRegen = root.Q<Slider>("slider-timeUntilStaminaRegen");
        var slider_currentTimeUntilStaminaRegen = root.Q<Slider>("slider-currentTimeUntilStaminaRegen");
        slider_currentTimeUntilStaminaRegen.SetEnabled(false);

        //Bindings
        slider_currentStamina.BindProperty(serializedObject.FindProperty("_currentStamina"));
        slider_maxStamina.BindProperty(serializedObject.FindProperty("_maxStamina"));
        slider_drainMultiplier.BindProperty(serializedObject.FindProperty("_drainMultiplier"));
        slider_walkRegenMultiplier.BindProperty(serializedObject.FindProperty("_walkRegenMultiplier"));
        slider_idleRegenMultiplier.BindProperty(serializedObject.FindProperty("_idleRegenMultiplier"));
        slider_timeUntilStaminaRegen.BindProperty(serializedObject.FindProperty("_timeUntilStaminaRegen"));
        slider_currentTimeUntilStaminaRegen.BindProperty(serializedObject.FindProperty("_timeUntilStaminaRegen_timer"));

        //Events
        slider_maxStamina.RegisterValueChangedCallback(evt =>
        {
            slider_currentStamina.highValue = slider_maxStamina.value;
            slider_currentStamina.value = slider_maxStamina.value;
        });
        slider_timeUntilStaminaRegen.RegisterValueChangedCallback(evt =>
        {
            slider_currentTimeUntilStaminaRegen.highValue = slider_timeUntilStaminaRegen.value;
        });


        return root;
    }


}
