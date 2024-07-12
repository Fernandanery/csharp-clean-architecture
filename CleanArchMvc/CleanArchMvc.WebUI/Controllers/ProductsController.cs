﻿using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController(IProductService productService, ICategoryService categoryService) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId =
            new SelectList(await _categoryService.GetCategories(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.Add(productDto);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId =
                            new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            }
            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _productService.GetById(id);

            if (productDto == null)
                return NotFound();

            var categories = await _categoryService.GetCategories();

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name",
                 productDto.CategoryId);

            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(productDto);
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _productService.GetById(id);

            if(productDto == null)
                return NotFound();

            return View(productDto);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
