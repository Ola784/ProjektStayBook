using Microsoft.AspNetCore.Mvc;
using SaleKiosk.Application.Services;
using SaleKiosk.Domain.Exceptions;
using SaleKiosk.SharedKernel.Dto;

namespace SaleKiosk.SharedKernel.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly ILogger<HotelController> _logger;

        //private readonly IValidator<CreateProductDto> _validator;

        //public ProductController(IProductService productService, IValidator<CreateProductDto> validator)
        //{
        //    this._productService = productService;
        //    _validator = validator;
        //}

        public HotelController(IHotelService hotelService, ILogger<HotelController> logger)
        {
            this._hotelService = hotelService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HotelDto>> Get()
        {
            var result = _hotelService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich hoteli");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HotelDto> Get(int id)
        {
            var result = _hotelService.GetById(id);
            _logger.LogDebug($"Pobrano hotel o id = {id}");
            return Ok(result);
        }


        // return CreatedAtAction() - dynamicznie twrozony url
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateHotelDto dto)
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

            var id = _hotelService.Create(dto);

            _logger.LogDebug($"Utworzono nowy hotel z id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _hotelService.Delete(id);
            _logger.LogDebug($"Usunieto hotel z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateHotelDto dto)
        {
            if (id != dto.Id)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _hotelService.Update(dto);
            _logger.LogDebug($"Zaktualizowano hotel z id = {id}");
            return NoContent();
        }
    }
}
