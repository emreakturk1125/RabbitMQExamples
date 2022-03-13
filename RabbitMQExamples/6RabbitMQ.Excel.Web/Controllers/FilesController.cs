using _6RabbitMQ.Excel.Web.Hubs;
using _6RabbitMQ.Excel.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _6RabbitMQ.Excel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<MyHub> _hubContext;

        public FilesController(AppDbContext context, IHubContext<MyHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpPost] 
        public async Task<IActionResult> Upload(int fileId,IFormFile file)
        {
            if (file is not { Length: > 0})
            {
                return BadRequest();
            }

            // Todo : files klasörü içinde son gün öncesi dosyaları sil

            DeleteOldFiles();

            // Todo : files klasörü içinde son gün öncesi dosyaları sil

            var userFile = await _context.UserFiles.FirstAsync(x => x.Id == fileId);
            var filePath = userFile.FileName + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", filePath);
            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);

            userFile.CreatedDate = DateTime.Now;
            userFile.FilePath = filePath;
            userFile.FileStatus = FileStatus.Completed;

            await _context.SaveChangesAsync();

            // Todo : SignalR 

            await _hubContext.Clients.User(userFile.UserId).SendAsync("CompletedFile");

            return Ok();
            
        }

        private void DeleteOldFiles()
        {
            var path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files");
            string[] files = Directory.GetFiles(path1);

            foreach (string item in files)
            {
                FileInfo fi = new FileInfo(item);
                if (fi.LastAccessTime < DateTime.Now.AddDays(-1))
                    fi.Delete();
            }

            var list = _context.UserFiles.AsNoTracking().Where(x => x.CreatedDate < DateTime.Now.AddDays(-1)).ToArray();
            _context.UserFiles.RemoveRange(list);
        }
    }
}
