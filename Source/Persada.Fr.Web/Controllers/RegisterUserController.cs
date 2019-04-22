using Persada.Fr.Facade;
using Persada.Fr.Facade.AES256Encryption;
using Persada.Fr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Persada.Fr.Web.Controllers
{
    public class RegisterUserController : Controller
    {
        IRegisterUser repository;
        public RegisterUserController()
        {
            repository = new RegisterUserConcrete();
        }

        [HttpGet]
        // GET: RegisterUser/Create
        public ActionResult Create()
        {
            return View(new RegisterUser());
        }

        // POST: RegisterUser/Create
        [HttpPost]
        public ActionResult Create(RegisterUser RegisterUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Create", RegisterUser);
                }

                // Validating Username 
                if (repository.ValidateUsername(RegisterUser))
                {
                    ModelState.AddModelError("", "User is Already Registered");
                    return View("Create", RegisterUser);
                }
                RegisterUser.CreateOn = DateTime.Now;

                // Encrypting Password with AES 256 Algorithm
                RegisterUser.Password = EncryptionLibrary.EncryptText(RegisterUser.Password);

                // Saving User Details in Database
                repository.Add(RegisterUser);
                TempData["UserMessage"] = "User Registered Successfully";
                ModelState.Clear();
                return View("Create", new RegisterUser());
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}