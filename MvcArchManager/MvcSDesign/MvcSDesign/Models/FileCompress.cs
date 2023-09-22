using System.Drawing;
using System.Web.Security;
using System.IO;
using System.Web.Hosting;
using System.Drawing.Drawing2D;
//using System.Windows.Media;
using Brushes = System.Drawing.Brushes;
using Brush = System.Drawing.Brush;
using System.Drawing.Imaging;


namespace MvcSDesign.Models
{
    public class FileCompress
    {

        public string Save(System.Web.HttpPostedFileBase fpath, string savepath)
        {
            Image image = Image.FromStream(fpath.InputStream);
            int ActualHeight = image.Height / 10;
            int actualwidth = image.Width;
            int fntSize = 14, strpos = 0, recHeight = 160;

            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            int pos = (image.Height) - 100;
            int imgW = image.Width;
            int imgH = image.Height;

            //string str = "PROPOSED " + ptype.ToUpper() + ", DESIGNED BY " + clientInfo.ToUpper();

            int ch = imgW - imgH;
            fntSize = 8;

            var dpiX = image.HorizontalResolution;
            int area = imgW * imgH;
            if (2500000 <= area)
            {
                if (ch >= 300)
                {
                    fntSize = 12;
                    pos = (image.Height) - 100;
                }
                else
                    fntSize = 8;

                if (dpiX < 100)
                {
                    fntSize = 30;
                    pos = (image.Height) - 100;
                }

            }
            else if ((2500000 >= area) && (area <= 3000000))
            {
                fntSize = 8;
                pos = (image.Height) - 100;
                if (dpiX < 100)
                {
                    fntSize = 12;

                }
                if (ch >= 300)
                {
                    //fntSize = 16;
                    fntSize = 14;
                    pos = (image.Height) - 100;
                }

            }
            else
            {
                fntSize = 10;
                pos = (image.Height) - 100;
                if (dpiX < 100)
                {
                    fntSize = 16;

                }
            }

            strpos = pos;

            if (image.Height < 600)
            {
                recHeight = 100;
                pos = image.Height - 50;
                strpos = image.Height - 70;
                fntSize = 8;
                if (dpiX > 200)
                {
                    fntSize = 5;
                }
            }
            else if (image.Height < 800)
            {
                recHeight = 110;
                pos = image.Height - 60;
                strpos = image.Height - 80;
                fntSize = 10;
                if (dpiX > 200)
                {
                    fntSize = 3;
                }


            }
            else if (image.Height < 1000)
            {
                pos = image.Height - 70;
                strpos = image.Height - 90;
                fntSize = 10;
                recHeight = (image.Height + 5) - strpos;
                if ((image.Height < 1000) && (image.Width < 1000))
                {
                    fntSize = 5;
                }

            }
            else if (image.Height < 1500)
            {
                pos = image.Height - 80;
                strpos = image.Height - 100;
                fntSize = 12;

                recHeight = (image.Height + 10) - strpos;

                if (dpiX >= 250)
                {
                    fntSize = 10;
                }
                else if ((dpiX >= 240) && (dpiX <= 250))
                {
                    fntSize = 8;
                }

            }
            else if ((image.Height < 2500) && (image.Height > image.Width))
            {
                pos = image.Height - 100;
                strpos = image.Height - 120;

                if (dpiX <= 300)
                    fntSize = 8;
                else
                    fntSize = 14;

            }
            else if (image.Height < 2500)
            {
                pos = image.Height - 130;
                strpos = image.Height - 150;
                if (dpiX < 100)
                {
                    if (image.Width >= 2000)
                    {
                        fntSize = 22;
                    }
                    else
                    {
                        fntSize = 14;
                    }
                }
                else if (dpiX < 300)
                {
                    if (image.Width >= 2000)
                    {
                        fntSize = 14;
                    }
                    else
                    {
                        fntSize = 16;
                    }
                }
                else
                {
                    if (image.Width >= 2000)
                    {
                        fntSize = 6;
                    }
                    else
                    {
                        fntSize = 10;
                    }
                }

            }

            if (image.Height > image.Width)
            {
                pos -= 40;
                strpos -= 55;
                recHeight = 200;
            }

            using (Graphics graphics = Graphics.FromImage(image))
            {
                using (System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(150, 10, 10, 10)))
                {
                    graphics.FillRectangle(myBrush, new Rectangle(0, pos, imgW, recHeight));
                }

                //graphics.DrawString(str,
                //     new Font("Futura Bk BT", fntSize), Brushes.White,
                //     new RectangleF(10, strpos, imgW - 10, recHeight),
                //     strFormat);
            }

            image.Save(@savepath);
            image.Dispose();


            return "Y";
        }


    }
}