// -----------------------------------------------------------------------
// <copyright file="LightSourceSerializable.cs" company="MapEditorReborn">
// Copyright (c) MapEditorReborn. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace MapEditorReborn.API.Features.Serializable
{
    using System;
    using UnityEngine;
    using YamlDotNet.Serialization;

    /// <summary>
    /// A tool used to easily handle light sources.
    /// </summary>
    [Serializable]
    public class LightSourceSerializable : SerializableObject
    {
        public LightSourceSerializable()
        {
        }

        public LightSourceSerializable(string color, float intensity, float range, bool shadows)
        {
            Color = color;
            Intensity = intensity;
            Range = range;
            ShadowType = shadows ? LightShadows.Soft : LightShadows.None;
        }

        public LightSourceSerializable(SchematicBlockData block)
        {
            Color = block.Properties["Color"]?.ToString() ?? "FFFFFF";
            Intensity = float.TryParse(block.Properties["Intensity"]?.ToString(), out float intensity) ? intensity : 1f;
            Range = float.TryParse(block.Properties["Range"]?.ToString(), out float range) ? range : 1f;
            Type = Enum.TryParse(block.Properties["LightType"]?.ToString(), out LightType type) ? type : LightType.Point;
            Shape = Enum.TryParse(block.Properties["Shape"]?.ToString(), out LightShape shape) ? shape : LightShape.Cone;
            SpotAngle = float.TryParse(block.Properties["SpotAngle"]?.ToString(), out float spotAngle) ? spotAngle : 45f;
            InnerSpotAngle = float.TryParse(block.Properties["InnerSpotAngle"]?.ToString(), out float innerSpotAngle) ? innerSpotAngle : 30f;
            ShadowStrength = float.TryParse(block.Properties["ShadowStrength"]?.ToString(), out float shadowStrength) ? shadowStrength : 1f;
            ShadowType = Enum.TryParse(block.Properties["ShadowType"]?.ToString(), out LightShadows shadowType) ? shadowType : LightShadows.Soft;
            Static = bool.TryParse(block.Properties["Static"]?.ToString(), out bool isStatic) && isStatic;
        }
        
        public LightType Type { get; set; } = LightType.Point;

        /// <summary>
        /// Gets or sets the <see cref="LightSourceSerializable"/>'s color.
        /// </summary>
        public string Color { get; set; } = "white";

        /// <summary>
        /// Gets or sets the <see cref="LightSourceSerializable"/>'s intensity.
        /// </summary>
        public float Intensity { get; set; } = 1f;

        /// <summary>
        /// Gets or sets the <see cref="LightSourceSerializable"/>'s range.
        /// </summary>
        public float Range { get; set; } = 1f;
        
        /// <summary>
        /// Gets or sets the <see cref="LightSourceSerializable"/>'s shape.
        /// </summary>
        public LightShape Shape { get; set; } = LightShape.Cone;
        
        /// <summary>
        /// Gets or sets the <see cref="LightSourceSerializable"/>'s spot angle.
        /// </summary>
        public float SpotAngle { get; set; } = 45f;
        
        /// <summary>
        /// Gets or sets the <see cref="LightSourceSerializable"/>'s inner spot angle.
        /// </summary>
        public float InnerSpotAngle { get; set; } = 30f;
        
        /// <summary>
        /// Gets or sets the <see cref="LightSourceSerializable"/>'s shadow strength.
        /// </summary>
        public float ShadowStrength { get; set; } = 1f;
        
        /// <summary>
        /// Gets or sets the <see cref="LightSourceSerializable"/>'s shadow type.
        /// </summary>
        public LightShadows ShadowType { get; set; } = LightShadows.Soft;
        
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="LightSourceSerializable"/> is static.
        /// </summary>
        public bool Static { get; set; } = false;

        [YamlIgnore] public override Vector3 Scale { get; set; }
    }
}