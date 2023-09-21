using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotnetBakery.Models; // Assuming Baker is in the DotnetBakery.Models namespace





namespace DotnetBakery
{
    public enum BreadType
    {
        Sourdough, //0
        Focaccia, //1
        Rye,
        White
    }
    public class Bread

    {
        

        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int count { get; set; }

        // This is the Id of the baker who made this bread
        // In a moment, we'll see how .NET can use this field to 
        // join our tables together for us
        [ForeignKey("bakedBy")]
        public int bakedById { get; set; }

        // While bakedById is an integer with the baker's ID,
        // this field is an actual Baker object. 
        // This will allow us to nest the baker object
        // inside our bread response from `GET /breads`
        public Baker bakedBy { get; set; }
        // We need this `JsonConverter` attribute
// to convert our `type` string into an Enum
[JsonConverter(typeof(JsonStringEnumConverter))]
public BreadType type {get; set;}

    }
}
