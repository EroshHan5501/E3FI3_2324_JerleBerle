using RecipeAPI.Database.Models;
using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class RecipeModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public List<RiuRelModel> Relations { get; set; }
}
