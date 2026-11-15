namespace MauiAppEventos.Models
{
    public class Evento
    {
        // 1. Dados básicos do evento
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime DataTermino { get; set; } = DateTime.Now;
        public int NumeroParticipantes { get; set; }
        public string Local { get; set; } = string.Empty;
        public double CustoPorParticipante { get; set; }

        // 2. Cálculo da iferença entre datas usando DateTime e TimeSpan
        public TimeSpan Duracao
        {
            get
            {
                return DataTermino.Date - DataInicio.Date;
            }
        }

        // Propriedade auxiliar para facilitar a exibição da duração em dias no Binding
        public int DuracaoEmDias
        {
            get
            {
                // Se o evento começa e termina no mesmo dia consideramos 1 dia
                int dias = Duracao.Days + 1;
                return dias < 1 ? 1 : dias;
            }
        }

        // 3. Cálculo do custo total com base no número de participantes
        public double CustoTotal
        {
            get
            {
                return NumeroParticipantes * CustoPorParticipante;
            }
        }
    }
}