// -----------------------------------------------------------------------
// <copyright file="LightSourceObject.cs" company="MapEditorReborn">
// Copyright (c) MapEditorReborn. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using PluginAPI.Core;

namespace MapEditorReborn.API.Features.Objects
{
    using AdminToys;
    using Exiled.API.Enums;
    using Mirror;
    using Serializable;
    using UnityEngine;
    using Light = Exiled.API.Features.Toys.Light;

    /// <summary>
    /// The component added to <see cref="LightSourceSerializable"/>.
    /// </summary>
    public class LightSourceObject : MapEditorObject
    {
        private LightSourceToy _lightSourceToy;

        private void Awake()
        {
            _lightSourceToy = GetComponent<LightSourceToy>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LightSourceObject"/> class.
        /// </summary>
        /// <param name="lightSourceSerializable">The required <see cref="LightSourceSerializable"/>.</param>
        /// <param name="spawn">A value indicating whether the component should be spawned.</param>
        /// <returns>The initialized <see cref="LightSourceObject"/> instance.</returns>
        public LightSourceObject Init(LightSourceSerializable lightSourceSerializable, bool spawn = true)
        {
            Base = lightSourceSerializable;

            ForcedRoomType = lightSourceSerializable.RoomType != RoomType.Unknown ? lightSourceSerializable.RoomType : FindRoom().Type;
            
            UpdateObject();

            if (spawn)
                NetworkServer.Spawn(gameObject);
            
            return this;
        }

        public override MapEditorObject Init(SchematicBlockData block)
        {
            base.Init(block);

            Base = new LightSourceSerializable(block);

            UpdateObject();

            return this;
        }

        /// <summary>
        /// The base <see cref="LightSourceSerializable"/>.
        /// </summary>
        public LightSourceSerializable Base;

        /*
        public bool IsStatic
        {
            get => _isStatic;
            set
            {
                _lightSourceToy.enabled = !value;
                Light.MovementSmoothing = (byte)(value ? 0 : 60);
                _isStatic = value;
            }
        }
        */

        /// <inheritdoc cref="MapEditorObject.IsRotatable"/>
        public override bool IsRotatable => true;

        /// <inheritdoc cref="MapEditorObject.IsScalable"/>
        public override bool IsScalable => false;

        /// <inheritdoc cref="MapEditorObject.UpdateObject()"/>
        public override void UpdateObject()
        {
            //Log.Info("Updating object");
            
            _lightSourceToy.Position = Base.Position;
            _lightSourceToy.Rotation = Quaternion.Euler(Base.Rotation);
            
            _lightSourceToy.NetworkLightType = Base.Type;
            _lightSourceToy.NetworkLightColor = GetColorFromString(Base.Color);
            _lightSourceToy.NetworkLightIntensity = Base.Intensity;
            _lightSourceToy.NetworkLightRange = Base.Range;
            _lightSourceToy.NetworkLightShape = Base.Shape;
            _lightSourceToy.NetworkSpotAngle = Base.SpotAngle;
            _lightSourceToy.NetworkInnerSpotAngle = Base.InnerSpotAngle;
            _lightSourceToy.NetworkShadowType = Base.ShadowType;
            _lightSourceToy.NetworkIsStatic = Base.Static;

            UpdateTransformProperties();
        }

        private void UpdateTransformProperties()
        {
            _lightSourceToy.NetworkPosition = transform.position;
            _lightSourceToy.NetworkRotation = transform.rotation;
        }

        private bool _isStatic;
    }
}