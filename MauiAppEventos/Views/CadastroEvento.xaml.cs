using MauiAppEventos.Models;

namespace MauiAppEventos.Views
{
    public partial class CadastroEvento : ContentPage
    {
        private Evento eventoAtual;
        private bool formatandoCusto = false;

        public CadastroEvento()
        {
            InitializeComponent();

            DateTime hoje = DateTime.Today;
            DateTime primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month, 1);

            eventoAtual = new Evento
            {
                DataInicio = hoje,
                DataTermino = hoje,
                NumeroParticipantes = 10
            };

            BindingContext = eventoAtual;

            // Define o MinimumDate no dia 1º do mês para o Windows exibir o mês inteiro (novembro de 2026)
            dtpck_inicio.MinimumDate = primeiroDiaDoMes;
            dtpck_termino.MinimumDate = primeiroDiaDoMes;Casamento 
        }

        private void dtpck_inicio_DateSelected(object sender, DateChangedEventArgs e)
        {
            // Impede escolher data anterior a hoje, mesmo mostrando o mês inteiro
            if (e.NewDate < DateTime.Today)
            {
                dtpck_inicio.Date = DateTime.Today;
                return;
            }

            // Garante que a data de término não fique menor que a de início
            if (dtpck_termino.Date < e.NewDate)
            {
                dtpck_termino.Date = e.NewDate;
            }
        }

        private void dtpck_termino_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (e.NewDate < dtpck_inicio.Date)
            {
                dtpck_termino.Date = dtpck_inicio.Date;
            }
        }

        private void txt_custo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (formatandoCusto)
                return;

            formatandoCusto = true;

            // Extrai apenas os dígitos numéricos
            string apenasNumeros = new string(e.NewTextValue?.Where(char.IsDigit).ToArray() ?? Array.Empty<char>());

            if (string.IsNullOrEmpty(apenasNumeros))
            {
                eventoAtual.CustoPorParticipante = 0;
                txt_custo.Text = string.Empty;
                formatandoCusto = false;
                return;
            }

            // Converte para centavos (ex: "25000" -> 250.00)
            double valor = double.Parse(apenasNumeros) / 100.0;
            eventoAtual.CustoPorParticipante = valor;

            string textoFormatado = valor.ToString("C2", new System.Globalization.CultureInfo("pt-BR"));
            txt_custo.Text = textoFormatado;

            // Dispatcher garante que o cursor vá para o final APÓS o Windows renderizar o texto
            Dispatcher.Dispatch(() =>
            {
                txt_custo.CursorPosition = textoFormatado.Length;
                txt_custo.SelectionLength = 0;
                formatandoCusto = false;
            });
        }

        // Ação do botão "Cadastrar Evento"
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Validação simples para evitar cadastro em branco
                if (string.IsNullOrWhiteSpace(eventoAtual.Nome))
                {
                    await DisplayAlert("Atenção", "Por favor, informe o nome do evento.", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(eventoAtual.Local))
                {
                    await DisplayAlert("Atenção", "Por favor, informe o local do evento.", "OK");
                    return;
                }

                if (eventoAtual.CustoPorParticipante < 0)
                {
                    await DisplayAlert("Atenção", "O custo por participante não pode ser negativo.", "OK");
                    return;
                }

                // Navega para a tela de Resumo passando o objeto Evento preenchido
                await Navigation.PushAsync(new ResumoEvento(eventoAtual));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops!", $"Ocorreu um erro ao cadastrar: {ex.Message}", "OK");
            }
        }
    }
}