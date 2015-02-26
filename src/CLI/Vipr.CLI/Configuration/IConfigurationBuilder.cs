using Mono.Options;
using TemplateWriter;

namespace Vipr.CLI.Configuration
{
    public interface IConfigurationBuilder
    {
		IConfigArguments Build();
		IConfigurationBuilder WithArguments(params string[] args);
		IConfigurationBuilder WithJsonConfig();
		void CreateOptionSet(params string[] args);
		OptionSet OptionSet { get; }
    }
}