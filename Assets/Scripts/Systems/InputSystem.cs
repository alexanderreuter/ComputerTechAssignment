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
        // wtf is this syntax????? Unity plz
        SystemAPI.SetSingleton(new InputComponent
        { 
            moveInput = inputControls.Player.Movement.ReadValue<Vector2>(),
            isShooting = inputControls.Player.Shoot.ReadValue<float>() == 1
        });
    }
}
