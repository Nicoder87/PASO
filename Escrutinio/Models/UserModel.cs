using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Escrutinio.Models
{
    public class UserModel
    {
        public long UserID { get; set; }

        //[Required]
        //[Display(Name = "Contraseña")]
        //public string Password { get; set; }

        //[Required]
        //[Display(Name = "Confirmar Contraseña")]
        //[System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "Las contraseñas ingresadas no coinciden.")]
        //public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        [Remote("existeUsuario", "User", HttpMethod = "POST", ErrorMessage = "El nombre de usuario ya existe")]
        public string Usuario { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Display(Name = "Matrícula")]
        public string Matricula { get; set; }

        public bool Visitas { get; set; }
        
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }
    }

    public class UserIndex
    {
        public long UserID { get; set; }

        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

    }

    public class UserUpdatePassword
    {
        public Guid ORID { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirmar Contraseña")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "Las contraseñas ingresadas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}