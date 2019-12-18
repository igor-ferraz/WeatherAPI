namespace Weather.Domain.DataContracts
{
    public class ViaCepResponse
    {
        public string Localidade { get; set; }
        public string UF { get; set; }
        public bool Erro { get; set; }
    }
}