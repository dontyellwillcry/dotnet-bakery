using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotnetBakery.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetBakery.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class BreadsController : ControllerBase
{
    private readonly ApplicationContext _context;
    public BreadsController(ApplicationContext context) 
    {
        _context = context;
    }


    // GET /api/breads
    // Returns all breads
    // Note that `IEnumerable<Bread>` is C#'s fancy way 
    // of saying "a list of Baker objects"
    [HttpGet]
    public IEnumerable<Bread> GetBreads() 
    {
        return _context.Breads
            // Include the `bakedBy` property
            // which is a list of `Baker` objects
            // .NET will do a JOIN for us!
            .Include(bread => bread.bakedBy);
    }

    // GET /api/breads/:id
    // Returns the bread with the given id
    [HttpGet("{id}")]
    public ActionResult<Bread> GetById(int id) {
        Bread bread =  _context.Breads
            .SingleOrDefault(bread => bread.id == id);
        
        if(bread is null) {
            return NotFound();
        }

        return bread;
    }

    // POST /api/breads
    // .NET automatically converts our JSON request body
    // into a `Bread` object. 
    [HttpPost]
    public Bread Post(Bread bread) 
    {
        // Tell the DB context about our new bread object
        _context.Add(bread);
        // ...and save the bread object to the database
        _context.SaveChanges();

        // Respond back with the created bread object
        return bread;
    }

    // PUT /api/breads/:id
    // Updates a bread by id
    [HttpPut("{id}")]
    public Bread Put(int id, Bread bread) 
    {
        // Our DB context needs to know the id of the bread to update
        bread.id = id;

        // Tell the DB context about our updated bread object
        _context.Update(bread);

        // ...and save the bread object to the database
        _context.SaveChanges();

        // Respond back with the created bread object
        return bread;
    }

    // DELETE /api/breads/:id
    [HttpDelete("{id}")]
    public void Delete(int id) 
    {
        // Find the bread, by ID
        Bread bread = _context.Breads.Find(id);

        // Tell the DB that we want to remove this bread
        _context.Breads.Remove(bread);

        // ...and save the changes to the database
        _context.SaveChanges();;
    }
}
}
