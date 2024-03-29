﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.AdminPanel.ViewModels;
using Pronia.DAL;
using Pronia.Models;
using System.Collections.Generic;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("AdminPanel")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Index(int page=1)
        {
            int limit = 2;

            double count = await _context.Products.CountAsync();

            if (page > (int)Math.Ceiling(count / limit) || page <= 0)
            {
                return BadRequest();
            }

            List<Tag> tags = await _context.Tags.Skip((page-1)*limit).Take(limit)
                .Include(t => t.ProductTags)
                .ToListAsync();

            PaginateVM<Tag> paginateVM = new PaginateVM<Tag>
            {
                CurrentPage = page,
                TotalPage = (int)Math.Ceiling(count / limit),
                Items = tags,
                Limit = limit
            };

            return View(paginateVM);
        }
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagVM tagVM)
        {
            if (!ModelState.IsValid) return View();

            bool result = _context.Tags.Any(c => c.Name.Trim() == tagVM.Name.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bu Tag artiq movcuddur.");
                return View();
            }

            Tag tag=new Tag
            {
                Name = tagVM.Name
            };

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (tag is null) return NotFound();

            UpdateTagVM updateTagVM = new UpdateTagVM
            {
                Name = tag.Name,
            };

            return View(updateTagVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateTagVM tagVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Tag existed = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            bool result = _context.Tags.Any(t => t.Name == tagVM.Name && t.Id != id);

            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda tag artiq movcuddur");
                return View();
            }

            existed.Name = tagVM.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Tag existed = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (existed is null) return NotFound();

            _context.Tags.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();
            Tag tag = await _context.Tags.Include(c => c.ProductTags).
                ThenInclude(p => p.Product).
                ThenInclude(p => p.ProductImages).
                FirstOrDefaultAsync(s => s.Id == id);
            if (tag == null) return NotFound();

            return View(tag);
        }
    }
}
