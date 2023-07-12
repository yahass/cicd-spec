using ApiSwagger.Context;
using ApiSwagger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly AppDbContext context;
        public PeliculasController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<PeliculasController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.Movie.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PeliculasController>/5
        [HttpGet("{id}", Name="GetPelicula")]
        public ActionResult Get(int id)
        {
            try
            {
                var Pelicula = context.Movie.FirstOrDefault(f => f.id == id);
                return Ok(Pelicula);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //return "value";
        }

        // POST api/<PeliculasController>
        [HttpPost]
        public ActionResult Post([FromBody]Peliculas peliculas)
        {
            try
            {
                context.Movie.Add(peliculas);
                context.SaveChanges();
                return CreatedAtRoute("GetPelicula", new { id= peliculas.id }, peliculas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PeliculasController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Peliculas peliculas)
        {
            try
            {
                if(peliculas.id == id)
                {
                    context.Entry(peliculas).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetPelicula", new { id = peliculas.id }, peliculas);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PeliculasController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id, [FromBody] Peliculas peliculas)
        {
            try
            {
                var pelicula = context.Movie.FirstOrDefault(f => f.id == id);
                if (pelicula != null)
                {
                    context.Movie.Remove(pelicula);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
