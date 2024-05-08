using System;
using System.ComponentModel.DataAnnotations; // Для атрибутів валідації

public class FormData
{
    [Required(ErrorMessage = "Поле 'Ім'я' обов'язкове для заповнення.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Поле 'Номер телефону' обов'язкове для заповнення.")]
    [DataType(DataType.PhoneNumber, ErrorMessage = "Некоректний формат номеру телефону.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Поле 'Email' обов'язкове для заповнення.")]
    [EmailAddress(ErrorMessage = "Некоректний формат email адреси.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Поле 'Дата візиту' обов'язкове для заповнення.")]
    [DataType(DataType.Date, ErrorMessage = "Некоректний формат дати.")]
    public DateTime VisitDate { get; set; }

    [Range(0, 150, ErrorMessage = "Некоректний вік.")]
    public int? Age { get; set; }

    [Required(ErrorMessage = "Поле 'Улюблена кухня' обов'язкове для заповнення.")]
    public string FavoriteCuisine { get; set; }

    [Required(ErrorMessage = "Поле 'Страви в меню' обов'язкове для заповнення.")]
    public string MenuItems { get; set; }

    [Required(ErrorMessage = "Поле 'Чому ви обрали наш заклад?' обов'язкове для заповнення.")]
    public string ReasonForChoosing { get; set; }

    [Required(ErrorMessage = "Поле 'Чи рекомендували б ви наш заклад друзям і знайомим?' обов'язкове для заповнення.")]
    public bool? RecommendToOthers { get; set; }
}
