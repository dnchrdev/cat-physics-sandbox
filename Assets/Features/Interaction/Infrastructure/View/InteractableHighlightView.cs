using System.Collections.Generic;
using System.Linq;
using Features.Interaction.Infrastructure.Configs;
using Features.Interaction.InterfaceAdapters.Ports;
using UnityEngine;
using UnityEngine.Rendering;

namespace Features.Interaction.Infrastructure.View
{
    public sealed class InteractableHighlightView : IInteractableHighlightView
    {
        private static readonly HashSet<Mesh> SmoothNormalsApplied = new();
        private static readonly int OutlineColorId = Shader.PropertyToID("_OutlineColor");
        private static readonly int OutlineWidthId = Shader.PropertyToID("_OutlineWidth");
        private static readonly int ZTestId = Shader.PropertyToID("_ZTest");

        private readonly MeshRenderer _meshRenderer;
        private readonly MeshFilter _meshFilter;
        private readonly InteractableVisualConfig _config;
        private readonly Material[] _originalMaterials;
        private readonly List<Material> _materialsBuffer = new();
        private Material _outlineMaskMaterial;
        private Material _outlineFillMaterial;

        public InteractableHighlightView(MeshRenderer meshRenderer, InteractableVisualConfig config)
        {
            _meshRenderer = meshRenderer;
            _config = config;
            if (_meshRenderer == null) return;

            _originalMaterials = _meshRenderer.sharedMaterials;
            _meshFilter = _meshRenderer.GetComponent<MeshFilter>();
        }

        public void ShowFocus()
        {
            if (_config == null || !_config.UseOutlineOnFocus) return;
            ApplyOutline(_config.OutlineColorOnFocus, _config.OutlineWidthOnFocus, _config.OutlineModeOnFocus);
        }

        public void ShowGrab()
        {
            if (_config == null) return;

            if (_config.OnGrabMaterial != null)
                ApplyMaterial(_config.OnGrabMaterial);

            if (_config.UseOutlineOnGrab)
                ApplyOutline(_config.OutlineColorOnGrab, _config.OutlineWidthOnGrab, _config.OutlineModeOnGrab);
        }

        public void Clear()
        {
            if (_meshRenderer == null || _originalMaterials == null) return;
            _meshRenderer.sharedMaterials = _originalMaterials;
        }

        private void ApplyMaterial(Material material)
        {
            if (_meshRenderer == null) return;

            _meshRenderer.GetSharedMaterials(_materialsBuffer);
            for (var i = 0; i < _materialsBuffer.Count; i++)
                _materialsBuffer[i] = material;
            _meshRenderer.SetSharedMaterials(_materialsBuffer);
        }

        private void ApplyOutline(Color color, float width, OutlineMode mode)
        {
            if (_meshRenderer == null) return;

            if (_outlineMaskMaterial == null)
                _outlineMaskMaterial = Object.Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
            if (_outlineFillMaterial == null)
                _outlineFillMaterial = Object.Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

            ApplySmoothNormals();

            var (maskZTest, fillZTest, outlineWidth) = mode switch
            {
                OutlineMode.OutlineAll => (CompareFunction.Always, CompareFunction.Always, width),
                OutlineMode.OutlineVisible => (CompareFunction.Always, CompareFunction.LessEqual, width),
                OutlineMode.OutlineHidden => (CompareFunction.Always, CompareFunction.Greater, width),
                OutlineMode.OutlineAndSilhouette => (CompareFunction.LessEqual, CompareFunction.Always, width),
                OutlineMode.SilhouetteOnly => (CompareFunction.LessEqual, CompareFunction.Greater, 0f),
                _ => (CompareFunction.Always, CompareFunction.LessEqual, width)
            };

            _outlineFillMaterial.SetColor(OutlineColorId, color);
            _outlineMaskMaterial.SetFloat(ZTestId, (float)maskZTest);
            _outlineFillMaterial.SetFloat(ZTestId, (float)fillZTest);
            _outlineFillMaterial.SetFloat(OutlineWidthId, outlineWidth);

            _meshRenderer.GetSharedMaterials(_materialsBuffer);
            _materialsBuffer.Add(_outlineMaskMaterial);
            _materialsBuffer.Add(_outlineFillMaterial);
            _meshRenderer.SetSharedMaterials(_materialsBuffer);
        }

        private void ApplySmoothNormals()
        {
            if (_meshFilter == null || _meshFilter.sharedMesh == null) return;

            var mesh = _meshFilter.sharedMesh;
            if (!SmoothNormalsApplied.Add(mesh)) return;

            var groups = mesh.vertices
                .Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index))
                .GroupBy(pair => pair.Key);

            var smoothNormals = new List<Vector3>(mesh.normals);
            foreach (var group in groups)
            {
                if (group.Count() == 1) continue;

                var smoothNormal = Vector3.zero;
                foreach (var pair in group)
                    smoothNormal += smoothNormals[pair.Value];
                smoothNormal.Normalize();

                foreach (var pair in group)
                    smoothNormals[pair.Value] = smoothNormal;
            }

            mesh.SetUVs(3, smoothNormals);
        }
    }
}
