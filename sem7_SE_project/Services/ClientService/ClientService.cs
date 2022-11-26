using sem7_SE_project.Data;
using sem7_SE_project.Models;

namespace sem7_SE_project.Services.ClientService
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddClient(string FirstName, string LastName, string? Address, string? PhoneNumber, string? Email)
        {
            Client client = new Client();
            client.FirstName = FirstName;
            client.LastName = LastName;
            client.Address = Address;
            client.PhoneNumber = PhoneNumber;
            client.Email = Email;
            try
            {
                _dbContext.Clients!.Add(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                _dbContext.SaveChanges();
            }
        }

        public void DeleteClient(int clientId)
        {
            var client = GetClient(clientId);
            try
            {
                if (client != null)
                {
                    _dbContext.Clients!.Remove(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                _dbContext.SaveChanges();
            }
        }

        public Client? GetClient(int clientId)
        {
            return _dbContext.Clients!.Where(c => c.Id.Equals(clientId)).FirstOrDefault();
        }

        public List<Client> GetClients()
        {
            return _dbContext.Clients!.ToList();
        }
    }
}
