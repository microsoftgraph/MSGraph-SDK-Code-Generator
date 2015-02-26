namespace TemplateWriter
{
    public class ConfigArguments : IConfigArguments
    {
        public ITemplateConfiguration TemplateConfiguration { get; set; }
        public BuilderArguments BuilderArguments { get; set; }
    }
}