using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;

namespace SmartLife.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly SmartLifeDb _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(SmartLifeDb context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        [BindProperty]
        public List<IFormFile> AdditionalPhotos { get; set; } = new();

        public SelectList CategorySelectList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            Product = product;
            CategorySelectList = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CategorySelectList = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
                return Page();
            }

            if (ImageFile != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "products");
                Directory.CreateDirectory(uploadsFolder);

                // Delete old image if exists
                if (!string.IsNullOrEmpty(Product.Image))
                {
                    var oldImagePath = Path.Combine(_environment.WebRootPath, Product.Image.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                Product.Image = "/uploads/products/" + uniqueFileName;
            }

            if (AdditionalPhotos != null && AdditionalPhotos.Any())
            {
                var photoUrls = new List<string>();
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "products");

                // Delete old additional photos if they exist
                if (!string.IsNullOrEmpty(Product.AdditionalPhotos))
                {
                    var oldPhotos = Product.AdditionalPhotos.Split(',');
                    foreach (var oldPhoto in oldPhotos)
                    {
                        var oldPhotoPath = Path.Combine(_environment.WebRootPath, oldPhoto.TrimStart('/'));
                        if (System.IO.File.Exists(oldPhotoPath))
                        {
                            System.IO.File.Delete(oldPhotoPath);
                        }
                    }
                }

                foreach (var photo in AdditionalPhotos)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(fileStream);
                    }

                    photoUrls.Add("/uploads/products/" + uniqueFileName);
                }

                Product.AdditionalPhotos = string.Join(",", photoUrls);
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }
    }
}
