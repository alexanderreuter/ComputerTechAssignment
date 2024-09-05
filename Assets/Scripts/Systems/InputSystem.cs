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
        foreach (var input in SystemAPI.Query<RefRW<InputComponent>>())
        {
            input.ValueRW.moveInput = inputControls.Player.Movement.ReadValue<Vector2>();
            input.ValueRW.isShooting = inputControls.Player.Shoot.ReadValue<float>() > 0;
        }
    }
}
