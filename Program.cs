using System;
using System.Collections.Generic;
using System.IO;

namespace Dalamud.Injector {
    internal static class Program {

        private static void Main(string[] args)
        {
            var exePath = args.Length == 0
                ? @"C:\Program Files (x86)\SquareEnix\FINAL FANTASY XIV - A Realm Reborn\game\ffxiv_dx11.exe"
                : args[1];
            
            AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs eventArgs)
            {
                File.WriteAllText("InjectorException.txt", eventArgs.ExceptionObject.ToString());
                Environment.Exit(0);
            };

            string gameArguments =
                "DEV.TestSID=0 " +
                "DEV.UseSqPack=1 " +
                "DEV.DataPathType=1 " +
                "SYS.Region=0 " +
                "language=1 " +
                "version=1.0.0.0 " +
                "DEV.MaxEntitledExpansionID=3 " +
                "DEV.GMServerHost=127.0.0.1 " +
                "DEV.GameQuitMessageBox=1";

            NativeAclFix.LaunchGame(Path.GetDirectoryName(exePath), exePath, gameArguments, new Dictionary<string, string>());
        }
    }
}
