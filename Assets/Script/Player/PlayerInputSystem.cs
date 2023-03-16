using Unity.Entities;
using Unity.Physics.Systems;
using UnityEngine;

[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(InitializationSystemGroup), OrderFirst = true)]
public partial class PlayerInputSystem : SystemBase {

    protected override void OnCreate() {
        base.OnCreate();
        EntityQuery entityQuery = GetEntityQuery(ComponentType.ReadWrite<PlayerInput>());
        RequireForUpdate(entityQuery);
    }

    protected override void OnUpdate() {
        RefRW<PlayerInput> playerInput = SystemAPI.GetSingletonRW<PlayerInput>();

        playerInput.ValueRW.horizontalAxis = 0f;
        playerInput.ValueRW.horizontalAxis += Input.GetKey(KeyCode.A) ? -1f : 0f;
        playerInput.ValueRW.horizontalAxis += Input.GetKey(KeyCode.D) ? 1f : 0f;

        playerInput.ValueRW.wasSpaceButtonClicked = Input.GetKeyDown(KeyCode.Space);
    }


}


[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(InitializationSystemGroup))]
[UpdateAfter(typeof(PlayerInputSystem))]
public partial struct PlayerControlSystem : ISystem {

    public void OnCreate(ref SystemState state) {
        EntityQuery entityQuery = SystemAPI.QueryBuilder().WithAll<PlayerInput, PlayerController>().Build();
        state.RequireForUpdate(entityQuery);
    }

    public void OnUpdate(ref SystemState state) {

        PlayerController playerController = SystemAPI.GetSingleton<PlayerController>();

        if(SystemAPI.HasComponent<ShipHandlingControl>(playerController.controlledEntity)) {
            PlayerInput playerInput = SystemAPI.GetSingleton<PlayerInput>();
            
            ShipHandlingControl shipHandlingControl = SystemAPI.GetComponent<ShipHandlingControl>(playerController.controlledEntity);
            shipHandlingControl.horizontalMovement = playerInput.horizontalAxis;
            shipHandlingControl.hasWeaponFired = playerInput.wasSpaceButtonClicked;

            SystemAPI.SetComponent(playerController.controlledEntity, shipHandlingControl);
        }

    }

}