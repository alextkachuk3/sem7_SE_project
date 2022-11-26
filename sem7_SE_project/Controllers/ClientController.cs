using Microsoft.AspNetCore.Mvc;
using sem7_SE_project.Services.ClientService;

namespace sem7_SE_project.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public JsonResult SearchClients(string? searchWord)
        {
            return new JsonResult(_clientService.SearchClients(searchWord).Select(s => new
            {
                id = s.Id,
                text = s.FirstName + " " + s.LastName + " | " + s.Address + " | " + s.PhoneNumber + " | " + s.Email
            }));
        }
    }
}
