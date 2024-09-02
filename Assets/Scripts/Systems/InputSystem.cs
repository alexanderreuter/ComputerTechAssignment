using Unity.Entities;
using UnityEngine;

public partial class InputSystem : SystemBase
{
    private InputControls inputControls;

    protected override void OnCreate()
    {
        inputControls = new InputControls();
        inputControls.Enable();
    }

    protected override void OnUpdate()
    {
        foreach (var inputComponent in SystemAPI.Query<RefRW<InputComponent>>())
        {
            inputComponent.ValueRW.moveInput = inputControls.Player.Newaction.ReadValue<Vector2>();
        }
    }
}
