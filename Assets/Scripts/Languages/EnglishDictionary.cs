using System.Collections.Generic;

public partial class LanguageDictionaries
{
    public static Dictionary<string, string> englishDictionary = new()
    {
        // Option UI
        { "GraphicButton","Graphic" },
        { "ControllerButton","Controller" },
        { "GameplayButton","Gameplay" },
        { "SoundButton","sound" },

        { "Reset","Reset" },

        { "All Reset Graphic Button", "Reset Graphics Settings" },
        { "All Reset Controller Button", "Reset Controller Settings" },
        { "All Reset Gameplay Button",   "Reset Gameplay Settings" },
        { "All Reset Sound Button",      "Reset Sound Settings" },
        { "Initialize Graphic Confirm", "Do you want to reset the graphics settings?" },
        { "Initialize Controller Confirm", "Do you want to reset the controller settings?" },
        { "Initialize Gameplay Confirm",   "Do you want to reset the gameplay settings?" },
        { "Initialize Sound Confirm",      "Do you want to reset the sound settings?" },

        { "On", "On" },
        { "Off", "Off" },

        { "Yes", "Yes" },
        { "No", "No" },

        // Graphic Display
        { "Resolution","Resolution" },
        { "ScreenMode","Screen Mode" },

        // Graphic_ScreenMode Dropdown Text
        { "ExclusiveFullScreen", "Exclusive Fullscreen"},
        { "FullScreenWindow", "Borderless Window"},
        { "Windowed", "Windowed"},
        
        // Graphic_FPS Dropdown Text 
        { "Unlimited","Unlimited" },

        // Controller Display
        { "displayUp", "Up " },
        { "displayDown", "Down" },
        { "displayLeft", "Left" },
        { "displayRight", "Right" },
        { "displayInventory", "Inventory" },

        // Gameplay Display
        { "LanguageSetting", "Language" },
        { "AutoSave", "Auto Save" },
        { "AutosaveInterval", "Auto Save Interval (Minutes)" },
        { "ShowMiniMap", "Mini Map" },
        { "ShowTime", "Show Time" },
        { "AutoLooting", "Auto Loot" },
        { "CameraShake", "Camera Shake" },
    };
}
