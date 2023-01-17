namespace ProjetoTransicao.Shared.Configurations
{
    public class CacheConfigurationOptions
    {
        public const string BaseConfig = "Cache";
        public string? ChaveProdutoCache { get; set; }
        public int TempoDeExpiracaoRelativo { get; set; }
        public int TempoOcioso { get; set; }
    }
}
