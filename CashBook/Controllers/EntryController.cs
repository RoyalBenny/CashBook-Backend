using CashBook.Business;
using CashBook.DAL.Entity;
using CashBook.Data.Entity;
using CashBook.Data.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CashBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class EntryController : ControllerBase
    {
        private readonly IEntryServices _entryServices;
        private readonly UserManager<User> _userManager;    
        public EntryController(IEntryServices entryServices, UserManager<User> userManager)
        {
            _entryServices = entryServices;
            _userManager = userManager;
        }

        // GET: api/<EntryController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                return Ok(_entryServices.GetEntries(User.Identity.Name));
            }catch(EntryNotFoundException ex)
            {
                return BadRequest(ex.Message); 
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<EntryController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<EntryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Entry entry)
        {
            try
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                entry.user = currentUser;
                return Ok(_entryServices.AddEntry(entry));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("{year}")]
        public IActionResult GetByYear(int year)
        {
            try
            {
                return Ok(_entryServices.GetEntriesByYear(User.Identity.Name,year));
            }catch(InvalidYearException ex)
            {
                return BadRequest(ex.Message);
            }catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{month}/{year}")]
        public IActionResult GetByMonth(int month,int year)
        {
            try
            {
                return Ok(_entryServices.GetEntriesByMonth(User.Identity.Name,month,year));
            }
            catch(InvalidMonthException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidYearException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // PUT api/<EntryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<EntryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
