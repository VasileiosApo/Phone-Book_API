using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phone_Book.Data.Models;
using Phone_Book.Data.Repositories;

namespace Phone_Book_API.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPersonRepository _personRepo;
        private readonly ILogger<PhoneBookController> _logger;
        public PhoneBookController(IPersonRepository personRepo, ILogger<PhoneBookController> logger)
        {
            _personRepo = personRepo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(Person person)
        {
            try
            {
                var createdPerson = await _personRepo.CreatePersonAsync(person);
                return CreatedAtAction(nameof(AddPerson), createdPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePerson(Person upPerson)
        {
            try
            {
                var existingPerson = await _personRepo.GetPeopleByIdAsync(upPerson.Id);
                if (existingPerson == null)
                {
                    return NotFound(new {
                        statusCode = 404, message = "record not found"
                    });
                }
                existingPerson.LastName = upPerson.LastName;
                existingPerson.FirstName = upPerson.FirstName;
                existingPerson.Type = upPerson.Type;
                existingPerson.PhoneNumber = upPerson.PhoneNumber;
                await _personRepo.UpdatePersonAsync(existingPerson);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var existingPerson = await _personRepo.GetPeopleByIdAsync(id);
                if (existingPerson == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "record not found"
                    });
                }
                await _personRepo.DeletePersonAsync(existingPerson);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPerson()
        {
            try
            {
                var people = await _personRepo.GetPeopleAsync();
                return Ok(people);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            try
            {
                var person = await _personRepo.GetPeopleByIdAsync(id);
                if (person == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "record not found"
                    });
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

    }
}
