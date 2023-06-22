namespace Api_Marcador.Resultado.Marcadores
{
    public class ListaMarcadores:ResultadoBase
    {
        public List<ItemMarcador> ListMarcadores { get; set; } = new List<ItemMarcador>();
    }
    public class ItemMarcador
    {
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Info { get; set; }

    }

    public class RespuestaMarcadores
    {
        public string Error { get; set; }
        public bool Ok { get; set; }
        public string MensajeInfo { get; set; }
        public int StatusCode { get; set; }
        public List<ItemMarcador> LitadoMarcadores { get;set; }
    }
}
