using System.ComponentModel.DataAnnotations;

namespace task_07.Models
{
    public class RepeatInput
    {
        [Required(ErrorMessage = "Поле необхідне.")]
        public string InputText { get; set; }

        [Required(ErrorMessage = "Поле необхідне.")]
        [Range(1, int.MaxValue, ErrorMessage = "Значення повинно бути більше 0.")]
        public int RepeatCount { get; set; }

        public string ProcessedText { get; set; }
    }
}
