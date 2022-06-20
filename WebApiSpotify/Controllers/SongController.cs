using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSpotify.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSpotify.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        // GET: api/<SongController>
        [HttpGet]
        public ApiResponse GetAll()
        {
            return new SongModel().GetAll();
        }

        // GET api/<SongController>/5
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            return new SongModel().Get(id);
        }

        // POST api/<SongController>
        [HttpPost]
        public ApiResponse Post([FromBody] SongModel model)
        {
            return new SongModel().Add(model);
        }

        // DELETE api/<SongController>/5
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new SongModel().Delete(id);
        }

        // PUT api/<SongController>/5
        [HttpPut("{id}")]
        public ApiResponse Put([FromBody] SongModel model)
        {
            return new SongModel().Update(model);
        }


        /*
            

           
        */
    }
}
