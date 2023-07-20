using foodWasteMgt.Data;
using foodWasteMgt.Models;
using foodWasteMgt.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace foodWasteMgt.Controllers
{
    public class WasteNoFoodController : Controller
    {
        private readonly FoodDbContext context;
     
        private readonly IWebHostEnvironment webHostEnvironment;

        //ctrl + fullstop for generating Assigning private field..for constructor args.
        public WasteNoFoodController(FoodDbContext context, 
           Microsoft.AspNetCore.Hosting.IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
         
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("SaveIndex")]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ApplicationUser model)
        {
              if (ModelState.IsValid)
                {
                    ApplicationUser formModel = new ApplicationUser
                    {
                        Name = model.Name,
                        Phone = model.Phone,
                        Occupation = model.Occupation,
                        Address = model.Address,
                        City = model.City,
                        State = model.State,
                        UserType = model.UserType
                    };
                    context.Add(formModel);
                    context.SaveChanges();
                    TempData["success"] = $"Welcome Mr/Miss {model.Name} to WasteNoFood MGt";

                    if (model.UserType == 0)
                    {
                        return RedirectToAction("dashboard", "WasteNoFood");
                    }
                    else
                    {
                        //query the database and select the userid 
                        var usertoken = context.ApplicationUsers.FirstOrDefault(e => e.Phone == model.Phone);
                        var token = usertoken.Id;
                        if (usertoken != null)
                        {
                            return RedirectToAction("AddFoodWaste", "WasteNoFood", new { userToken = token });
                        }
                    }
                       
               }
               return View(model);
        }
        public IActionResult AddFoodWaste(int userToken)
        {
            ViewBag.userid = userToken;
            if(userToken <= 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFoodWaste(FoodItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                //find a way to validate the size of the image as well not to pass 1Mb
                // post.Photo.Length == 
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                FoodItem wasteFood = new FoodItem
                {
                    ApplicationUserId = model.ApplicationUserId,
                    CategoryType = model.CategoryType,
                    Weight = model.Weight,
                    Imagepath = uniqueFileName
                };
                context.Add(wasteFood);
                context.SaveChanges();
                TempData["success"] = "Food Item Submitted Successfully ,You can always Add More!!!";
                var token = model.ApplicationUserId;
                return RedirectToAction("AddFoodWaste", "WasteNoFood", new { userToken = token });
            }
            return View(model);
        }

        public async Task<IActionResult> Dashboard(string searchString, int? pageNumber)
        {
            var model = from m in context.ApplicationUsers
                        select m;
            //posts.Include(p => p.Creator);
            if (searchString != null)
            {
                pageNumber = 1;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(p => p.State.Contains(searchString) || p.Address.Contains(searchString) || p.City.Contains(searchString));
            }
            // return View(await posts.Include(p => p.Creator).OrderByDescending(p => p.Created).ToListAsync());

            int pageSize = 6;
            return View(await PaginatedList<ApplicationUser>.
                CreateAsync(model.Include(p => p.FoodItems)
                .OrderByDescending(p => p.Id).AsNoTracking(), pageNumber ?? 1, pageSize));
        }











    }
}
