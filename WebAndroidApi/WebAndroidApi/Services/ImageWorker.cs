using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using WebAndroidApi.Interfaces;

namespace WebAndroidApi.Services
{
    public class ImageWorker : IImageWorker
    {
        private readonly IConfiguration _configuration;

        public ImageWorker(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Delete(string fileName)
        {
            try
            {
                var dir = _configuration["ImagesDir"] ?? "uploading";
                string imageSizes = _configuration["ImageSizes"] ?? "50,150,300,600,1200";
                var sizes = imageSizes.Split(",")
                    .Select(x => int.Parse(x));
                foreach (var size in sizes)
                {
                    string dirSave = Path.Combine(Directory.GetCurrentDirectory(),
                        dir, $"{size}_{fileName}");

                    if (File.Exists(dirSave))
                        File.Delete(dirSave);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> Save(IFormFile image)
        {
            string imageName = String.Empty;

            using (MemoryStream ms = new())
            {
                await image.CopyToAsync(ms);
                var bytes = ms.ToArray();
                imageName = await SaveByteArray(bytes);
            }

            return imageName;
        }
        private async Task<string> SaveByteArray(byte[] bytes)
        {
            string imageName = Guid.NewGuid().ToString() + ".webp";
            

            string imageSizes = _configuration["ImageSizes"] ?? "50,150,300,600,1200";

            var sizes = imageSizes.Split(",")
                    .Select(x => int.Parse(x));

            var tasks = sizes
                .AsParallel()
                .Select(s => SaveImageAsync(bytes, imageName, s))
                .ToArray();

            await Task.WhenAll(tasks);

            return imageName;
        }

        private async Task SaveImageAsync(byte[] bytes, string name, int size)
        {
            var dir = _configuration["ImagesDir"] ?? "uploading";

            using var image = Image.Load(bytes);
            try
            {
                string dirSave = Path.Combine(Directory.GetCurrentDirectory(),
                    dir, $"{size}_{name}");
                using (var imageLoad = Image.Load(bytes))
                {
                    // Resize the image (50% of original dimensions)
                    imageLoad.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(size, size),
                        Mode = ResizeMode.Max
                    }));

                    // Save the image with compression
                    await imageLoad.SaveAsync(dirSave, new WebpEncoder());
                }
            }
            catch (Exception)
            {
                Delete(name);
            }
        }

        public async Task<string> Save(string urlImage)
        {
            string imageName = String.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Send a GET request to the image URL
                    HttpResponseMessage response = client.GetAsync(urlImage).Result;

                    // Check if the response status code indicates success (e.g., 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the image bytes from the response content
                        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                        imageName = await SaveByteArray(imageBytes);
                    }
                }
            }
            catch
            {
                return imageName;
            }
            return imageName;
        }
    }
}
