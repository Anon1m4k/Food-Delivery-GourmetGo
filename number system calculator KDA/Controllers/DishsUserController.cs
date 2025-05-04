using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model;
using Microsoft.EntityFrameworkCore;

namespace number_system_calculator_KDA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DishesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DishesController> _logger;

        public DishesController(ApplicationDbContext context, ILogger<DishesController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Получает список всех блюд
        /// </summary>
        /// <returns>Список блюд</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Dish>))]
        public IActionResult GetAll()
        {
            return Ok(_context.Dishs.ToList());
        }

        /// <summary>
        /// Получает блюдо по ID
        /// </summary>
        /// <param name="id">Идентификатор блюда</param>
        /// <returns>Найденное блюдо</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dish))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var dish = _context.Dishs.Find(id);
            if (dish == null) return NotFound();
            return Ok(dish);
        }

        /// <summary>
        /// Добавляет новое блюдо
        /// </summary>
        /// <param name="dish">Данные блюда</param>
        /// <returns>Созданное блюдо</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Dish))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] Dish dish)
        {
            _context.Dishs.Add(dish);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = dish.Id }, dish);
        }

        /// <summary>
        /// Обновляет существующее блюдо
        /// </summary>
        /// <param name="id">Идентификатор блюда</param>
        /// <param name="dish">Обновленные данные блюда</param>
        /// <returns>Результат операции</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] Dish dish)
        {
            try
            {
                if (id != dish.Id)
                {
                    return BadRequest("ID в URL не совпадает с ID в теле запроса");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingDish = await _context.Dishs.FindAsync(id);
                if (existingDish == null)
                {
                    _logger.LogWarning("Блюдо с ID {DishId} не найдено для обновления", id);
                    return NotFound();
                }

                _context.Entry(existingDish).CurrentValues.SetValues(dish);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Блюдо с ID {DishId} успешно обновлено", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении блюда с ID {DishId}", id);
                return StatusCode(500, "Произошла внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Удаляет блюдо
        /// </summary>
        /// <param name="id">Идентификатор блюда</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dish = await _context.Dishs.FindAsync(id);
                if (dish == null)
                {
                    _logger.LogWarning("Блюдо с ID {DishId} не найдено для удаления", id);
                    return NotFound();
                }

                _context.Dishs.Remove(dish);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Блюдо с ID {DishId} успешно удалено", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении блюда с ID {DishId}", id);
                return StatusCode(500, "Произошла внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Ищет блюда по названию
        /// </summary>
        /// <param name="searchTerm">Поисковый запрос</param>
        /// <returns>Список найденных блюд</returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Dish>))]
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest("Поисковый запрос не может быть пустым");
                }

                var dishes = await _context.Dishs
                    .Where(d => d.Title.Contains(searchTerm))
                    .ToListAsync();

                return Ok(dishes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при поиске блюд по запросу {SearchTerm}", searchTerm);
                return StatusCode(500, "Произошла внутренняя ошибка сервера");
            }
        }
    }
}