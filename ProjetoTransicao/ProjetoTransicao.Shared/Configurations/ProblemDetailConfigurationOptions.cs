namespace ProjetoTransicao.Shared.Configurations
{
    public class ProblemDetailConfigurationOptions
    {
        public const string BaseConfig = "ProblemDetailConfiguration";
        public string? Title { get; set; }
        public string? Detail { get; set; }

        public ProblemDetailConfigurationOptions() { }
    }
}