using System;
using System.IO;
using Mono.Options;
using TemplateWriter;
using Vipr.CLI.Configuration;

namespace Vipr.CLI
{
    internal class Program
    {

        static void Main(string[] args)
        {
			try
            {
                var builder = new ConfigurationBuilder().WithArguments(args);
                var entrypoint = new CLIEntryPoint(builder, new TemplateProcessorManager());
                entrypoint.Process();

            }
            catch (OptionException optionException)
            {
                Logger.Log("ODataCodeGen: ");
                Logger.Log(optionException.Message);
                Logger.Log("Try 'ODataCodeGen --help' for more information.");
            }
            catch (Exception e)
            {
                Logger.Log("*-------------------An Exception has been raised -------------------*");
                Logger.Log("Message: " + e.Message);
                Logger.Log("  Stack:" + e.StackTrace);
                Logger.Log("*-------------------------------------------------------------------*" + Environment.NewLine);
            }

            Console.WriteLine("The log was saved in the file log.txt in the path " + Directory.GetCurrentDirectory());
            Logger.Log("Press a key to exit");
            Console.ReadKey();
        }
    }
}