using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task4_5.Data;
using Task4_5.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task4_5.Controllers
{
    [ApiController, Route("api/talents"), RequireHttps]
    public class TalentsController : ControllerBase
    {
        private ITalentRepository talentRepository;

        public TalentsController(ITalentRepository talentRepository)
        {
            this.talentRepository = talentRepository;
        }

        //Create Method
        [HttpPost]
        public IActionResult PostTalent(Talent inputTalent)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest("Invalid object as parameter."); }

                Talent tempTalent = talentRepository.Get(inputTalent.Id);
                if (tempTalent != null) { return Conflict("Talent with the id of " + inputTalent.Id + " already exists."); }

                talentRepository.Post(inputTalent);
                return Ok("Talent with the id of " + inputTalent.Id + " was successfully created.");
            }
            catch (Exception) { return StatusCode(500); }
        }

        //Read Methods
        [HttpGet, Route("{inputId:int:min(1)}")]
        public Talent GetTalent(int inputId) => talentRepository.Get(inputId);

        [HttpGet]
        public IEnumerable<Talent> GetAllTalents()
        {
            System.Threading.Thread.Sleep(1500);
            return talentRepository.GetAll();
        }

        //Update Method
        [HttpPut, Route("{inputId:int:min(1)}")]
        public IActionResult PutTalent(int inputId, Talent inputTalent)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid object as parameter.");
                }

                Talent tempTalent = talentRepository.Get(inputId);
                if (tempTalent == null) { return NotFound("Talent with the id of " + inputTalent.Id + " does not exist."); }
                if (tempTalent.Id != inputTalent.Id)
                {
                    tempTalent = talentRepository.Get(inputTalent.Id);
                    if (tempTalent != null) { return Conflict("Potential conflict of proposed Id detected."); }
                }
                
                talentRepository.Put(inputId, inputTalent);
                return Ok("Talent with the id of " + inputTalent.Id + " was successfully updated.");
            }
            catch (Exception) { return StatusCode(500); }
        }

        //Delete Method
        [HttpDelete, Route("{inputId:int:min(1)}")]
        public IActionResult DeleteTalent(int inputId)
        {
            try
            {
                Talent tempTalent = talentRepository.Get(inputId);
                if (tempTalent == null)
                {
                    return NotFound("Talent with the id of " + inputId + " does not exist.");
                }
                talentRepository.Delete(inputId);
                return Ok("Talent with the id of " + inputId + " was successfully deleted.");
            }
            catch (Exception) { return StatusCode(500); }
        }
    }
}
