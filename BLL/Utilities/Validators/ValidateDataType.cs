using Models.Message;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Message;

namespace BLL.Utilities.Validators
{
    public static class ValidateDataType
    {
        /// <summary>
        /// kiểm tra enum Status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string ValidateStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return null;

            status = status.Trim();

            if (Enum.TryParse<LabelStatus>(status, ignoreCase: true, out var result))
            {
                return result.ToString();
            }

            return null;
        }

        /// <summary>
        /// kiểm tra dữ liệu đầu vào null hoặc empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T ValidateInput<T>(T input, string errorMessage)
        {
            if (input is string str)
            {
                input = (T)(object)str.Trim();
            }
            if (EqualityComparer<T>.Default.Equals(input, default))
            {
                throw new ArgumentException(errorMessage);
            }

            return input;
        }

        /// <summary>
        /// kiểm tra input ngày 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T ValidateDateFormat<T>(T input)
        {
            if (input is DateOnly || input is DateTime)
            {
                return input;
            }
            throw new ArgumentException("kiểu dữ liệu không hỗ trợ");
        }


        /// <summary>
        /// kiểm tra logic ngày bắt đầu, ngày kết thúc và ngày tạo
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="dueDate"></param>
        /// <param name="createDate"></param>
        /// <exception cref="Exception"></exception>
        public static void ValidateDate(DateTime? startDate, DateTime? dueDate, DateTime createDate)
        {
            if (startDate != null && startDate < createDate)
                throw new Exception("Ngày bắt đầu không được nhỏ hơn hôm nay");

            if (dueDate != null && dueDate < createDate)
                throw new Exception("Ngày kết thúc không được nhỏ hơn hôm nay");

            if (startDate != null && dueDate != null)
            {
                if (dueDate < startDate)
                    throw new Exception("Ngày kết thúc không được nhỏ hơn ngày bắt đầu");
            }
        }

        /// <summary>
        /// kiểm tra format tên Title
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="Exception"></exception>
        public static void ValidateTitle(string title)
        {
            int maxLength = 50;
            ValidateInput(title, "Title không được để trống");

            if (title.Length > maxLength)
                throw new Exception($"Title không được quá {maxLength} ký tự");
        }

        public static void ValidateDescription(string description, int maxLength)
        {
            if (description.Length > maxLength && description != null)
                throw new Exception($"description không được quá {maxLength} ký tự");
        }
    }
}
