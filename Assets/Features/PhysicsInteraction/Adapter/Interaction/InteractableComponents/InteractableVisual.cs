using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public class InteractableVisual
    {
        private readonly MeshRenderer _meshRenderer;
        private readonly InteractableVisualConfig _interactableVisualConfig;

        private Material[] _materials;
        private static readonly HashSet<Mesh> _smoothNormalsApplied = new HashSet<Mesh>();
        private Material _outlineMaskMaterial;
        private Material _outlineFillMaterial;


        public InteractableVisual(MeshRenderer meshRenderer, InteractableVisualConfig interactableVisualConfig)
        {
            _meshRenderer = meshRenderer;
            _interactableVisualConfig = interactableVisualConfig;

            if (_meshRenderer == null || _interactableVisualConfig == null) return;

            _materials = meshRenderer.materials;
        }

        public void ShowVisualOnFocus()
        {
            if (_meshRenderer == null || _interactableVisualConfig == null || _interactableVisualConfig.UseOutlineOnFocus == false) return;

            ApplyOutlineColor(_interactableVisualConfig.OutlineColorOnFocus, _interactableVisualConfig.OutlineWidthOnFocus, _interactableVisualConfig.OutlineOutlineModeOnFocus);
        }

        private void ApplyOutlineColor(Color color, float width, OutlineMode outlineMode)
        {
            if (_outlineMaskMaterial == null)
                _outlineMaskMaterial = Object.Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));

            if (_outlineFillMaterial == null)
                _outlineFillMaterial = Object.Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

            ApplySmoothNormals();

            _outlineFillMaterial.SetColor("_OutlineColor", color);

            switch (outlineMode)
            {
                case OutlineMode.OutlineAll:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", width);
                    break;

                case OutlineMode.OutlineVisible:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", width);
                    break;

                case OutlineMode.OutlineHidden:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", width);
                    break;

                case OutlineMode.OutlineAndSilhouette:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", width);
                    break;

                case OutlineMode.SilhouetteOnly:
                    _outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
                    _outlineFillMaterial.SetFloat("_OutlineWidth", 0f);
                    break;
            }

            var mats = _meshRenderer.materials.ToList();
            mats.Add(_outlineMaskMaterial);
            mats.Add(_outlineFillMaterial);
            _meshRenderer.materials = mats.ToArray();
        }

        private void ApplySmoothNormals()
        {
            var meshFilter = _meshRenderer.GetComponent<MeshFilter>();
            if (meshFilter == null || meshFilter.sharedMesh == null) return;

            var mesh = meshFilter.sharedMesh;

            if (!_smoothNormalsApplied.Add(mesh)) return;

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

        public void ShowVisualOnGrab()
        {
            if (_meshRenderer == null || _interactableVisualConfig == null) return;

            if (_interactableVisualConfig.OnGrabMaterial != null) 
            { 
                var mats = _meshRenderer.materials;
                for (var i = 0; i < mats.Length; i++)
                {
                    mats[i] = _interactableVisualConfig.OnGrabMaterial;
                }
                _meshRenderer.materials = mats;
            }

            if (_interactableVisualConfig.UseOutlineOnGrab)
            {
                ApplyOutlineColor(_interactableVisualConfig.OutlineColorOnGrab, _interactableVisualConfig.OutlineWidthOnGrab, _interactableVisualConfig.OutlineOutlineModeOnGrab);
            }

        }

        public void Reset()
        {
            if (_meshRenderer == null || _materials == null) return;

            _meshRenderer.materials = _materials;

        }
    }
}