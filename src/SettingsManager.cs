using AudioStream.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioStream {
    
    internal static class SettingsManager {

        private static RegistryKey? startup = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        /// <summary>
        /// A boolean indicating whether or not the tray icon should be visible.
        /// </summary>
        public static bool EnableTrayIcon { 

            get {
                return Settings.Default.EnableTrayIcon;
            }

            set {
                Settings.Default.EnableTrayIcon = value;
                Settings.Default.Save();
            }

        }
        
        /// <summary>
        /// A boolean indicating whether or not the application should start minimized.
        /// </summary>
        public static bool StartMinimized {

            get {
                return Settings.Default.StartMinimized;
            }

            set {
                Settings.Default.StartMinimized = value;
                Settings.Default.Save();
            }

        }

        /// <summary>
        /// A boolean indicating whether or not the application should start automatically when the user logs in.
        /// Note that changing this value will directly modify the registry.
        /// </summary>
        public static bool EnableStartup {
        
            get {
                return startup != null && startup.GetValue("AudioStream") != null;
            }

            set {
                if (startup != null) {
                    if (!value && EnableStartup) {
                        startup.DeleteValue("AudioStream");
                    }
                    else if (value && !EnableStartup) {
                        startup.SetValue("AudioStream", "\"" + Application.ExecutablePath.ToString() + "\"");
                    }
                }
            }

        }

    }

}
