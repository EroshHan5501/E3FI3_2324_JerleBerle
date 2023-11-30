using RecipeAPI.Database.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;


[Table("Recipe")]
public class RecipeModel
{
	public int Id { get; set; }
	public string Name { get; set; }
}
