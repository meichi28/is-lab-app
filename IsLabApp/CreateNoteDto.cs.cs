using System.ComponentModel.DataAnnotations;

namespace NotesApi.Models
{
    public class CreateNoteDto
    {
        [Required(ErrorMessage = "Заголовок обязателен")]
        [StringLength(100, ErrorMessage = "Не более 100 символов")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Текст обязателен")]
        [StringLength(1000, ErrorMessage = "Не более 1000 символов")]
        public string Text { get; set; } = string.Empty;
    }
}