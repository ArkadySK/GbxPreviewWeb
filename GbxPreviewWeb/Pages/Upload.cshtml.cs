using GBX.NET.Engines.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;

namespace GbxPreviewWeb.Pages
{
    public class UploadModel : PageModel
    {
        public void OnGet()
        {
        }

        public CGameCtnChallenge? Map { get; set; }

        [BindProperty]
        public string MapImageWebPath { get; set; }



        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public UploadModel(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public IFormFile UploadMap { get; set; }
        public async Task OnPostAsync()
        {
            var file = Path.Combine(_environment.ContentRootPath, "uploads", Path.GetFileName(UploadMap.FileName));
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await UploadMap.CopyToAsync(fileStream);
            }
            GbxProcesser gbxProcesser = new GbxProcesser();
            try
            {
                Map = gbxProcesser.ProcessMap(file);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
            if (Map is null) return;
            if(Map.Thumbnail is null) return;

            ClearOtherImages();
            SaveToPng(Map.Thumbnail);   
        }


        void SaveToPng(byte[] bytes)
        {
            if(bytes == null) return;

            var file = "imgs/" + Path.GetFileNameWithoutExtension(UploadMap.FileName) + "img.jpg";

            using (var stream = new MemoryStream(bytes))
            {
                Directory.CreateDirectory(Path.Combine(_environment.ContentRootPath, "wwwroot", "imgs"));
                var img = Image.FromStream(stream);
                img.RotateFlip(RotateFlipType.Rotate180FlipX);
                img.Save(Path.Combine("wwwroot", file));
            }
            MapImageWebPath = file;
        }

        void ClearOtherImages()
        {
            var imgsFolder = _environment.ContentRootPath + "\\wwwroot\\imgs\\";

            if (!Directory.Exists(imgsFolder)) 
                return;

            foreach (var file in Directory.GetFiles(imgsFolder))
            {
                System.IO.File.Delete(file);
            }
        }
    }
}
