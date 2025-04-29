namespace MeNotaAi.Domain.Entities
{
    public class Visitante
    {
        public Visitante(string? nome)
        {
            Id = Guid.NewGuid();
            DataVisita = DateTime.Now;
            Nome = nome;
            CreatedAt = DateTime.Now;
        }

        public static Visitante Create(string nome)
            => new Visitante(nome);

        public Guid Id { get; private set; }
        public DateTime DataVisita { get; private set; }
        public string? Nome { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }

    public record VisitanteInputModel(string Nome);
    public record VisitanteViewModel(string Nome, DateTime DataVisita);
}
