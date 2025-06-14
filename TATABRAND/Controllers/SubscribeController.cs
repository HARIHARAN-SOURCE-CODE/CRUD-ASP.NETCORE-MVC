using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TATABRAND.Data;
using TATABRAND.Models;

namespace TATABRAND.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public SubscribeController(ApplicationDbContext dbContext
            , IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _WebHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult subscribe()
        {
            List<TataPk> tata = _dbContext.tatatable.ToList();

            return View(tata);
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();


        }
        [HttpPost]
        public IActionResult Create(TataPk tataPk)
        {
            string WeebRootPath = _WebHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count>0)
            {
                string NewFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(WeebRootPath, @"Image\img\");
                var Extension = Path.GetExtension(file[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, NewFileName + Extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                string v= @"\Image\img\" + NewFileName + Extension;
                tataPk.Logo = v; 
            }
            if (ModelState.IsValid) // tabledata  store  the  data..
            {
                _dbContext.tatatable.Add(tataPk);
                _dbContext.SaveChanges();
                TempData["success"] = "Record created successfully";
                return RedirectToAction(nameof(subscribe));
            }
            return View(tataPk);

        }

        [HttpGet]

        public IActionResult Detail(int Id)
        {
            TataPk tataPk = _dbContext.tatatable.FirstOrDefault(x => x.ID == Id);
            return View(tataPk); 
        }
        // Edit  post and get
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            TataPk tataPk = _dbContext.tatatable.FirstOrDefault(x => x.ID == Id);
            return View(tataPk);
        }
        [HttpPost]
        public IActionResult Edit(TataPk tataPk)
        {
            string WeebRootPath = _WebHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                string NewFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(WeebRootPath, @"Image\img\");
                var Extension = Path.GetExtension(file[0].FileName);
                //delet old image
                var dboldpic = _dbContext.tatatable.AsNoTracking().FirstOrDefault(x => x.ID == tataPk.ID);
                try
                {

                    if (dboldpic.Logo != null)
                    {
                    var trackoldpic = Path.Combine(WeebRootPath, dboldpic.Logo.Trim('\\'));
                        if (System.IO.File.Exists(trackoldpic))
                        {
                            System.IO.File.Delete(trackoldpic);
                        }
                    }

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                using (var fileStream = new FileStream(Path.Combine(upload,NewFileName + Extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                string v = @"\Image\img\" + NewFileName + Extension;
                tataPk.Logo = v;
            }
            if (ModelState.IsValid)
            { 
                   _dbContext.tatatable.Update(tataPk);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(subscribe));
            }
            return View();

        }
        //DELETE
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            TataPk tataPk = _dbContext.tatatable.FirstOrDefault(x => x.ID == Id);
            return View(tataPk);
        }
        [HttpPost]
        public IActionResult Delete(TataPk tataPk)
        {
            string WeebRootPath = _WebHostEnvironment.WebRootPath;
            var dboldpic = _dbContext.tatatable.AsNoTracking().FirstOrDefault(x => x.ID == tataPk.ID);
            if (! string.IsNullOrEmpty(tataPk.Logo))

            {
                var trackoldpic = Path.Combine(WeebRootPath, dboldpic.Logo.Trim('\\'));
                if (System.IO.File.Exists(trackoldpic))
                {
                    System.IO.File.Delete(trackoldpic);
                }
            }
            _dbContext.tatatable.Remove(tataPk);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(subscribe));
        }

    }    
}
