using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using Microsoft.EntityFrameworkCore;

namespace SupermarketWEB.Pages.Paymodes
{
    public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;

        public IndexModel(SupermarketContext context)
        {
            _context = context;
        }

        public IList<Paymode> Paymodes{ get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Paymodes != null)
            {
                Paymodes = await _context.Paymodes.ToListAsync();
            }
        }
    }
}