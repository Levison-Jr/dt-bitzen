namespace DTBitzen.Services.Interfaces
{
    public interface ISalaService
    {
        Task<bool> Criar(string nomeSala, int capacidade);

        Task<bool> Editar(int salaId, string nomeSala, int capacidade);

        Task<bool> Excluir(int salaId);
    }
}
