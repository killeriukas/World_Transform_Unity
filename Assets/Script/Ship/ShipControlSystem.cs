using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateBefore(typeof(PhysicsSystemGroup))]
public partial struct ShipControlSystem : ISystem {

    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate(SystemAPI.QueryBuilder().WithAll<PhysicsVelocity, ShipHandlingControl, ShipParameters>().Build());
    }

    public void OnUpdate(ref SystemState state) {
        
        foreach(var (velocity, shipHandling, shipParameters) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<ShipHandlingControl>, RefRO<ShipParameters>>()) {
            velocity.ValueRW.Linear.x = shipHandling.ValueRO.horizontalMovement * shipParameters.ValueRO.speed;
        }

    }

}