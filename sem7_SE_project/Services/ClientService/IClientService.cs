using sem7_SE_project.Models;

namespace sem7_SE_project.Services.ClientService
{
    public interface IClientService
    {
        public List<Client> GetClients();

        public Client? GetClient(int clientId);

        public void AddClient(string FirstName, string LastName, string? Address, string? PhoneNumber, string? Email);

        public void DeleteClient(int clientId);

        public void EditClient(int clientId, string FirstName, string LastName, string? Address, string? PhoneNumber, string? Email);

        public List<Client> SearchClients(string? searchWord);
    }
}
