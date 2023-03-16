using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[Serializable]
public struct ShipParameters : IComponentData {
    
    // [NonSerialized]
    // public Entity weaponEntity;
    
    public float speed;
}

public struct ShipHandlingControl : IComponentData {
    public float horizontalMovement;
    public bool hasWeaponFired;
}

public class ShipAuthoring : MonoBehaviour {

    // [SerializeField]
    // private WeaponAuthoring weapon;
    
    [SerializeField]
    private ShipParameters shipParameters;

    private class ShipBaker : Baker<ShipAuthoring> {

        public override void Bake(ShipAuthoring authoring) {
            AddComponent<ShipHandlingControl>();
            AddComponent(new ShipParameters() {
                speed = authoring.shipParameters.speed,
          //      weaponEntity = GetEntity(authoring.weapon.gameObject)
            });
        }

    }


}