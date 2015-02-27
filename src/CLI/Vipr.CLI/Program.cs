using System;
using System.Reflection;
using Mono.Options;
using TemplateWriter;
using Vipr.CLI.Configuration;

namespace Vipr.CLI
{
    internal class Program
    {
        private static readonly string AppName = Assembly.GetExecutingAssembly().GetName().Name;

        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder().WithJsonConfig().WithArguments(args);
                var entrypoint = new CLIEntryPoint(new TemplateProcessorManager(), builder);
                entrypoint.Process();
            }
            catch (OptionException optionException)
            {
                Logger.Log(AppName);
                Logger.Log(optionException.Message);
                Logger.Log(string.Format("Try '{0} --help' for more information.", AppName));
            }
            catch (Exception e)
            {
                Logger.Log("*-------------------An Exception has been raised -------------------*");
                Logger.Log("Message: " + e.Message);
                Logger.Log("  Stack:" + e.StackTrace);
                Logger.Log("*-------------------------------------------------------------------*" + Environment.NewLine);
            }

            Logger.Log("Generation finished");
            Logger.Log("Press a key to exit");
            Console.ReadKey();
        }
    }
}