using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    private static InputSystem_Actions _inputs;
    private static InputActionAsset _asset;
    private static bool _initialized;

    /// <summary>
    /// Ensures the input system is initialized.
    /// </summary>
    private static void EnsureInitialized()
    {
        if (_initialized) return;

        _inputs = new InputSystem_Actions();
        _asset = _inputs.asset;
        _initialized = true;
    }

    /// <summary>
    /// Global access to the generated input wrapper.
    /// </summary>
    public static InputSystem_Actions Inputs
    {
        get
        {
            EnsureInitialized();
            return _inputs;
        }
    }

    /// <summary>
    /// Sets the global unity cursor lock state and visibility.
    /// </summary>
    /// <param name="lockState">The desired cursor lock state.</param>
    /// <param name="visible">Whether the cursor should be visible.</param>
    public static void SetCursor(CursorLockMode lockState, bool visible)
    {
        Cursor.lockState = lockState;
        Cursor.visible = visible;
    }

    /// <summary>
    /// Sets the active input maps, disabling all others.
    /// </summary>
    /// <param name="activeMaps">The input maps to enable.</param>
    public static void SetActiveInputMaps(InputActionMap[] activeMaps)
    {
        EnsureInitialized();

        // Disable everything first
        foreach (var map in _asset.actionMaps)
        {
            map.Disable();
        }

        // Enable only the maps explicitly requested
        if (activeMaps != null)
        {
            foreach (var map in activeMaps)
            {
                if (map == null) continue;

                if (map.asset != _asset)
                {
                    Debug.LogWarning(
                        $"InputController.SetInputMaps: Map '{map.name}' does not belong to global asset '{_asset.name}'.");
                }

                map.Enable();
            }
        }
    }

    /// <summary>
    /// Sets the active input maps, disabling all others.
    /// </summary>
    /// <param name="activeMaps">The input maps to enable.</param>
    /// <param name="lockState">The desired cursor lock state.</param>
    /// <param name="visible">Whether the cursor should be visible.</param>
    public static void SetActiveInputMaps(InputActionMap[] activeMaps, CursorLockMode lockState, bool visible)
    {
        EnsureInitialized();

        // Disable everything first
        foreach (var map in _asset.actionMaps)
        {
            map.Disable();
        }

        // Enable only the maps explicitly requested
        if (activeMaps != null)
        {
            foreach (var map in activeMaps)
            {
                if (map == null) continue;

                if (map.asset != _asset)
                {
                    Debug.LogWarning(
                        $"InputController.SetInputMaps: Map '{map.name}' does not belong to global asset '{_asset.name}'.");
                }

                map.Enable();
            }
        }

        // Set cursor state
        SetCursor(lockState, visible);
    }
}