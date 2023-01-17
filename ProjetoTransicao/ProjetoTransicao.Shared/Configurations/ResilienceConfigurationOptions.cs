namespace ProjetoTransicao.Shared.Configurations
{
    public class ResilienceConfigurationOptions
    {
        public const string? BaseConfig = "ResilienceConfiguration";
        public int QuantidadeDeRetentativas { get; set; }
        public string? NomeCliente { get; set; }

        public ResilienceConfigurationOptions() { }
    }
}