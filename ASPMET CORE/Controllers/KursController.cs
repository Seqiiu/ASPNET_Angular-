using ASPMET_CORE.hubs;
using ASPNET_CORE_Domain;
using ASPNETCORE_Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace ASPMET_CORE.Controllers
{
    [ApiController]
    [Route("kurs")]
    public class KursController : ControllerBase
    {
        private readonly IMessagesRepository _messagesRepository;
        private readonly IHubContext<MessageHubClient, IMessageHubClient> _messageHub;

        public KursController(IMessagesRepository messagesRepository,
                              IHubContext<MessageHubClient,IMessageHubClient> messageHub)
        {
            _messagesRepository = messagesRepository;
            _messageHub = messageHub;
        }

        [HttpGet] //end point 
        [Route("getMessages")]  //nie można wysłać 
        public IActionResult GetMassages()
        {
            var messages = _messagesRepository.GetAll();

            var messagesDto = messages.Select(x => new MessageDto
            {
                Content = x.Message,
                Author = x.LastName
            });

            //var serializedMessage = JsonConvert.SerializeObject(message); // zmiana pliku w json
            //var deserializedMessage = JsonConvert.DeserializeObject<JsonMessage>(serializedMessage);   // w drugą strone z json w plik 
            return Ok(messagesDto); // Zwaraca status 200 czyli wszystko okej z aplikacją 
        }


        [Authorize("Administrator")]
        [Route("getSomeSecretData")]
        public IActionResult GetSomeSecretData()
        {
            return Ok("PrivateKey");
        }



        //[HttpGet] //end point 
        //[Route("sendMessage")]
        //public IActionResult SendMassage(string message)
        //{
        //    var messageClass = new JsonMessage
        //    {
        //        Content = message,
        //        Author = "Jan kowalski",
        //    };
        //    return Ok(messageClass);
        //}

        [HttpPost] //end point // Wywysłanie wiadomości na stronę, Tym wyślemy hasło, do bazy danych, dane które są wraźliwe
        [Route("sendMessage")]
        public IActionResult SendMassage([FromBody]JsonMessage messageDto) //trzeba dopsiać [FromBody]
        {
            var messageEntity = new MessageEntity
            {
                Message = messageDto.Content,
                LastName = messageDto.Author

            };


            var result = _messagesRepository.Add(messageEntity);
            if (result)
            {
                _messageHub.Clients.All.NewMessage().Wait();
                return Ok(messageDto);
            }
            return NotFound();
        }

        //Pozostałe http methods służą do wysyłania innym urztkowinką dane 

    }
}
