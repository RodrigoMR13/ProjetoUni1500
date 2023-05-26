using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SystemFH.Models;

public class Person : IdentityUser<int>
{
    [DisplayName("Turma/Círculo")]
    public int? CircleId { get; set; }
    [DisplayName("Turma/Círculo")]
    public Circle Circle { get; set; }
	[DisplayName("Tipo")]
	public TypePerson Type { get; set; }
    [DisplayName("Nome")]
    public string Name { get; set; }
	public string CPF { get; set; }
	public string Email { get; set; }
    [DisplayName("Telefone")]
    public string Phone { get; set; }
    [DisplayName("Data de Nascimento")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime DateBorn { get; set; }
    [DisplayName("Nível de Estudo")]
    public NivelStudy NivelStudy { get; set; }
    [DisplayName("Universidade")]
    public string University { get; set; }
    [DisplayName("Data de Formação")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime GraduateDate { get; set; }
    [DisplayName("Data de Registro")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime DateRegister { get; set; }
    [DisplayName("Empresa")]
    public string Enterprise { get; set; }
    [DisplayName("Recomendação")]
    public bool Recommendation { get; set; }
    [DisplayName("Está Estudando?")]
    public bool IsStudying { get; set; }

    [DisplayName("Inserido Por:")]
    public int UserId { get; set; }
}

public enum TypePerson
{
	Mentorado,
	Mentor
}

public enum NivelStudy
{
    EnsinoFundamentalIncompleto,
    EnsinoFundamentalCompleto,
	EnsinoMedioIncompleto,
	EnsinoMedioCompleto,
	EnsinoSuperiorIncompleto,
	EnsinoSuperiorCompleto
}