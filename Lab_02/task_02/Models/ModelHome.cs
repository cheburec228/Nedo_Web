    using System;
    using System.ComponentModel.DataAnnotations;

    namespace Lab_02
    {
        public class ModelHome
        {
            [Required(ErrorMessage = "Ім'я обов'язкове поле")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Телефон обов'язкове поле")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Пошта обов'язкове поле")]
            [EmailAddress(ErrorMessage = "Введіть коректну адресу електронної пошти")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Дата народження обов'язкове поле")]
            public DateTime DOB { get; set; }
        }
    }
