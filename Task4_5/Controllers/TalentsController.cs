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

        #region API Controllers
        //1. Get Talents
        [HttpGet]
        public IEnumerable<Talent> GetAllTalents()
        {
            //System.Threading.Thread.Sleep(1500);
            return talentRepository.GetAll();
        }

        //2. Get Talent by Id
        [HttpGet, Route("{Id:int:min(1)}")]
        public Talent GetTalent(int Id) => talentRepository.Get(Id);

        //3. Create Talent
        [HttpPost]
        public IActionResult PostTalent(Talent inputTalent)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest("Invalid object as parameter."); }

                Talent tempTalent = talentRepository.Get(inputTalent.Id);
                if (tempTalent != null) { return Conflict("Talent " + inputTalent.Id + " already exists."); }

                talentRepository.Post(inputTalent);
                return Ok("Talent " + inputTalent.Id + " was successfully created.");
            }
            catch (Exception) { return StatusCode(500); }
        }

        //4. Update Talent
        [HttpPut, Route("{Id:int:min(1)}")]
        public IActionResult PutTalent(int Id, Talent newTalent)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid object as parameter.");
                }

                Talent oldTalent = talentRepository.Get(Id);
                if (oldTalent == null) {
                    return NotFound("Talent with the id of " + newTalent.Id + " does not exist.");
                }

                if (oldTalent.Id != newTalent.Id) {
                    return Conflict("Id Mismatch.");
                }
                
                talentRepository.Put(Id, newTalent);
                return Ok("Talent " + newTalent.Id + " was successfully updated.");
            }
            catch (Exception) { return StatusCode(500); }
        }

        //4. Delete Talent
        [HttpDelete, Route("{inputId:int:min(1)}")]
        public IActionResult DeleteTalent(int inputId)
        {
            try
            {
                Talent tempTalent = talentRepository.Get(inputId);
                if (tempTalent == null)
                {
                    return NotFound("Talent " + inputId + " does not exist.");
                }
                talentRepository.Delete(inputId);
                return Ok("Talent " + inputId + " was successfully deleted.");
            }
            catch (Exception) { return StatusCode(500); }
        }
        #endregion
    }
}
