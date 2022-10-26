using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _HostEnvironment;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          
        }

        public IActionResult Index()
        {
            
            return View();
        }

       
        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new();
           

            if (id == null || id == 0)
            {
                //create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(company);
            }
            else
            {
                //update product
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                
                return View(company);
            }
          
            
           
        }

        //POST
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Company obj)
        {

            if (ModelState.IsValid)
            {
               
               
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    TempData["success"] = "Company updated successfully";
                    _unitOfWork.Company.Update(obj);
                }
                _unitOfWork.Save();
                
                return RedirectToAction("Index");
            }
            return View(obj);
        }
       
        
        //built in api calls
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new { data =companyList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var obj = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
            if (obj == null) { 
                return Json(new {success=false,message ="Error while Deleting"});
            }

           

            _unitOfWork.Company.Remove(obj);

            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
           

        }
        #endregion

    }
}
