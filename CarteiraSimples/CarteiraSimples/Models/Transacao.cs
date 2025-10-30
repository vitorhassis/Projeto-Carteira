namespace CarteiraSimples.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public DateTime Data { get; set; }
    }
}
