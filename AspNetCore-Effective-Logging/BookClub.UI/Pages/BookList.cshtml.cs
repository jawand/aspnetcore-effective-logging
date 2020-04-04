﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookClub.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookClub.UI.Pages
{
    public class BookListModel : PageModel
    {
        private readonly ILogger<BookListModel> _logger;
        public List<Book> Books;

        public BookListModel(ILogger<BookListModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            string userId = User.Claims.FirstOrDefault(s => s.Type == "sub")?.Value;
            _logger.LogInformation("{UserName}  ({UserId}) is about to call the book api to get all books {claims}", User.Identity.Name, userId, User.Claims);

            _logger.LogInformation("About to call API to get book list");
            using (var http = new HttpClient(new StandardHttpMessageHandler(HttpContext)))
            {
                var response = await http.GetAsync("https://localhost:44322/api/Book");
                Books = JsonConvert.DeserializeObject<List<Book>>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}