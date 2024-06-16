using Microsoft.AspNetCore.Mvc;
using SaleKiosk.Application.Services;
using SaleKiosk.Domain.Exceptions;
using SaleKiosk.SharedKernel.Dto;

namespace SaleKiosk.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;


        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this._userService = userService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            var result = _userService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich użytkowników");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> Get(int id)
        {
            var result = _userService.GetById(id);
            _logger.LogDebug($"User o id = {id}");
            return Ok(result);
        }


        // return CreatedAtAction() - dynamicznie twrozony url
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateUserDto dto)
        {
            // 1. Atrybut [ApiController]                               --> uruchamia automatyczną walidację
            // 2. Brak atrybutu [ApiController]                         --> automatyczna walidacja nie jest uruchamiania 
            // 3. Brak atrybutu [ApiController] + ModelState.IsValid    --> uruchamia walidację 
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //var validationResult = _validator.Validate(dto);
            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult);
            //}

            var id = _userService.Create(dto);

            _logger.LogDebug($"Dodano nowego usera z id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _userService.Delete(id);
            _logger.LogDebug($"Usunieto usera z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateUserDto dto)
        {
            if (id != dto.Id)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _userService.Update(dto);
            _logger.LogDebug($"Zaktualizowano usera z id = {id}");
            return NoContent();
        }
    }
}
