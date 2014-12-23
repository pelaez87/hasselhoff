using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HasselhoffMaker.Helpers;
using HasselhoffMaker.Systems.Core;

namespace HasselhoffMaker
{
    class Program
    {
        //Helper vars
        private static bool hasLock = false;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            ShowAppInfo();

            var parsedArguments = ConsoleCommand.ParseArguments(args).ToList();
            if (parsedArguments.Count == 0)
            {
                ConsoleCommand.PrintWarning("No commands provided please see Commands API Help below and try to execute it again");
                ConsoleCommand.EmptyLine();
                ConsoleCommand.PrintWarning("You must create a .bat file to execute this program and provide some commands");
                ConsoleCommand.PrintWarning("SAMPLE: \"Hasselhoff Maker.exe\" -LOCK");
                ConsoleCommand.PrintCommandsApiHelp();
                ConsoleCommand.Wait();
            }
            else
            {
                var deactivate = false;
#if GODMODE
                deactivate = (parsedArguments.Count == 1 && parsedArguments.First().Equals(Options.DeactivateGodMode));
#endif
                if (GodMode.IsInstalled && !deactivate)
                {
                    ConsoleCommand.PrintWarning("Could not perform any action because it is a protected PC");
                    ConsoleCommand.Wait();
                }
                else
                {
                    var errors = ConsoleCommand.ValidateArguments(parsedArguments).ToList();

                    if (errors.Any())
                    {
                        errors.ForEach(ConsoleCommand.PrintError);
                        ConsoleCommand.Wait();
                    }
                    else
                    {
                        //To debug an explicit argument go to Project Properties > Debug > Initial Options and insert command to debug
                        ExecuteArguments(parsedArguments);

                        if (hasLock) 
                            return;

                        ConsoleCommand.EmptyLine();
                        ConsoleCommand.PrintMessage("Hasselhoff operation completed successfully!");
                        ConsoleCommand.Wait();
                    }
                }
            }
        }

        private static void ShowAppInfo()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            var copyrightInformation = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute));

            ConsoleCommand.PrintInformation("*******************************************************");
            ConsoleCommand.PrintInformation(string.Format("{0} v{1}", assemblyName.Name, assemblyName.Version));
            ConsoleCommand.PrintInformation(copyrightInformation.Copyright);
            ConsoleCommand.PrintInformation("*******************************************************");
            ConsoleCommand.EmptyLine();
            ConsoleCommand.EmptyLine();
        }

        static void ExecuteArguments(List<string> args)
        {
            var factory = new HasselhoffFactory(SystemHelper.System);

            if (!args.Contains(Options.Restore)) //Avoid backup if performing a restore
                factory.BackupCurrentSettings(); //For security reasons, is better to make backup of volatile settings

            foreach (var argument in args)
            {
                var commandApplied = true;
                switch (argument)
                {
                    case Options.PresetBackground:
                        factory.ChangeBackground();
                        break;
                    case Options.DesktopBackground:
                        factory.ChangeBackgroundWithDesktopCapture();
                        break;
                    case Options.DesktopCustomBackground:
                        factory.ChangeBackgroundWithCustomWallpaper();
                        break;
                    case Options.BootScreen:
                        factory.ChangeBootscreen();
                        break;
                    case Options.HideIcons:
                        factory.HideDesktopIcons();
                        break;
                    case Options.DesktopDisable:
                        factory.DisableDesktop();
                        break;
                    case Options.RotateScreen:
                        factory.RotateScreen();
                        break;
                    case Options.MouseButtons:
                        factory.ChangeMouseButtonsConfiguration();
                        break;
                    case Options.DesktopScreenshot:
                        factory.MakeDesktopScreenshot();
                        break;
                    case Options.Lock:
                        hasLock = true;
                        factory.LockSession();
                        break;
                    case Options.HideTaskbar:
                        factory.HideTaskbar();
                        break;
                    //Utilities
                    case Options.Restore:
                        factory.RestoreSettings();
                        break;
#if GODMODE
                    case Options.ActivateGodMode:
                        GodMode.Install();
                        break;
                    case Options.DeactivateGodMode:
                        GodMode.Uninstall();
                        break;

#endif
                    default:
                        commandApplied = false;
                        break;
                }

                if (commandApplied)
                    ConsoleCommand.PrintMessage(string.Format("Command {0} applied succesfully", argument));
                else
                    ConsoleCommand.PrintWarning(string.Format("Command {0} skipped because is not recognized", argument));
            }
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            ConsoleCommand.PrintError(e.ExceptionObject.ToString());

            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}