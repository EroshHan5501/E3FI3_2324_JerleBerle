using RecipeAPI.Database.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Summary description for Class1
/// </summary>

[Table("Recipe")]
public class RecipeModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public List<RiuRelModel> Relations { get; set; }
}
