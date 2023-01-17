namespace ProjetoTransicao.Shared.Configurations
{
    public class HealthchecksConfigurationOptions
    {
        public const string? BaseConfig = "HealthchecksConfiguration";

        public int TempoDePooling { get; set; }
        public int MaximoDeEntradaPorEndpoints { get; set; }
        public string? NomeAplicacao { get; set; }
        public bool TemUIHealthcheck { get; set; }

        public HealthchecksConfigurationOptions() { }

    }
}