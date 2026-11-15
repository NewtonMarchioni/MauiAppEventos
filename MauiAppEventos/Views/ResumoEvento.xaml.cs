using MauiAppEventos.Models;

namespace MauiAppEventos.Views
{
    public partial class ResumoEvento : ContentPage
    {
        public ResumoEvento(Evento eventoCadastrado)
        {
            InitializeComponent();
            BindingContext = eventoCadastrado;
        }

        // 1. Volta mantendo a tela anterior na memória com os dados preenchidos
        private async void BtnAlterar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops!", ex.Message, "OK");
            }
        }

        // 2. Cria uma nova tela CadastroEvento limpa e remove a antiga da pilha
        private async void BtnNovoCadastro_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Insere uma nova tela CadastroEvento zerada logo abaixo da tela atual
                Navigation.InsertPageBefore(new CadastroEvento(), this);

                // Remove a página anterior antiga que estava na base da pilha
                var paginaAntiga = Navigation.NavigationStack[0];
                if (paginaAntiga is CadastroEvento)
                {
                    Navigation.RemovePage(paginaAntiga);
                }

                // Volta para a nova tela limpa
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops!", ex.Message, "OK");
            }
        }
    }
}