using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HasselhoffMaker.Helpers
{
    /// <summary>
    /// Command Line Helpers
    /// </summary>
    internal static class ConsoleCommand
    {
        #region Console Helpers

        internal static void PrintMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(message);
        }

        internal static void PrintInformation(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        internal static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }

        internal static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
        }

        internal static void EmptyLine()
        {
            PrintMessage(string.Empty);
        }

        internal static void Wait()
        {
            Console.ReadKey();
        }

        internal static void Clean()
        {
            Console.Clear();
        }

        #endregion

        internal static void ExecuteCommandSync(object command)
        {
            // create the ProcessStartInfo using "cmd" as the program to be run,
            // and "/c " as the parameters.
            // Incidentally, /c tells cmd that we want it to execute the command that follows,
            // and then exit.
            var procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // The following commands are needed to redirect the standard output.
            // This means that it will be redirected to the Process.StandardOutput StreamReader.
            // Do not create the black window.
            // Now we create a process, assign its ProcessStartInfo and start it
            new Process { StartInfo = procStartInfo }.Start();
        }

        #region Console Arguments Management

        private static string ParseArgument(string argument)
        {
            argument = argument.Trim();

            if (argument.StartsWith("-"))
                argument = argument.Substring(1);

            return argument.ToUpperInvariant();
        }

        internal static IEnumerable<string> ParseArguments(string[] args)
        {
            if (args.Length == 1)
            {
                var argument = ParseArgument(args[0]);
                switch (argument)
                {
                    case Options.Basic:
                        yield return Options.DesktopBackground;
                        yield return Options.HideIcons;
                        yield return Options.HideTaskbar;
                        break;
                    case Options.Medium:
                        yield return Options.DesktopBackground;
                        yield return Options.HideIcons;
                        yield return Options.RotateScreen;
                        yield return Options.MouseButtons;
                        yield return Options.HideTaskbar;
                        break;
                    case Options.Complete:
                        yield return Options.DesktopBackground;
                        yield return Options.HideIcons;
                        yield return Options.RotateScreen;
                        yield return Options.MouseButtons;
                        yield return Options.DesktopDisable;
                        yield return Options.HideTaskbar;
                        yield return Options.Lock;
                        break;
                    case Options.Full:
                        yield return Options.DesktopCustomBackground;
                        yield return Options.HideIcons;
                        yield return Options.RotateScreen;
                        yield return Options.MouseButtons;
                        yield return Options.DesktopDisable;
                        yield return Options.HideTaskbar;
                        yield return Options.Lock;
                        break;
                    default:
                        yield return argument;
                        break;
                }
            }
            else
            {
                foreach (var arg in args)
                {
                    yield return ParseArgument(arg);
                }
            }
        }

        internal static IEnumerable<string> ValidateArguments(List<string> args)
        {
            //RESTORE must be single command
            if (args.Select(ParseArgument).Contains(Options.Restore) && args.Count > 1)
                yield return string.Format("Command {0} cannot be combined with other commands", Options.Restore);
        }

        internal static void PrintCommandsApiHelp()
        {
            EmptyLine();
            PrintMessage("Press any key to display help...");
            Wait();
            EmptyLine();
            PrintInformation("***************");
            PrintInformation("*** OPTIONS ***");
            PrintInformation("***************");
            PrintCommandHelp(Options.PresetBackground, "Sets a default predefined desktop background");
            PrintCommandHelp(Options.DesktopBackground, "Sets a desktop capture as desktop background");
            PrintCommandHelp(Options.DesktopCustomBackground, "Sets a custom file named 'custom' located inside application folder with an image extension (jpg, jpeg, png, bmp, gif, tiff) as desktop background");
            PrintCommandHelp(Options.BootScreen, "[NOT IMPLEMENTED YET] Changes bootscreen");
            PrintCommandHelp(Options.HideIcons, "Hide desktop icons");
            PrintCommandHelp(Options.RotateScreen, "[NOT IMPLEMENTED YET] Rotate screen 180 degree");
            PrintCommandHelp(Options.MouseButtons, "Invert mouse buttons");
            PrintCommandHelp(Options.DesktopDisable, "Disables desktop. User cannot click anything");
            PrintCommandHelp(Options.DesktopScreenshot, "Takes a desktop screenshot and saves to file 'test.bmp' inside application folder");
            PrintCommandHelp(Options.Lock, "Locks session (Windows + L)");
            PrintCommandHelp(Options.HideTaskbar, "Hide desktop hidebar");
        }

        private static void PrintCommandHelp(string command, string description)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(command);
            PrintMessage(description);
            EmptyLine();
        }

        #endregion
    }
}