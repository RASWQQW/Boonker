
using Microsoft.AspNetCore.Http;

namespace Boonker.Data.Models

{
    public class CreateAuthorViewModel
    {
        public Author Author { get; set; }
        public IFormFile Image { get; set; }
    }
}
