using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PInvoke {


    public class OsExtend {

        public static readonly Version OSVersion = Environment.OSVersion.Version;

        public static bool IsWindowsXP() {
            return OSVersion.Major == 5 && OSVersion.Minor == 1;
        }

        public static bool IsWindowsXPOrGreater() {
            return (OSVersion.Major == 5 && OSVersion.Minor >= 1) || OSVersion.Major > 5;
        }

        public static bool IsWindowsVista() {
            return OSVersion.Major == 6;
        }

        public static bool IsWindowsVistaOrGreater() {
            return OSVersion.Major >= 6;
        }

        public static bool IsWindows7() {
            return OSVersion.Major == 6 && OSVersion.Minor == 1;
        }

        public static bool IsWindows7OrGreater() {
            return (OSVersion.Major == 6 && OSVersion.Minor >= 1) || OSVersion.Major > 6;
        }

        public static bool IsWindows8() {
            return OSVersion.Major == 6 && OSVersion.Minor == 2;
        }

        public static bool IsWindows8OrGreater() {
            return (OSVersion.Major == 6 && OSVersion.Minor >= 2) || OSVersion.Major > 6;
        }

        public static bool IsWindows10OrGreater(int build = -1) {
            return OSVersion.Major >= 10 && OSVersion.Build >= build;
        }
    }
}
