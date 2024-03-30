using System.Drawing;

namespace Cafe_Site.Services
{
    public class DefaultService : IDefaultService
    {
        public byte[] ImageToByteArray(string path)
        {
            try
            {
                Image imageIn = Image.FromFile(path);

                using (var ms = new MemoryStream())
                {
                    imageIn.Save(ms, imageIn.RawFormat);
                    return ms.ToArray();
                }
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }
    }
}
