using Unity.Entities;
using UnityEngine;

public struct PlayerInput : IComponentData {
    public float horizontalAxis;
    public bool wasSpaceButtonClicked;
}

public struct PlayerController : IComponentData {
    public Entity controlledEntity;
}

public class PlayerAuthoring : MonoBehaviour {

    [SerializeField]
    private ShipAuthoring playerShipGO;

    private class PlayerInputBaker : Baker<PlayerAuthoring> {

        public override void Bake(PlayerAuthoring authoring) {
            AddComponent<PlayerInput>();
            AddComponent(new PlayerController() {
                controlledEntity = GetEntity(authoring.playerShipGO)
            });
        }
    }
}