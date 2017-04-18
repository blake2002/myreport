using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSCADA.ReportEngine
{
    class SizeConversion
    {
        /// <summary>
        /// 获取微米尺寸
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static float GetUmSize(string unit, float size)
        {
            // Size is specified in CSS Length Units
            // format is <decimal number nnn.nnn><optional space><unit>
            // in -> inches (1 inch = 2.54 cm)
            // cm -> centimeters (.01 meters)
            // mm -> millimeters (.001 meters)
            // pt -> points (1 point = 1/72.27 inches)
            // pc -> Picas (1 pica = 12 points)

            float umSize = 0.0f;
            switch (unit.ToLower())
            {
                case "cm":
                    umSize = size * 10000;
                    break;
                case "mm":
                    umSize = size * 1000;
                    break;
                case "pt":
                    umSize = size * (25400 / 72.7f);
                    break;
                case "pc":
                    umSize = size * (25400 / 72.7f * 12);
                    break;
                case "in":
                default:
                    umSize = size * 25400;
                    break;
            }
            return umSize;
        }
        /// <summary>
        /// 获取像素值对应的微米尺寸
        /// </summary> 
        /// <returns></returns>
        public static float ConvertPixelToUm(float size)
        {
            // Size is specified in CSS Length Units
            // format is <decimal number nnn.nnn><optional space><unit>
            // in -> inches (1 inch = 2.54 cm) 
            // pt -> points (1 point = 1/72.27 inches)  

            return (float)(size * (25400 / 96.0f));
        }
        /// <summary>
        /// 获取微米尺寸对应的像素值
        /// </summary> 
        /// <returns></returns>
        public static float ConvertUmToPixel(float size)
        {
            // Size is specified in CSS Length Units
            // format is <decimal number nnn.nnn><optional space><unit>
            // in -> inches (1 inch = 2.54 cm) 
            // pt -> points (1 point = 1/72.27 inches)   
            return (float)(size * 96.0f / 25400);
        }

        /// <summary>
        /// 获取像素值对应的英寸尺寸
        /// </summary> 
        /// <returns></returns>
        public static float ConvertPixelToInches(float size,float Dpi=96.0f)
        {
            if (Dpi != 0.0f)
            {
                return (float)(size / Dpi);
            }
            return size;
        }
        /// <summary>
        /// 获取英寸尺寸对应的像素值
        /// </summary> 
        /// <returns></returns>
        public static float ConvertInchesToPixel(float size, float Dpi = 96.0f)
        {
            return (float)(size * Dpi);
        }

		/// <summary>
		/// 获取英寸尺寸对应的像素值
		/// </summary> 
		/// <returns></returns>
		public static float ConvertInchesToPixel(float size,float max, float Dpi = 96.0f)
		{
			var px = (float)(size * Dpi);
			return px > max ? max : px;
		}

        /// <summary>
        /// 获取像素值对应的厘米尺寸
        /// </summary> 
        /// <returns></returns>
        public static float ConvertPixelToCentimeter(float size, float Dpi = 96.0f)
        {
            if (Dpi != 0.0f)
            {
                return (float)((size / Dpi)*2.54);
            }
            return size; 
        }

        /// <summary>
        /// 获取厘米尺寸对应的像素值
        /// </summary> 
        /// <returns></returns>
        public static float ConvertCentimeterToPixel(float size, float Dpi = 96.0f)
        {  
            return (float)((size/2.54) * Dpi);
        }

        /// <summary>
        /// 获取英寸尺寸对应的厘米尺寸
        /// </summary> 
        /// <returns></returns>
        public static float ConvertInchesToCentimeter(float size)
        { 
           return (float)(size  * 2.54); 
        }

        /// <summary>
        /// 获取厘米尺寸对应的英寸尺寸
        /// </summary> 
        /// <returns></returns>
        public static float ConvertCentimeterToInches(float size)
        {
            return (float)(size / 2.54); 
        }
    }
}
