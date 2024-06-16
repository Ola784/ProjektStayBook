using Microsoft.AspNetCore.Mvc;
using SaleKiosk.Application.Services;
using SaleKiosk.Domain.Exceptions;
using SaleKiosk.SharedKernel.Dto;

namespace SaleKiosk.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ILogger<ReviewController> _logger;


        public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        {
            this._reviewService = reviewService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReviewDto>> Get()
        {
            var result = _reviewService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich użytkowników");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetReview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ReviewDto> Get(int id)
        {
            var result = _reviewService.GetById(id);
            _logger.LogDebug($"User o id = {id}");
            return Ok(result);
        }


        // return CreatedAtAction() - dynamicznie twrozony url
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateReviewDto dto)
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

            var id = _reviewService.Create(dto);

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
            _reviewService.Delete(id);
            _logger.LogDebug($"Usunieto usera z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateReviewDto dto)
        {
            if (id != dto.Id)
            {
                throw new BadRequestException("Id param is not valid");
            }

            _reviewService.Update(dto);
            _logger.LogDebug($"Zaktualizowano usera z id = {id}");
            return NoContent();
        }
    }
}
