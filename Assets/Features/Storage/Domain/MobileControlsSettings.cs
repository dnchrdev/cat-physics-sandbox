using Feature.MobileButtonsAdjustment;
using System;
using UnityEngine;

namespace Feature.Storage
{
    public struct AnchoredPosition
    {
        public float X;
        public float Y;

        public AnchoredPosition(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public class MobileControlsSettings : IReadOnlyMobileControls
    {
        private AdjustableButtonType[] adjustableTypes;

        private bool[] _isDefault;

        private bool[] _isInitialized;

        private AnchoredPosition[] _adjustablePosition;

        private AnchoredPosition[] _defaultPosition;

        public int JoystickRadius { get; private set; }
        public bool IsDynamicJoystick { get; private set; }
        public bool IsFollowJoystick { get; private set; }

        public int MaxJoystickRadius => _maxJoystickRadius;

        public int MinJoystickRadius => _minJoystickRadius;

        private readonly int _maxJoystickRadius;

        private readonly int _minJoystickRadius;

        public AnchoredPosition[] AdjustablePositions => _adjustablePosition;

        public bool[] Defaults => _isDefault;

        public MobileControlsSettings()
        {
            adjustableTypes = new AdjustableButtonType[6]
            {
                AdjustableButtonType.Hit,
                AdjustableButtonType.Grab,
                AdjustableButtonType.Throw,
                AdjustableButtonType.Release,
                AdjustableButtonType.MoveJoystick,
                AdjustableButtonType.Jump
            };

            _isDefault = new bool[6]
            {
                true,
                true,
                true,
                true,
                true,
                true,
            };

            _isInitialized = new bool[6]
            {
                false,
                false,
                false,
                false,
                false,
                false,
            };

            _adjustablePosition = new AnchoredPosition[6]
            {
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
            };

            _defaultPosition = new AnchoredPosition[6]
            {
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
            };

            _maxJoystickRadius = 250;
            _minJoystickRadius = 50;

             SetJoystickRadius(150);
            IsDynamicJoystick = true;
            IsFollowJoystick = true;
        }

        public Vector2 GetAnchoredPosition(AdjustableButtonType adjustableType)
        {
            for (int i = 0; i < adjustableTypes.Length; i++)
            {
                if (adjustableTypes[i] == adjustableType)
                {
                    if (_isDefault[i])
                    {
                        if (GetIsInitialized(adjustableTypes[i]) == false) throw new System.Exception($"{adjustableType} is not initialized");

                        return GetVector2(_defaultPosition[i]);
                    }
                    else
                    {
                        return GetVector2(_adjustablePosition[i]);
                    }
                }
            }

            throw new System.Exception($"Invalid button Position");
        }

        public void SetAnchoredPosition(AdjustableButtonType adjustableType, Vector2 position)
        {
            for (int i = 0; i < adjustableTypes.Length; i++)
            {
                if (adjustableTypes[i] == adjustableType)
                {
                    _isDefault[i] = false;
                    _adjustablePosition[i] = GetParsedPosition(position);
                    return;
                }
            }
        }

        public void SetAnchoredPositions(float[] positionX, float[] positionY, bool[] defaults)
        {
            if (positionX == null || positionX.Length != _adjustablePosition.Length) return;
            if (positionY == null || positionY.Length != _adjustablePosition.Length) return;
            if (defaults == null || defaults.Length != _adjustablePosition.Length) return;

            for (int i = 0; i < adjustableTypes.Length; i++)
            {
                if (defaults[i] == true) continue;

                _adjustablePosition[i].X = positionX[i];
                _adjustablePosition[i].Y = positionY[i];
                _isDefault[i] = false;
            }
        }

        public void SetDefaultAnchoredPosition(AdjustableButtonType adjustableType, Vector2 position)
        {
            for (int i = 0; i < adjustableTypes.Length; i++)
            {
                if (adjustableTypes[i] == adjustableType)
                {
                    _isInitialized[i] = true;
                    _defaultPosition[i] = GetParsedPosition(position);
                    return;
                }
            }
        }

        public bool GetIsInitialized(AdjustableButtonType adjustableType)
        {
            for (int i = 0; i < adjustableTypes.Length; i++)
            {
                if (adjustableTypes[i] == adjustableType)
                {
                    return _isInitialized[i];
                }
            }

            return false;
        }

        public void ResetToDefaults()
        {
            for (int i = 0; i < adjustableTypes.Length; i++)
            {
                if (_isInitialized[i])
                {
                    _isDefault[i] = false;
                    _adjustablePosition[i] = _defaultPosition[i];
                }
            }
        }

        public void SetJoystickRadius(int radius)
        {
            JoystickRadius = Mathf.Clamp(radius, _minJoystickRadius, _maxJoystickRadius);
        }

        public void SetDynamicJoystick(bool dymanic)
        {
            IsDynamicJoystick = dymanic;
        }

        public void SetFollowJoystick(bool follow)
        {
            IsFollowJoystick = follow;
        }

        private Vector2 GetVector2(AnchoredPosition anchoredPosition)
        {
            return new Vector2(anchoredPosition.X, anchoredPosition.Y);
        }

        private AnchoredPosition GetParsedPosition(Vector2 vector)
        {
            return new AnchoredPosition(vector.x, vector.y);
        }

        public void ResetAllPositions()
        {
            adjustableTypes = new AdjustableButtonType[6]
           {
                AdjustableButtonType.Hit,
                AdjustableButtonType.Grab,
                AdjustableButtonType.Throw,
                AdjustableButtonType.Release,
                AdjustableButtonType.MoveJoystick,
                AdjustableButtonType.Jump
           };

            _isDefault = new bool[6]
            {
                true,
                true,
                true,
                true,
                true,
                true,
            };

            _isInitialized = new bool[6]
            {
                false,
                false,
                false,
                false,
                false,
                false,
            };

            _adjustablePosition = new AnchoredPosition[6]
            {
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
            };

            _defaultPosition = new AnchoredPosition[6]
            {
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
                new AnchoredPosition(0f, 0f),
            };
        }
    }
}