﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.AdminPanel.ViewModels;
using Pronia.DAL;
using Pronia.Models;
using Pronia.Utilities.Extensions;



namespace ProniaAB104.Areas.ProniaAdmin.Controllers
{
    [Area("AdminPanel")]
    public class SlideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.ToListAsync();

            return View(slides);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSlideVM slideVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            
            if (!slideVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File dont type");
                return View();
            }
            if (!slideVM.Photo.ValidateSize(2 * 1024))
            {
                ModelState.AddModelError("Photo", "More size");
                return View();
            }


            string fileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "slider");

            Slide slide = new Slide
            {
                Image = fileName,
                Title = slideVM.Title,
                SubTitle = slideVM.SubTitle,
                Description = slideVM.Description,
                Order = slideVM.Order
            };



            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (existed is null) return NotFound();

            UpdateSlideVM slideVM = new UpdateSlideVM
            {
                Image = existed.Image,
                Title = existed.Title,
                SubTitle = existed.SubTitle,
                Description = existed.Description,
                Order = existed.Order
            };

            return View(slideVM);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSlideVM slideVM)
        {

            if (!ModelState.IsValid)
            {
                return View(slideVM);
            }
            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (existed is null) return NotFound();


            if (slideVM.Photo is not null)
            {
                if (!slideVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File dont typel");
                    return View(slideVM);
                }
                if (!slideVM.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", " 2 mb dont more ");
                    return View(slideVM);
                }
                string newImage = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "slider");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "slider");
                existed.Image = newImage;

            }

            existed.Title = slideVM.Title;
            existed.Description = slideVM.Description;
            existed.SubTitle = slideVM.SubTitle;
            existed.Order = slideVM.Order;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));



        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide is null) return NotFound();

            slide.Image.DeleteFile(_env.WebRootPath, "assets", "images", "slider");


            _context.Slides.Remove(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

    }
}

