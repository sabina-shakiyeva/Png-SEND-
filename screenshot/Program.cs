using System.Drawing;
using System.Drawing.Imaging;

string path = "yourPath";
using (Bitmap bitmap = new Bitmap(1920, 1080))
{
    using Graphics g = Graphics.FromImage(bitmap);
    g.CopyFromScreen(Point.Empty, Point.Empty, new Size(1920, 1080));
    bitmap.Save(path, ImageFormat.Png);
}