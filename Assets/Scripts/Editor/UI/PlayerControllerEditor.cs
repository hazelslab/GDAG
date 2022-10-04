using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    private PlayerController controller;

    private VisualElement rootElem;
    private VisualTreeAsset visualTree;

    private void OnEnable()
    {
        controller = target as PlayerController;
        rootElem = new VisualElement();
        visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/UI/PlayerController.uxml");
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = rootElem;
        root.Clear();
        visualTree.CloneTree(root);

        Color enabledColor = new Color(.55f, .85f, .50f);
        Color disabledColor = new Color(.85f, .55f, .50f);

        #region Components
        var label_movement = root.Q<Label>("label-movement");
        var label_jump = root.Q<Label>("label-jump");

        var content_movement = root.Q<VisualElement>("content-movement");
        var content_jump = root.Q<VisualElement>("content-jump");

        var toggle_move = root.Q<Toggle>("toggle-move");
        toggle_move.BindProperty(serializedObject.FindProperty("canMove"));
        var toggle_jump = root.Q<Toggle>("toggle-jump");
        toggle_jump.BindProperty(serializedObject.FindProperty("canJump"));

        var atr_crouchSpeed = root.Q<Slider>("crouchSpeed");
        atr_crouchSpeed.BindProperty(serializedObject.FindProperty("crouchSpeed"));
        var atr_walkSpeed = root.Q<Slider>("walkSpeed");
        atr_walkSpeed.BindProperty(serializedObject.FindProperty("walkSpeed"));
        var atr_runSpeed = root.Q<Slider>("runSpeed");
        atr_runSpeed.BindProperty(serializedObject.FindProperty("runSpeed"));
        var atr_acceleration = root.Q<Slider>("acceleration");
        atr_acceleration.BindProperty(serializedObject.FindProperty("acceleration"));
        var atr_deceleration = root.Q<Slider>("deceleration");
        atr_deceleration.BindProperty(serializedObject.FindProperty("deceleration"));
        var atr_airAcceleration = root.Q<Slider>("airAcceleration");
        atr_airAcceleration.BindProperty(serializedObject.FindProperty("airAccel"));
        var atr_airDeceleration = root.Q<Slider>("airDeceleration");
        atr_airDeceleration.BindProperty(serializedObject.FindProperty("airDecel"));
        var atr_velPower = root.Q<Slider>("velPower");
        atr_velPower.BindProperty(serializedObject.FindProperty("velPower"));
        var atr_friction = root.Q<Slider>("friction");
        atr_friction.BindProperty(serializedObject.FindProperty("friction"));

        var atr_jumpForce = root.Q<Slider>("jumpForce");
        atr_jumpForce.BindProperty(serializedObject.FindProperty("jumpForce"));
        var atr_jumpCutMultiplier = root.Q<Slider>("jumpCutMultiplier");
        atr_jumpCutMultiplier.BindProperty(serializedObject.FindProperty("jumpCutMultiplier"));
        var atr_fallGravityMultiplier = root.Q<Slider>("fallGravityMultiplier");
        atr_fallGravityMultiplier.BindProperty(serializedObject.FindProperty("fallGravityMultiplier"));
        var atr_jumpCoyoteTime = root.Q<Slider>("jumpCoyoteTime");
        atr_jumpCoyoteTime.BindProperty(serializedObject.FindProperty("jumpCoyoteTime"));
        var atr_jumpBufferTime = root.Q<Slider>("jumpBufferTime");
        atr_jumpBufferTime.BindProperty(serializedObject.FindProperty("jumpBufferTime"));

        var atr_groundCheckPoint = root.Q<ObjectField>("groundCheckPoint");
        atr_groundCheckPoint.BindProperty(serializedObject.FindProperty("groundCheckPoint"));
        var atr_groundCheckSize = root.Q<Vector2Field>("groundCheckSize");
        atr_groundCheckSize.BindProperty(serializedObject.FindProperty("groundCheckSize"));

        var atr_walkingParticles = root.Q<ObjectField>("walkingParticles");
        atr_walkingParticles.BindProperty(serializedObject.FindProperty("_walkingDust"));

        var atr_groundLayer = root.Q<LayerMaskField>("groundLayer");
        atr_groundLayer.BindProperty(serializedObject.FindProperty("groundLayer"));

        var atr_showMoveTrail = root.Q<Toggle>("showMoveTrail");
        atr_showMoveTrail.BindProperty(serializedObject.FindProperty("showMoveTrail"));
        #endregion

        toggle_move.RegisterValueChangedCallback(evt =>
        {
            content_movement.SetEnabled(evt.newValue);
            if (evt.newValue)
            {
                content_movement.style.display = DisplayStyle.Flex;
                label_movement.style.color = enabledColor;
            }
            else if (!evt.newValue)
            {
                content_movement.style.display = DisplayStyle.None;
                label_movement.style.color = disabledColor;
            }
        });
        toggle_jump.RegisterValueChangedCallback(evt =>
        {
            content_jump.SetEnabled(evt.newValue);
            if (evt.newValue)
            {
                content_jump.style.display = DisplayStyle.Flex;
                label_jump.style.color = enabledColor;
            }
            else if (!evt.newValue)
            {
                content_jump.style.display = DisplayStyle.None;
                label_jump.style.color = disabledColor;
            }
        });

        return root;
    }
}
